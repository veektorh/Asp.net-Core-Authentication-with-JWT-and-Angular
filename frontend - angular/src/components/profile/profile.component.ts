import { Component, OnInit } from '@angular/core';
import { HttpService } from "src/services/http.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  studentArray:any;

  constructor(private service: HttpService) { }

  ngOnInit() {
    this.service.GetResource('/values').subscribe(res=>{
      
      this.studentArray = res;

    },error=>{

      console.log(error);
    })
  }

}
