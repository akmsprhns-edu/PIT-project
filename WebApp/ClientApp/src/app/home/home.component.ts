import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
  }

  public pingAuth(){
    this.httpClient.get<string>(environment.baseUrl + '/Api/App/AuthPing').subscribe(result => {
      console.log(result)
    }, error => {
      console.error(error);
    })
  }
}
