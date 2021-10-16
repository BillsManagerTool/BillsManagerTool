import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit {
  lenguage: string;
  constructor() {}

  ngOnInit(): void {}

  getLenguage(lenguage: string) {
    console.log(lenguage);
  }
}
