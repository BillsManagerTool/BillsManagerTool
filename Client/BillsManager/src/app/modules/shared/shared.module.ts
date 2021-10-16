import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputFieldComponent } from './custom-controls/input-field/input-field.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { CustomButtonComponent } from './../shared/custom-controls/custom-button/custom-button.component';

@NgModule({
  declarations: [InputFieldComponent, CustomButtonComponent],
  imports: [FormsModule, CommonModule, InputTextModule, ReactiveFormsModule],
  exports: [
    InputFieldComponent,
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    CustomButtonComponent,
  ],
})
export class SharedModule {}
