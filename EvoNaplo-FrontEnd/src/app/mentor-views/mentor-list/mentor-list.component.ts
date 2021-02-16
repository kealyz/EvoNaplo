import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MentorService } from 'src/app/services/mentor.service';
import { Mentor } from 'src/app/models/mentor.model';

@Component({
  selector: 'app-mentor-list',
  templateUrl: './mentor-list.component.html',
  styleUrls: ['./mentor-list.component.scss']
})
export class MentorListComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  data : Mentor[];
  displayedColumns = ['firstName', 'lastName', 'email', 'phoneNumber'];
  dataSource = new MatTableDataSource<Mentor>(this.data);
  
  constructor(private mentorService: MentorService) { }

  ngOnInit(): void {
    this.getAllMentors();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getAllMentors(){
    let mentors = this.mentorService.getMentors();
    mentors.subscribe(mentor => this.dataSource.data = mentor as Mentor[] );
  }
}