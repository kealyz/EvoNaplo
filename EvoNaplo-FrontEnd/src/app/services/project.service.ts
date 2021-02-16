import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/project.model';
import { UserProject } from 'src/app/models/userproject.model';
import { SemesterAdminProjectModel } from '../models/semesterAdminProject.model';


@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) {

  }

  getProjects(): Observable<Project[]>{
    return this.http.get<Project[]>('api/Project');
  }

  getProjectById(id)
  {
    return this.http.get<Project>('api/Project/id?id='+id);
  }

  getProjectsBySemesterId(id): Observable<Project[]>
  {
    return this.http.get<Project[]>('api/Project/semesterProjects?id='+id);
  }

  assignUserToProject(email,id)
  {
    this.http.post('api/Project/assignUserToProject?email='+email+'&id='+id,null).subscribe(() => {window.location.reload()});
  }
  
  getMyProjectForTheSemester(email)
  {
    return this.http.get<Project>('api/Project/getMyProjectForTheSemester?email='+email);
  }

  deleteProjectSubscription(id)
  {
    this.http.delete('api/Project/deleteProjectSubscription?id='+id).subscribe(()=>{window.location.reload()});
  }

  getSubscribedProjectId(email)
  {
    return this.http.get<any>('api/Project/getSubscribedProject?email='+email);
  }

  getSemesterAdminProjects(id)
  {
    return this.http.get<SemesterAdminProjectModel[]>('api/Project/adminSemesterProjects?id='+id);
  }

  getSemesterProjectById(id)
  {
    return this.http.get<SemesterAdminProjectModel>('api/Project/getSemesterProjectById?id='+id)
  }
}
