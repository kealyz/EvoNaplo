import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentService} from '../../services/student.service';
import { Student } from '../../models/student.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from 'src/app/services/dialog.service';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;


  data : Student[];
  displayedColumns = ['firstName', 'lastName', 'email', 'phoneNumber', 'actions'];
  dataSource = new MatTableDataSource<Student>(this.data);
  
  constructor(private studentService: StudentService, private dialogService: DialogService) { }

  ngOnInit(): void {
    this.getAllStudents();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getAllStudents(){
    let students = this.studentService.getStudents();
    students.subscribe(student => this.dataSource.data = student as Student[] );
  }

  deleteStudent(id:number){
    this.dialogService.openConfirmDialog("Are you sure you want to delete this record?")
    .afterClosed().subscribe(res => {
      if(res){
        this.studentService.deleteStudent(id).subscribe(student => {
          this.getAllStudents();
        });
      }
    });
  }

  editStudent(id:number){
    this.dialogService.openStudentEditDialog().afterClosed().subscribe(res => {
      console.log("Dialog output:", res);
      if(res){
        this.studentService.editStudent(id, res).subscribe(student => {
          this.getAllStudents();
        });
      }
    });
  }
}