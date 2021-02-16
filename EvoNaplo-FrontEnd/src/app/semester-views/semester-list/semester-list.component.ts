import { Component, OnInit, ViewChild } from '@angular/core';
import { SemesterService } from '../../services/semester.service';
import { Semester } from '../../models/semester.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/services/dialog.service';

@Component({
  selector: 'app-semester-list',
  templateUrl: './semester-list.component.html',
  styleUrls: ['./semester-list.component.scss']
})
export class SemesterListComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  data : Semester[];
  displayedColumns = ['startDate', 'endDate', 'demoDate', 'actions'];
  dataSource = new MatTableDataSource<Semester>(this.data);
  
  constructor(private dialogService: DialogService,private semesterService: SemesterService,private router: Router) { }

  ngOnInit(): void {
    this.getAllSemesters();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim();
  }

  getAllSemesters(){
    let sem = this.semesterService.getSemesters();
    sem.subscribe(semester => this.dataSource.data = semester as Semester[] );
  }

  
  editSemester(id:number){
    this.dialogService.openSemesterEditDialog().afterClosed().subscribe(res => {
      console.log("Dialog output:", res);
      if(res){
        this.semesterService.editSemester(id, res).subscribe(semester => {
          this.getAllSemesters();
        });
      }
    });
  }
  goToProjects(id):void
  {
    this.router.navigate(['/projects-in-semester',id]);
  }

}
