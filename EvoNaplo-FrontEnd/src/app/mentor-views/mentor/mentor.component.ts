import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-mentor',
  templateUrl: './mentor.component.html',
  styleUrls: ['./mentor.component.scss']
})
export class MentorComponent implements OnInit {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  mentorRegistrationForm: FormGroup;

  ngOnInit(): void {
    this.mentorRegistrationForm = this.fb.group({
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
        Validators.pattern("^((.*[a-z].*)[0-9](.*[a-z]?.*))|((.*[a-z]?.*)[0-9](.*[a-z].*))$"),
        Validators.minLength(4)]
      ],
      phoneNumber: ['',
        [Validators.minLength(9)]
      ]
    });
    this.mentorRegistrationForm.valueChanges.subscribe(console.log);
  }

  hide = true;

  get email(){
    return this.mentorRegistrationForm.get('email');
  }

  get password(){
    return this.mentorRegistrationForm.get('password');
  }

  get firstName(){
    return this.mentorRegistrationForm.get('firstName');
  }

  get lastName(){
    return this.mentorRegistrationForm.get('lastName');
  }

  get phoneNumber(){
    return this.mentorRegistrationForm.get('phoneNumber');
  }

  public submit(){
    const mentor = {
      "Email": this.email.value,
      "FirstName": this.firstName.value,
      "LastName": this.lastName.value,
      "Password": this.password.value,
      "PhoneNumber": this.phoneNumber.value
    }
    
    this.http.post("api/Mentor", mentor).subscribe(()=>{
      this.mentorRegistrationForm.reset();
    });
  }
}