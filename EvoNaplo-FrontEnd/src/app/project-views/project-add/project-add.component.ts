import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-project-add',
  templateUrl: './project-add.component.html',
  styleUrls: ['./project-add.component.scss']
})

export class ProjectAddComponent implements OnInit {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  projectCreationForm: FormGroup;


  ngOnInit(): void {
    this.projectCreationForm = this.fb.group({
      projectName: ['',[
        Validators.required,
      ]],
      sourceLink: ['',[
        Validators.required
      ]],
      usedTechnologies: ['', [
        Validators.required
      ]],
      semesterId: [null, [
        Validators.required
      ]]
    });

    this.projectCreationForm.valueChanges.subscribe(console.log);
  }

  get projectName(){
    return this.projectCreationForm.get('projectName');
  }

  get sourceLink(){
    return this.projectCreationForm.get('sourceLink');
  }

  get usedTechnologies(){
    return this.projectCreationForm.get('usedTechnologies');
  }

  get semesterId(){
    return this.projectCreationForm.get('semesterId');
  }

  public submit(){
    const project = {
      "ProjectName" : this.projectName.value,
      'SourceLink' : this.sourceLink.value,
      "UsedTechnologies": this.usedTechnologies.value,
      'SemesterId': this.semesterId.value
    }

    this.http.post("api/Project", project).subscribe(()=>{
      this.projectCreationForm.reset();
    });
  }
}