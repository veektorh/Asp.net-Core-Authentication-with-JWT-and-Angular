
import { Injectable } from "@angular/core";
import { HttpRequest,HttpHandler,HttpEvent,HttpInterceptor,HttpResponse , HttpErrorResponse} from "@angular/common/http";
import { HttpService } from "src/services/http.service";
import { Observable,throwError } from "rxjs";
import { map,catchError } from "rxjs/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor{

    
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // return next.handle(req);
        let token = localStorage.getItem('access_token');
        if(token){
            let cloned = req.clone({
                headers: req.headers.set('Authorization','Bearer '+token)
            });

            return next.handle(cloned);
        }else{
            return next.handle(req);
        }
    }

    
}