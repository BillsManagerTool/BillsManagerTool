import { JsonpClientBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import enUS from '../../localization/en-US.json';
import bgBG from '../../localization/bg-BG.json';

@Injectable({
  providedIn: 'root',
})
export class TranslateService {
  constructor(private http: HttpClient) {}

  translate(language: string) {
    if (language === 'en-US') {
      return enUS;
    } else {
      return bgBG;
    }
  }
}
