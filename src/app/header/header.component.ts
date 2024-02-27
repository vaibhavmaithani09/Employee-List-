import { Component, OnInit } from '@angular/core';
import { EmployeesService } from '../services/employees.service';
import { Observable } from 'rxjs';
import { Employ } from '../employ';
import { ChangeDetectorRef } from '@angular/core'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

 
  data: any;
  loginform: any;
  

  constructor(private employeesService: EmployeesService,private cdr: ChangeDetectorRef) {}
 
  

  ngOnInit() {
    this.employeesService.Employee().subscribe(data =>{ this.data = data});

   
  }

  
  delete(id: number) {
    this.employeesService.delete(id).subscribe(() => {
      this.cdr.detectChanges();
  console.log("delete done");
  
  // Update local data
  this.data = this.data.filter((e:any) => e.id !== id);

    });
    
    
  }
 
  
  
     
      
    
  }
  
    
  


