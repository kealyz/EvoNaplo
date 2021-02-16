import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-semester-edit-dialog',
  templateUrl: './semester-edit-dialog.component.html',
  styleUrls: ['./semester-edit-dialog.component.scss']
})
export class SemesterEditDialogComponent implements OnInit {

  semesterEditForm: FormGroup;
  semester: any;

  constructor(@Inject(MAT_DIALOG_DATA) public SemesterEditDialogData, public dialogRef: MatDialogRef<SemesterEditDialogComponent>, private fb: FormBuilder) {
    this.semester = SemesterEditDialogData.semester;
  }

  ngOnInit(): void {
    this.semesterEditForm = this.fb.group({
      startDate: ['',[
        Validators.required
      ]],
      endDate: ['', [
        Validators.required
      ]],
      demoDate: ['', [
        Validators.required
      ]]
    });

    this.semesterEditForm.valueChanges.subscribe(console.log);
  }

  get startDate(){
    return this.semesterEditForm.get('startDate');
  }

  get endDate(){
    return this.semesterEditForm.get('endDate');
  }

  get demoDate(){
    return this.semesterEditForm.get('demoDate');
  }

  public submit(){
    const semesterToPass = {
      "startDate" : this.formatDate(this.startDate.value),
      "endDate" : this.formatDate(this.endDate.value),
      "demoDate" : this.formatDate(this.demoDate.value)
    }
    this.dialogRef.close(semesterToPass);
  }

  private addLeadingZeros(value: number): string {
    return ('0' + value).slice(-2);
  }
 
  private formatDate(date: Date): string {
    return `${date.getFullYear()}-${this.addLeadingZeros(date.getMonth() + 1)}-${this.addLeadingZeros(date.getDate())}`;
  }

  close(){
    this.dialogRef.close();
  }
}
