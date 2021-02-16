import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss']
})
export class StudentComponent implements OnInit {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  studentRegistrationForm: FormGroup;

  ngOnInit() {
    this.studentRegistrationForm = this.fb.group({
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
      password: ['',
        [Validators.required,
        //Validators.pattern("^(?=.[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+).{4}$"),
        Validators.pattern("^((.*[a-z].*)[0-9](.*[a-z]?.*))|((.*[a-z]?.*)[0-9](.*[a-z].*))$"),
        Validators.minLength(4)]
      ],
      phoneNumber: ['',
        [Validators.minLength(9)]
      ]
    });
    this.studentRegistrationForm.valueChanges.subscribe(console.log); 
  }

  hide = true;

  get email(){
    return this.studentRegistrationForm.get('email');
  }

  get password(){
    return this.studentRegistrationForm.get('password');
  }

  get firstName(){
    return this.studentRegistrationForm.get('firstName');
  }

  get lastName(){
    return this.studentRegistrationForm.get('lastName');
  }

  get phoneNumber(){
    return this.studentRegistrationForm.get('phoneNumber');
  }

  public submit(){
    const student = {
      "Email": this.email.value,
      "FirstName": this.firstName.value,
      "LastName": this.lastName.value,
      "Password": this.password.value,
      "PhoneNumber": this.phoneNumber.value
    }
    
    this.http.post("api/Student",student).subscribe(()=>{
      this.studentRegistrationForm.reset();
    });
  }
}
