import { Component, OnInit } from '@angular/core';
import { currentUser } from '../models/currentUser.model';
import { CurrentUserService } from '../services/current-user.service';

@Component({
  selector: 'app-current-user',
  templateUrl: './current-user.component.html',
  styleUrls: ['./current-user.component.scss']
})
export class CurrentUserComponent implements OnInit {
  user: currentUser;

  constructor(private currentUserService: CurrentUserService) {
  }

  ngOnInit(): void {
    this.currentUserService.getCurrentUser().subscribe((usr: currentUser) => {
      this.user = usr;
    })
  }
}
