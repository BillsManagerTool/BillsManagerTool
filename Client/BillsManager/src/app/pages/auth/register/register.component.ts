import { ICountry } from './../../../interfaces/country';
import { TranslateService } from './../../../services/translate.service';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { MustMatch } from 'src/app/shared/utils/password-validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerHousekeeperForm: FormGroup;

  dataLocale: any;
  selectedCountry: string;
  selectedTown: string;
  countries: Array<ICountry>;
  towns: any;
  submitted = false;

  test: '';

  constructor(
    private authService: AuthService,
    private translateService: TranslateService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    let lang = localStorage.getItem('ui-lang');
    this.translateService.language = lang;

    this.registerHousekeeperForm = this.formBuilder.group(
      {
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
          Validators.minLength(4),
        ]),
        confirmPassword: new FormControl('', [
          Validators.required,
          Validators.minLength(4),
        ]),
        buildingAddress: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
        ]),
        entranceNumber: new FormControl('', Validators.required),
        apartmentNumber: new FormControl('', Validators.required),
        apartmentFloor: new FormControl('', Validators.required),
        town: new FormControl('', Validators.required),
        country: new FormControl('', Validators.required),
      },
      {
        validator: MustMatch('password', 'confirmPassword'),
      }
    );
    let data = this.translateService.translateLabels(lang);
    this.dataLocale = data.Auth.Register;

    this.translateService.getCountriesAsObservable().subscribe((res) => {
      this.countries = res;
    });
  }

  onSubmit() {
    this.submitted = true;
    this.authService.register(this.registerHousekeeperForm).subscribe((res) => {
      console.log(res);
    });
  }

  onChangeCountry(selectedCountry) {
    this.towns = new Array<any>();
    this.translateService.getCountriesAsObservable().subscribe((res) => {
      res.forEach((country) => {
        if (country.Country == selectedCountry) {
          this.towns = country.Towns.map((townName) => ({ name: townName }));
        }
      });
    });
  }

  get formControls() {
    return this.registerHousekeeperForm.controls;
  }
}
