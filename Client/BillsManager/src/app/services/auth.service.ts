import { RegisterRequest } from './../domain-models/registerRequest';
import { IBaseResponse } from './../interfaces/baseResponse';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticateRequest } from '../domain-models/authenticateRequest';
import { AuthenticateResponse } from '../domain-models/authenticateResponse';
import { FormGroup } from '@angular/forms';

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

  comparePasswords(fb: FormGroup) {
    let confirmPasswordControl = fb.get('confirmPassword');
    console.log(confirmPasswordControl);
    if (
      confirmPasswordControl.errors == null ||
      'passwordMismatch' in confirmPasswordControl.errors
    ) {
      if (fb.get('password').value != confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({
          passwordMismatch: true,
        });
      } else {
        confirmPasswordControl.setErrors(null);
      }
    }
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

  register(requestFormGroup: FormGroup): Observable<IBaseResponse> {
    let request = {
      FirstName: requestFormGroup.value.firstName,
      LastName: requestFormGroup.value.lastName,
      Email: requestFormGroup.value.email,
      Password: requestFormGroup.value.password,
      Country: requestFormGroup.value.country.Country,
      Town: requestFormGroup.value.town.name,
    };
    console.log(request);
    return this.http.post<IBaseResponse>(
      `${this._BASE_URL}/auth/register`,
      request
    );
  }
}
