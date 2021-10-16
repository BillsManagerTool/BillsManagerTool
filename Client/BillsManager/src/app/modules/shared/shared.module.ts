import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputFieldComponent } from './custom-controls/input-field/input-field.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  declarations: [InputFieldComponent, InputFieldComponent],
  imports: [FormsModule, CommonModule, InputTextModule, ReactiveFormsModule],
  exports: [
    InputFieldComponent,
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
  ],
})
export class SharedModule {}
