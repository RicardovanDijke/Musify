import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Router } from "@angular/router";

//import { UserService } from './user.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    BEARER_TOKEN = '';


    constructor(private http: HttpClient,
        private router: Router) { }

    login(username, password): Observable<any> {
        var jsonBody = {
            "username":username,
            "password":password
          }
        return this.http.post(`${environment.authService}/login`,
        jsonBody,
          {
            headers: new HttpHeaders()
              .set('Content-Type', 'application/json')
          })
    }
}
