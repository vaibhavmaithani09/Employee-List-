import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employ } from '../employ';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
 url="http://localhost:5034/api/Employee";
  
  constructor(private http:HttpClient,private router:Router) {}
    Employee(){
     
      return this.http.get<Employ[]>(this.url);
      
   }
   employeebyid(employeeId: number): Observable<Employ> {
    return this.http.get<Employ>(`${this.url}/${employeeId}`);
  }
  
   saveemployee(data:any){
 
 
    
      this.http.post(this.url,data).subscribe(res =>
      {
        this.router.navigate(['/header']);
      });
   
      
    
   }
   delete(id: number) {
    
    const deleteUrl = `${this.url}/${id}`;
   
    return  this.http.delete(deleteUrl);
    
    ;
  }
  update(inputdata: any,id:number) {      
    const updateUrl = `http://localhost:5034/api/Employee/${id}`;   
     this.http.put<any>(updateUrl, inputdata).subscribe((res)=>{
      this.router.navigate(['/header']);     
      console.log(res);   
    })   
  }
}
