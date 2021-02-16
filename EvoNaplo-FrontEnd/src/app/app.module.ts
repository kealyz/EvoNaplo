import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { MentorComponent } from './mentor-views/mentor/mentor.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { StudentService } from './services/student.service';
import { StudentComponent } from './students-views/student/student.component';
import { StudentListComponent } from './students-views/student-list/student-list.component';
import { SemesterAddComponent } from './semester-views/semester-add/semester-add.component';
import { SemesterListComponent } from './semester-views/semester-list/semester-list.component';
import { SemesterService } from './services/semester.service';
import { MaterialModule } from './material/material.module';
import { MentorListComponent } from './mentor-views/mentor-list/mentor-list.component';
import { MentorService } from './services/mentor.service';
import { ConfirmDeleteComponent } from './dialogs/confirm-delete/confirm-delete.component';
import { DialogService } from './services/dialog.service';
import { StudentEditDialogComponent } from './dialogs/edit-dialogs/student-edit-dialog/student-edit-dialog.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { ProjectAddComponent } from './project-views/project-add/project-add.component';
import { ProjectListComponent } from './project-views/project-list/project-list.component';
import { ProjectService } from './services/project.service';
import { SemesterEditDialogComponent } from './dialogs/edit-dialogs/semester-edit-dialog/semester-edit-dialog.component';
import { ProjectComponent } from './project-views/project/project.component';
import { ProjectsInSemesterComponent } from './project-views/projects-in-semester/projects-in-semester.component';
import { SemesterStarterComponent } from './semester-starter/semester-starter/semester-starter.component';
import { EnvironmentUrlService } from './services/environment-url.service';
import { AuthenticationService } from './services/authentication.service';
import { AuthInterceptor } from './Interceptors/auth-interceptor';
import { ForbiddenComponent } from './error-pages/forbidden/forbidden.component';
import { SemesterStarterAdminComponent } from './semester-starter-admin/semester-starter-admin/semester-starter-admin.component';
import { CurrentUserComponent } from './current-user/current-user.component';



@NgModule({
  declarations: [
    AppComponent,
    MentorComponent,
    StudentComponent,
    SemesterAddComponent,
    SemesterListComponent,
    StudentListComponent,
    MentorListComponent,
    ConfirmDeleteComponent,
    LoginComponent,
    HomeComponent,
    ProjectAddComponent,
    ProjectListComponent,
    StudentEditDialogComponent,
    SemesterEditDialogComponent,
    ProjectComponent,
    ProjectsInSemesterComponent,
    SemesterStarterComponent,
    ForbiddenComponent,
    SemesterStarterAdminComponent,
    CurrentUserComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    MaterialModule,
  ],
  providers: [
    StudentService,
    SemesterService,
    MentorService,
    MaterialModule,
    DialogService,
    ProjectService,
    EnvironmentUrlService,
    AuthenticationService,

    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
