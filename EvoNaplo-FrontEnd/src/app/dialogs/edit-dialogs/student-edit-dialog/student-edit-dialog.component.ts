import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-student-edit-dialog',
  templateUrl: './student-edit-dialog.component.html',
  styleUrls: ['./student-edit-dialog.component.scss']
})
export class StudentEditDialogComponent implements OnInit {

  studentEditForm: FormGroup;
  student: any;

  constructor(@Inject(MAT_DIALOG_DATA) public StudentEditDialogData, public dialogRef: MatDialogRef<StudentEditDialogComponent>,private fb: FormBuilder) { 
    this.student = StudentEditDialogData.student;
  }

  ngOnInit(): void {
    this.studentEditForm = this.fb.group({
      email: ['',
        [Validators.required,
        Validators.email]
        ],  
      firstName: ['',
        [Validators.required]
      ],
      lastName: ['',
        [Validators.required]
      ],
      phoneNumber: ['',
        [Validators.minLength(9)]
      ]
    });
    this.studentEditForm.valueChanges.subscribe(console.log);
  }

  hide = true;

  get email(){
    return this.studentEditForm.get('email');
  }

  get firstName(){
    return this.studentEditForm.get('firstName');
  }

  get lastName(){
    return this.studentEditForm.get('lastName');
  }

  get phoneNumber(){
    return this.studentEditForm.get('phoneNumber');
  }

  submit(){
    this.dialogRef.close(this.studentEditForm.value);
  }

  close(){
    this.dialogRef.close();
  }

}
