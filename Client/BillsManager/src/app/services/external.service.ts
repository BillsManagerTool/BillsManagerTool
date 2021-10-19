import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ExternalService {
  constructor(private http: HttpClient) {}

  getCountries() {
    return this.http
      .get<any>(`https://countriesnow.space/api/v0.1/countries`)
      .toPromise()
      .then((res) => <any[]>res.data)
      .then((data) => {
        return data;
      });
  }
}
