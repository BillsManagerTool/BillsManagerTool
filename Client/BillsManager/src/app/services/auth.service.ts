import { AuthenticateResponse } from './../models/authenticateResponse';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticateRequest } from '../models/authenticateRequest';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _BASE_URL = environment.baseUrl;

  constructor(private http: HttpClient) {}

  private UrlBuilder(endpoint: string, params?: object): string {
    const queryParams = params
      ? Object.keys(params)
          .map((key) => key + '=' + params[key])
          .join('&')
      : '';
    return `${this._BASE_URL}${endpoint}?${queryParams}`;
  }

  authenticate(
    email: string,
    password: string
  ): Observable<AuthenticateResponse> {
    let request = new AuthenticateRequest();
    request.Email = email;
    request.Password = password;

    return this.http.post<AuthenticateResponse>(
      `${this._BASE_URL}/auth/authenticate`,
      request
    );
  }

  // register(email: string, password: string): Observable<IAuthenticateResponse> {
  //   let request = new AuthenticateRequest();
  //   request.Email = email;
  //   request.Password = password;

  //   return this.http.post<IAuthenticateResponse>(
  //     `${this._BASE_URL}/auth/authenticate`,
  //     request
  //   );
  // }
}
