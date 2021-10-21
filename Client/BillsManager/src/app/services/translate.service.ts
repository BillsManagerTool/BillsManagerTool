import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import enUS from '../../localization/en-US.json';
import bgBG from '../../localization/bg-BG.json';
import countriesBG from '../../assets/countries-bg.json';
import countriesEN from '../../assets/countries-en.json';
import { Observable } from 'rxjs';
import { ICountry } from '../interfaces/country';
import { reduce } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TranslateService {
  language: string;

  constructor(private http: HttpClient) {}

  getCountriesAsObservable(): Observable<ICountry[]> {
    if (this.language === 'en-US') {
      return this.http.get<ICountry[]>('../../assets/countries-en.json');
    } else {
      return this.http.get<ICountry[]>('../../assets/countries-bg.json');
    }
  }

  getTownsAsObservable(): any {
    if (this.language === 'en-US') {
      let res = this.http.get<any>('../../assets/countries-en.json');

      console.log(res);
      return res;
    } else {
      return this.http.get<any>('../../assets/countries-bg.json');
    }
  }

  getLanguage(): void {
    let lg = localStorage.getItem('ui-lang');
    this.language = lg;
  }

  translateLabels(language: string): any {
    if (language === 'en-US') {
      return enUS;
    } else {
      return bgBG;
    }
  }

  translateCountries(language: string): any {
    if (language === 'en-US') {
      return countriesEN;
    } else {
      return countriesBG;
    }
  }
}
