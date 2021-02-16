import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../models/student.model';
import { currentUser } from '../models/currentUser.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) {
     
  }

  getStudents(): Observable<Student[]>{
    return this.http.get<Student[]>('api/Student');
  }

  deleteStudent(id){
    return this.http.delete('api/Student/delete?id='+id)
  }

  editStudent(id, student){
    return this.http.put('api/Student/edit?id='+id, student);
  }

  getStudentsOnSemesterWithoutProject()
  {
    return this.http.get<Student[]>('api/Student/getStudentsOnSemesterWithoutProject');
  }
}
