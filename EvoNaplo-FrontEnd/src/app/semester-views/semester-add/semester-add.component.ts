import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-semester-add',
  templateUrl: './semester-add.component.html',
  styleUrls: ['./semester-add.component.scss']
})
export class SemesterAddComponent implements OnInit {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  semesterCreationForm: FormGroup;

  ngOnInit(): void {
    this.semesterCreationForm = this.fb.group({
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

    this.semesterCreationForm.valueChanges.subscribe(console.log);
  }

  get startDate(){
    return this.semesterCreationForm.get('startDate');
  }

  get endDate(){
    return this.semesterCreationForm.get('endDate');
  }

  get demoDate(){
    return this.semesterCreationForm.get('demoDate');
  }

  public submit(){
    const semester = {
      "startDate" : this.formatDate(this.startDate.value),
      "endDate" : this.formatDate(this.endDate.value),
      "demoDate" : this.formatDate(this.demoDate.value)
    }

    this.http.post("api/semester", semester).subscribe(()=>{
      this.semesterCreationForm.reset();
    });
  }

  private addLeadingZeros(value: number): string {
    return ('0' + value).slice(-2);
  }
 
  private formatDate(date: Date): string {
    return `${date.getFullYear()}-${this.addLeadingZeros(date.getMonth() + 1)}-${this.addLeadingZeros(date.getDate())}`;
  }
}
