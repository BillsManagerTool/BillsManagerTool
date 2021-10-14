import { AuthService } from './services/auth.service';
import { Component, OnInit } from '@angular/core';
import { IAuthenticateResponse } from './interfaces/authenticateResponse';
import { AuthenticateRequest } from './models/authenticateRequest';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'BillsManager';

  constructor(private authService: AuthService) {}

  ngOnInit() {
    let request = new AuthenticateRequest();
    request.Password = 'Pesho';
    request.Email = 'Pesho';

    console.log(request);
    this.authService
      .authenticate(request)
      .subscribe((response: IAuthenticateResponse) => {
        console.log(response);
      });
  }
}
