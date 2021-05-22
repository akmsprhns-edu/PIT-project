import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { BaseResponse } from '../common';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
  }

  public formError = "";
  public inProgress = false;

  public form = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email,
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8)
    ])

  });

  public matcher = new MyErrorStateMatcher();

  public submit() {
    if(!this.form.valid){
      return;
    } else {
      let creds = <LoginCredentials> this.form.value ;
      
      this.inProgress = true;
      this.httpClient.post<BaseResponse>(environment.baseUrl + '/Api/Auth/Login', creds).subscribe(result => {
        if(!result.success){
          this.formError = result.error;
        } else {
          this.router.navigateByUrl('/')
        }
        this.inProgress = false;
      }, error => {
        console.error(error);
        this.inProgress = false;
      })
    }
  }
}

class LoginCredentials {
  email: string;
  password: string;
  repeatPassword: string;
}