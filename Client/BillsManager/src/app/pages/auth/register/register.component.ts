import { ICountry } from './../../../interfaces/country';
import { TranslateService } from './../../../services/translate.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerHousekeeperForm = new FormGroup({
    firstName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(12),
    ]),
    confirmPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(12),
    ]),
    buildingAddress: new FormControl(Validators.required),
    entranceNumber: new FormControl(Validators.required),
    apartmentNumber: new FormControl(Validators.required),
    apartmentFloor: new FormControl(Validators.required),
    town: new FormControl('', Validators.required),
    country: new FormControl('', Validators.required),
  });

  dataLocale: any;
  searchCountry: '';
  searchTown: '';

  countries$: Observable<ICountry[]>;
  towns: any;

  constructor(
    private authService: AuthService,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    let lang = localStorage.getItem('ui-lang');
    this.translateService.language = lang;
    this.countries$ = this.translateService.getCountriesAsObservable();

    this.translateService.getCountriesAsObservable().subscribe((res) => {
      // console.log(res);
      this.towns = res;
    });

    let data = this.translateService.translateLabels(lang);
    this.dataLocale = data.Auth.Register;

    this.translateService.getTownsAsObservable();
  }

  onSubmit() {
    console.log(this.registerHousekeeperForm.value);
  }
}
