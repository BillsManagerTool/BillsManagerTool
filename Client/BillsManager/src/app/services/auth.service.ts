import { IAuthenticateResponse } from './../interfaces/authenticateResponse';
import { IAuthenticateRequest } from './../interfaces/authenticateRequest';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

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
    request: IAuthenticateRequest
  ): Observable<IAuthenticateResponse> {
    return this.http.post<IAuthenticateResponse>(
      `${this._BASE_URL}/auth/authenticate`,
      request
    );
  }
}
