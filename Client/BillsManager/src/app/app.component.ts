import { RegisterRequest } from './domain-models/registerRequest';
import { IBaseResponse } from './interfaces/baseResponse';
import { AuthService } from './services/auth.service';
import { Component, OnInit } from '@angular/core';
import { AuthenticateResponse } from './domain-models/authenticateResponse';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'BillsManager';

  constructor(private authService: AuthService) {}

  ngOnInit() {
    let registerRequest = new RegisterRequest();
    registerRequest.Email = 'cholaka@gmail.com';
    registerRequest.Password = 'cholaka1234';
    registerRequest.BuildingAddress = 'Uoashbarn 61';
    registerRequest.TownId = 1;
    registerRequest.CountryId = 1;
    registerRequest.EntranceNumber = 'A';
    registerRequest.ApartmentNumber = '2C';
    registerRequest.ApartmentFloor = 2;

    this.authService
      .register(registerRequest)
      .subscribe((response: IBaseResponse) => {
        console.log(response);
      });

    setTimeout(() => 3000);

    let email = 'cholaka@gmail.com';
    let password = 'cholaka1234';

    this.authService
      .authenticate(email, password)
      .subscribe((response: AuthenticateResponse) => {
        console.log(response);
      });
  }
}
