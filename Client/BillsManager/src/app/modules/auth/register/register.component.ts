import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

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
    password: new FormControl(Validators.required, [
      Validators.minLength(6),
      Validators.maxLength(12),
    ]),
    buildingAddress: new FormControl(Validators.required),
    entranceNumber: new FormControl(Validators.required),
    apartmentNumber: new FormControl(Validators.required),
    apartmentFloor: new FormControl(Validators.required),
    town: new FormControl(Validators.required),
    country: new FormControl(Validators.required),
  });

  btnConfig = {
    styles: {
      position: 'relative',
      width: '200px',
      height: '40px',
      fontSize: '16px',
      borderRadius: '5px',
      backgroundColor: '#00a9d6',
      fontFamily: 'sans-serif',
      marginTop: '30px',
    },
    text: 'Register',
  };

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.registerHousekeeperForm.value);
  }
}
