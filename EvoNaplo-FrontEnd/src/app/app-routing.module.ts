import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentComponent } from './students-views/student/student.component';
import { StudentListComponent } from './students-views/student-list/student-list.component';
import { MentorComponent } from './mentor-views/mentor/mentor.component';
import { MentorListComponent } from './mentor-views/mentor-list/mentor-list.component';
import { SemesterAddComponent } from './semester-views/semester-add/semester-add.component';
import { SemesterListComponent } from './semester-views/semester-list/semester-list.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

import { ProjectListComponent } from './project-views/project-list/project-list.component';
import { ProjectAddComponent } from './project-views/project-add/project-add.component';
import { ProjectComponent } from './project-views/project/project.component';
import { ProjectsInSemesterComponent } from './project-views/projects-in-semester/projects-in-semester.component';
import { SemesterStarterComponent } from './semester-starter/semester-starter/semester-starter.component';
import { ForbiddenComponent } from './error-pages/forbidden/forbidden.component';
import { SemesterStarterAdminComponent } from './semester-starter-admin/semester-starter-admin/semester-starter-admin.component';
import { CurrentUserComponent } from './current-user/current-user.component';



const routes: Routes = [
  { path: 'student', component: StudentComponent},
  { path: 'student-list', component: StudentListComponent},
  { path: 'mentor', component: MentorComponent},
  { path: 'mentor-list', component: MentorListComponent},
  { path: 'semester-add', component: SemesterAddComponent},
  { path: 'semester-list', component: SemesterListComponent},
  { path: 'home', component: HomeComponent},
  { path: 'login', component: LoginComponent,},
  { path: 'projects-in-semester/:id', component: ProjectsInSemesterComponent},
  { path: 'semester-starter', component: SemesterStarterComponent},
  { path: 'project-add', component: ProjectAddComponent},
  { path: 'project-list', component: ProjectListComponent},
  { path: 'project/:id', component: ProjectComponent},
  { path: 'forbidden', component: ForbiddenComponent},
  { path: 'semester-starter-admin', component: SemesterStarterAdminComponent},
  { path: 'current-user', component: CurrentUserComponent},


  //LEGALULRA!!
  { path: '**', redirectTo: 'home'},
//DONTMOVE!!

  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
