import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email: new FormControl(Validators.required, Validators.email),
    password: new FormControl(Validators.required, [
      Validators.maxLength(12),
      Validators.minLength(6),
    ]),
  });

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}
}
