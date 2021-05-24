import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { BaseResponse } from '../_models/base-response';
import { AdvertCategory } from '../_models/db_models';
import { NewAdvertModel } from '../_models/new-advert-model';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-create-advert',
  templateUrl: './create-advert.component.html',
  styleUrls: ['./create-advert.component.scss']
})
export class CreateAdvertComponent implements OnInit {

  public categories : AdvertCategory[];

  constructor(private httpClient: HttpClient, private router: Router) {

    this.httpClient.get<AdvertCategory[]>(environment.baseUrl + '/Api/Main/GetRootCategories').subscribe(result => {
      this.categories = result;
    }, error => {
      console.error(error)
    });
  }

  ngOnInit(): void {
  }
  private files: FileList

  public formError = "";
  public inProgress = false;
  public submitted = false;


  public form = new FormGroup({
    title: new FormControl('', [
      Validators.required
    ]),
    description: new FormControl('', [
      Validators.required
    ]),
    category: new FormControl('', [
      Validators.required
    ]),
    subCategory: new FormControl('', [
      Validators.required
    ]),
    price: new FormControl('', [
      Validators.required
    ])
  });

  get subCategories() {
    let currentCategory:AdvertCategory = this.form.value.category
    if(currentCategory){
      return currentCategory.subCategories
    }
    return null;
  }

  get selectedFiles() : string {
    if(!this.files || this.files.length < 1){
      return ""
    } else {
      return Array.from(this.files).map(x => x.name).join("; ")
    }
  }

  selectFormControl = new FormControl('', Validators.required);

  public matcher = new MyErrorStateMatcher();

  public submit() {
    if(!this.form.valid){
      return;
    }else if(this.files.length > 10){
      this.formError = "Atļauts pievienot ne vairāk ka 10 attēlus"
      return;
    } else {
      let data = new FormData();
      data.append('title', this.form.value.title);
      data.append('description', this.form.value.description);
      data.append('categoryId', (<AdvertCategory>this.form.value.subCategory).id.toString());
      data.append('price', this.form.value.price);
      if(this.files){
        for(let i = 0;i<this.files.length;i++){
          data.append('images', this.files[i], this.files[i].name)
        }
      }
      this.inProgress = true;
      this.httpClient.post<BaseResponse>(environment.baseUrl + '/Api/Main/CreateAdvert', data).subscribe(result => {
        if(!result.success){
          this.formError = result.error;
        } else {
          //this.router.navigateByUrl('/')
          this.submitted = true;
        }
        this.inProgress = false;
      }, error => {
        console.error(error);
        this.inProgress = false;
      })
    }
  }





  onFileChange(event) {
    this.files = event.target.files
  }

}
