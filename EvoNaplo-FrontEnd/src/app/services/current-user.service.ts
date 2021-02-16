import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { currentUser } from '../models/currentUser.model';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {

  constructor(private http: HttpClient) { }

  getCurrentUser(): Observable<currentUser> {
    return this.http.get<currentUser>('api/Student/currentUser');
  }
}
