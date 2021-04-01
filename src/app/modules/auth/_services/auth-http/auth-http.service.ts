import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserModel } from '../../_models/user.model';
import { environment } from '../../../../../environments/environment';
import { AuthModel } from '../../_models/auth.model';
import { LoginModel } from '../../_models/login.model';

const API_USERS_URL = `${environment.apiUrl}/Login/SignIn`;

@Injectable({
  providedIn: 'root',
})
export class AuthHTTPService {
  constructor(private http: HttpClient) { }

  // public methods
  login(email: string, password: string): Observable<any> {
    let loginModel: LoginModel = {
      grantType: 'password',
      userName: email,
      password: password,
    };
    return this.http.post<AuthModel>(API_USERS_URL, loginModel);
  }

  // CREATE =>  POST: add a new user to the server
  createUser(user: UserModel): Observable<UserModel> {
    return this.http.post<UserModel>(API_USERS_URL, user);
  }

  // Your server should check email => If email exists send link to the user and return true | If email doesn't exist return false
  forgotPassword(email: string): Observable<boolean> {
    // return this.http.post<boolean>(`${API_USERS_URL}/forgot-password`, {
    //   email,
    // });
    return of(true);
  }

  getUserByToken(token): Observable<UserModel> {
    const httpHeaders = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.http.get<UserModel>(`${API_USERS_URL}`, {
      headers: httpHeaders,
    });
  }
}
