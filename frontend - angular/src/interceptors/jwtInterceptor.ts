
    import { Injectable, Injector } from "@angular/core";
    import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse, HttpClient } from "@angular/common/http";
    import { HttpService } from "src/services/http.service";
    import { Observable, throwError, of } from "rxjs";
    import { HttpErrorResponse } from "@angular/common/http";
    import { Router } from "@angular/router";
    import { catchError } from "rxjs/operators";
    import { Token } from "src/models/Token";
    import { switchMap } from "rxjs/internal/operators/switchMap";
    import { ApiResponse } from "src/models/APiResponse";


    @Injectable()
    export class JwtInterceptor implements HttpInterceptor {

        constructor(private injector: Injector, private router: Router, private service: HttpService) { }


        intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

            return <any>next.handle(req).pipe(

                catchError(err => {

                    //intercept only 401 error - when access_token has expired
                    if (err.status == 401) {

                        let formData = new FormData();
                        let token = this.service.getToken();

                        formData.append('token', token.access_token);
                        formData.append('refreshToken', token.refresh_token);
                        formData.append('uniqueId', token.uniqueId);

                        //call for refresh token && when successful retry the request
                        return this.service.PostResource('/auth/refresh', formData).pipe(switchMap(result => {

                            if (result) {
                                let token: any = result;
                                localStorage.setItem('access_token', token.access_token);
                                localStorage.setItem('refresh_token', token.refresh_token);

                                let cloned = req.clone({
                                    headers: req.headers.set('Authorization', 'Bearer ' + token.access_token)
                                });

                                return next.handle(cloned);
                            }
                            
                            return throwError(err);

                        }))
                    } 
                    else {

                        this.router.navigateByUrl("/login");
                        return next.handle(req);
                    }
                }))
        }



    }