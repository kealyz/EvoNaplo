import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Mentor } from '../models/mentor.model';

@Injectable({
  providedIn: 'root'
})
export class MentorService {

  constructor(private http: HttpClient) {
     
  }

  getMentors(): Observable<Mentor[]>{
    return this.http.get<Mentor[]>('api/Mentor');
  }
}
