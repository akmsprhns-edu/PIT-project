import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { BaseResponse } from '../_models/base-response';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {


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
    ]),
    repeatPassword: new FormControl('', [
      Validators.required
    ])

  });

  public matcher = new MyErrorStateMatcher();

  public submit() {
    if(!this.form.valid){
      return;
    } else {
      let creds = <RegisterCredentials> this.form.value ;
      if(creds.password != creds.repeatPassword){
        this.formError = "Paroles nesakrīt!"
        return;
      }
      this.inProgress = true;
      this.httpClient.post<BaseResponse>(environment.baseUrl + '/Api/Auth/Register', creds).subscribe(result => {
        if(!result.success){
          this.formError = result.error;
        } else {
          this.router.navigateByUrl('/Login')
        }
        this.inProgress = false;
      }, error => {
        console.error(error);
        this.inProgress = false;
      })
    }
  }

}

class RegisterCredentials {
  email: string;
  password: string;
  repeatPassword: string;
}


