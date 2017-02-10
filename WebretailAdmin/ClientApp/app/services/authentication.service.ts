import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { Token, Login } from '../shared/models';
import { Helpers } from '../shared/helpers';

@Injectable()
export class AuthenticationService {

  constructor(private _router: Router, private _http: Http) { }

  login(user: Login) : Observable<Token> {
    return this._http.post('/api/session/login', JSON.stringify(user), { headers: Helpers.getHeaders() })
  		.map(response => <Token>response.json());
  }

  logout() {
  	let tokenModel = localStorage.getItem('token');
  	this._http.post('/api/session/logout', tokenModel, { headers: Helpers.getHeaders() })
  		.map((response) => response.json())
      .subscribe(result => {
        console.log(result);
      });
	  this.removeCredentials();
  }

  isAuthenticated() : boolean {
  	return localStorage.getItem('token') !== null;
  }

  getEmail() : string {
  	return localStorage.getItem('email');
  }

  grantCredentials(token: Token, email: string) {
    localStorage.setItem('token', JSON.stringify(token));
    localStorage.setItem('email', email);
    this._router.navigate(['home']);
  }

  removeCredentials() {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    this._router.navigate(['login']);
  }

  checkCredentials() {
    if (!this.isAuthenticated()) {
      this._router.navigate(['login']);
    }
  }
}
