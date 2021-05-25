import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { VirtualTimeScheduler } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AdvertShortInfo } from '../_models/advert-short-info';
import { AdvertCategory } from '../_models/db_models';

@Component({
  selector: 'app-search-category',
  templateUrl: './search-category.component.html',
  styleUrls: ['./search-category.component.scss']
})
export class SearchCategoryComponent implements OnInit {


  private id: number

  public adverts: AdvertShortInfo[];
  public category: AdvertCategory;
  public subCategory = new FormControl();
  public sortOrder = new FormControl();
  public loading = true;

  constructor(private httpClient: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = +this.route.snapshot.paramMap.get('id')

    this.httpClient.get<AdvertCategory[]>(environment.baseUrl + '/Api/Main/GetRootCategories').subscribe(result => {
      this.category = result.find(x => x.id === this.id );
    }, error => {
      console.error(error)
    });

    this.loadData();
  }

  public loadData(){
    this.adverts = undefined
    let requestData = {
      categoryId: this.subCategory.value?.id || this.id,
      sortOrder: this.sortOrder.value
    }
    this.httpClient.post<AdvertShortInfo[]>(environment.baseUrl + '/Api/Main/GetAdverts',requestData).subscribe(result => {
      this.adverts = result;
      this.loading = false;
    }, error => {
      console.error(error)
    });
  }

  get subCategories() {
    return this.category.subCategories
  }

  getFullUrl(relativePath: string){
    return environment.baseUrl + relativePath
  }

}
