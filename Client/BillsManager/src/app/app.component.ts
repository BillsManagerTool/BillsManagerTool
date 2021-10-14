import { AuthService } from './services/auth.service';
import { Component, OnInit } from '@angular/core';
import { AuthenticateResponse } from './models/authenticateResponse';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'BillsManager';

  constructor(private authService: AuthService) {}

  ngOnInit() {
    let email = 'cholaka@gmail.com';
    let password = 'cholaka1234';

    this.authService
      .authenticate(email, password)
      .subscribe((response: AuthenticateResponse) => {
        console.log(response);
      });
  }
}
