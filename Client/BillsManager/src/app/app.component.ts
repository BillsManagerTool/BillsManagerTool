import { TranslateService } from './services/translate.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'BillsManager';

  constructor(private translateService: TranslateService) {}

  ngOnInit() {
    this.translateService.translate('en-US');
  }
}
