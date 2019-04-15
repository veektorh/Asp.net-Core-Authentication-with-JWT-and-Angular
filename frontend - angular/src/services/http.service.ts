import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { HttpHeaders } from "@angular/common/http";
import { Token } from "src/models/Token";
import { ApiResponse } from "src/models/APiResponse";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  url: string = "http://localhost:55309/api";//http://localhost:55309/api/values/1
  headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf8');
  //headers = new HttpHeaders().set('Content-Type','application/x-www-form-urlencoded');

  PostResource(path: string, form: FormData) {
    return this.http.post(this.url + path, form)

  }
  GetResource(path: string) {
    return this.http.get(this.url + path)

  }

  getToken() {
    let token  = new Token();

    token.access_token = localStorage.getItem('access_token');
    token.refresh_token = localStorage.getItem('refresh_token');
    token.uniqueId = localStorage.getItem('uniqueId');
    token.userId = localStorage.getItem('userId');
    return token;
  }

  setToken(data) {
    localStorage.setItem('access_token', data.access_token);
    localStorage.setItem('refresh_token', data.refresh_token);
    localStorage.setItem('uniqueId', data.uid);
    localStorage.setItem('userId', data.userId);

  }

  getUniqueId(){
    
    var UniqueId = localStorage.getItem('uniqueId');
    
    if(UniqueId == "undefined" || UniqueId == "null" || UniqueId == null || UniqueId == undefined){
      return '';
    }

    return UniqueId;
  }

}
