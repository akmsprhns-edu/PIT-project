import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AdvertFullInfo } from '../_models/advert-full-info';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  private id: number
  public advert: AdvertFullInfo;
  
  constructor(private httpClient: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = +this.route.snapshot.paramMap.get('id')

    this.httpClient.get<AdvertFullInfo>(environment.baseUrl + `/Api/Main/GetAdvertInfo/${this.id}`).subscribe(result => {
      this.advert = result;
    }, error => {
      console.error(error)
    });
  }

  getFullUrl(relativePath: string){
    return environment.baseUrl + relativePath
  }


}
