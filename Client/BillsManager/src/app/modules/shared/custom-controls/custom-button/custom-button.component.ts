import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'custom-button',
  templateUrl: './custom-button.component.html',
  styleUrls: ['./custom-button.component.scss'],
})
export class CustomButtonComponent implements OnInit {
  @Input() disabled: boolean = false;
  @Input() buttonText: string;
  constructor() {}

  ngOnInit(): void {}
}
