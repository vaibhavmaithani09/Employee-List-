import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule,FormGroup ,FormControl,FormBuilder,Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeesService } from '../services/employees.service';

@Component({
  selector: 'app-updatepage',
  templateUrl: './updatepage.component.html',
  styleUrl: './updatepage.component.css'
})
export class UpdatepageComponent implements OnInit {
  employeeId: any;
  constructor(private route :ActivatedRoute,private employeeservice :EmployeesService,
    private router:Router,private formbuilder:FormBuilder){ 
  }
  updateform: FormGroup = new FormGroup({ 
  });
  ngOnInit() {
    this.employeeId = this.route.snapshot.paramMap.get('id'); 
    this.employeeservice.employeebyid(this.employeeId).subscribe((res) => {
      if (res) {
        this.updateform.patchValue(res);
      }
    });
    this.updateform = this.formbuilder.group({
      name: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.maxLength(10)]]
    });   
  } 
updatestudent() {
  const inputdata = this.updateform.value;
  this.employeeservice.update(inputdata, this.employeeId);
}
    }
  

