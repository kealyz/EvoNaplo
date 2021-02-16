import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Semester } from '../models/semester.model';

@Injectable({
  providedIn: 'root'
})
export class SemesterService {

  constructor(private http: HttpClient) {
     
  }

  getSemesters(): Observable<Semester[]>{
    return this.http.get<Semester[]>('api/Semester');
  }

  editSemester(id, semester){
    return this.http.put('api/Semester/edit?id='+id, semester);
  }

}
