import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators, FormBuilder,ReactiveFormsModule } from '@angular/forms';
import { EmployeesService } from '../services/employees.service';
import {  Router } from '@angular/router';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css'
})
export class SigninComponent implements OnInit {
  constructor(public employeesService:EmployeesService,private router:Router,private formbuilder:FormBuilder){}
  ngOnInit(): void {
    this.addform = this.formbuilder.group({
      name: ['', [Validators.required, Validators.maxLength(20), Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.maxLength(10)]]
    });   
  }
  addform: FormGroup = new FormGroup({  
  });
  addstudent(){    
      const data = this.addform.value;        
      this.employeesService.saveemployee(data);   
    }     
  }


