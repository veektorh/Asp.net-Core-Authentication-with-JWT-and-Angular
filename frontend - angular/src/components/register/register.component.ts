import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { HttpService } from "src/services/http.service";


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  myForm : FormGroup;
  post : any;
  username : string ;
  password : string;
  error:string;
  success:string;

  constructor(private fb : FormBuilder,private http:HttpService) {
    this.myForm = fb.group({

      'Username' : [null, Validators.compose([Validators.required,Validators.email]) ],
      'Password' : [null, Validators.compose([Validators.required]) ],
      'comparepassword' : [null, Validators.compose([Validators.required]) ],
    });

   }

  ngOnInit() {
  }

  OnSubmit(form : FormGroup){

    if(!form.status){
      this.error = 'An error occured';
      return;
    }
    var data = new FormData();
    data.append('Username',form.value.Username);
    data.append('Password',form.value.Password);

    this.http.PostResource("/auth/register",data).subscribe(data=>{
      this.success = 'Saved Successfully!';
      return;
    },error=>{
      this.error = error.error.message;
      console.log(error.error);
      return;
    });
    
  }

}
