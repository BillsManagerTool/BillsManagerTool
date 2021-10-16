import { JsonpClientBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import enUS from '../../localization/en-US.json';

@Injectable({
  providedIn: 'root',
})
export class TranslateService {
  myData: any;

  constructor(private http: HttpClient) {}

  translate(lenguage: string) {
    if (lenguage === 'en-US') {
      console.log(enUS);
    }
  }
}
