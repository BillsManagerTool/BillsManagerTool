import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import enUS from '../../localization/en-US.json';
import bgBG from '../../localization/bg-BG.json';
import countriesBG from '../../assets/countries-bg.json';
import countriesEN from '../../assets/countries-en.json';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TranslateService {
  language: string;

  constructor(private http: HttpClient) {}

  searchCountries = async (searchText) => {
    let response = new Response();

    if (this.language === 'en-US') {
      response = await fetch('../../assets/countries-en.json');
    } else {
      response = await fetch('../../assets/countries-bg.json');
    }
    const countries = await response.json();
    let matches = countries.filter((country) => {
      const regex = new RegExp(`^${searchText}`, 'gi');
      return country.Country.match(regex);
    });
    console.log(matches);
  };

  getCountriesAsObservable(): Observable<any> {
    if (this.language === 'en-US') {
      return this.http.get<any>('../../assets/countries-en.json');
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
