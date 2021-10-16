import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.scss'],
})
export class InputFieldComponent implements OnInit {
  @Input() label: string;
  @Input() formGroupName: FormGroup;
  @Input() controlName: FormControl;
  constructor() {}

  ngOnInit(): void {}
}
