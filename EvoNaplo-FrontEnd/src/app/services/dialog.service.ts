import { Injectable } from '@angular/core';
import { ConfirmDeleteComponent } from '../dialogs/confirm-delete/confirm-delete.component';
import { MatDialog } from '@angular/material/dialog';
import { StudentEditDialogComponent } from '../dialogs/edit-dialogs/student-edit-dialog/student-edit-dialog.component';
import { SemesterEditDialogComponent } from '../dialogs/edit-dialogs/semester-edit-dialog/semester-edit-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog: MatDialog) { }

  openConfirmDialog(msg){
    return this.dialog.open(ConfirmDeleteComponent, {
      width: '390px',
      panelClass: 'confirm-dialog-container',
      disableClose: true,
      data :{
        message: msg
      }
    });
  }

  openStudentEditDialog(){
    return this.dialog.open(StudentEditDialogComponent, {
      width: '20%',
      panelClass: 'confirm-dialog-container',
      disableClose: true,
      data: {

      }
    })
  }

  openSemesterEditDialog(){
    return this.dialog.open(SemesterEditDialogComponent, {
      width: '20%',
      panelClass: 'confirm-dialog-container',
      disableClose: true,
      data: {

      }
    })
  }
}
