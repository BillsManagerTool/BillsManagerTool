import { RegisterRequest } from './../domain-models/registerRequest';
import { IBaseResponse } from './../interfaces/baseResponse';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticateRequest } from '../domain-models/authenticateRequest';
import { AuthenticateResponse } from '../domain-models/authenticateResponse';

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

  register(request: RegisterRequest): Observable<IBaseResponse> {
    return this.http.post<IBaseResponse>(
      `${this._BASE_URL}/auth/register`,
      request
    );
  }

  //https://restcountries.com/v3.1/name/united - search by partial name
  getCountries() {
    return this.http.get<any>(`https://countriesnow.space/api/v0.1/countries`);
  }
}
