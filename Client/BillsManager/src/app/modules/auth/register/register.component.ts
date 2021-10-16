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
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl(Validators.required, [
      Validators.maxLength(12),
      Validators.minLength(6),
    ]),
    buildingAddress: new FormControl(Validators.required),
    entranceNumber: new FormControl(Validators.required),
    apartmentNumber: new FormControl(Validators.required),
    apartmentFloor: new FormControl(Validators.required),
    townId: new FormControl(Validators.required),
    countryId: new FormControl(Validators.required),
  });

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.registerHousekeeperForm.value);
  }
}
