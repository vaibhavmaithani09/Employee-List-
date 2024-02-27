import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { UpdatepageComponent } from './updatepage/updatepage.component';
import { SigninComponent } from './signin/signin.component';

const routes: Routes = [
  {
    path:'header',
    component:HeaderComponent

  },
  {
    path: 'employee/:id',
    component: UpdatepageComponent
},
{
  path: 'header/employee/signin',
  component: SigninComponent
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
