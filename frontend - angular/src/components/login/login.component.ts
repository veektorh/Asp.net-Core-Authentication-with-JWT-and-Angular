import { Component, OnInit } from '@angular/core';  
import { FormBuilder , FormGroup , Validators} from "@angular/forms";
import { HttpService } from "src/services/http.service";
import { ApiResponse } from "src/models/APiResponse";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})



export class LoginComponent implements OnInit {

  myForm : FormGroup;
  error : string;
  success : string ;
  Username : string;
  Password : string;

  constructor(private fb:FormBuilder, private http: HttpService) {

    this.myForm = fb.group({
      
            'Username' : [null, Validators.compose([Validators.required,Validators.email]) ],
            'Password' : [null, Validators.compose([Validators.required]) ]            
          });

   }

  ngOnInit() {
  }

  OnSubmit(form:FormGroup){
    if(!form.status){
      this.error = "An Error Occured";
      return
    }

    var formData = new FormData();
    
    formData.append('Username', form.value.Username);
    formData.append('Password', form.value.Password);
    formData.append('UniqueId', this.http.getUniqueId());
    

    this.http.PostResource("/auth/login", formData).subscribe(data=>{
      this.http.setToken(data);  
      this.success ="Logged In Successfully"    
    }, error=>{
      this.error = (error.error.message);
    });

  }

  

}
