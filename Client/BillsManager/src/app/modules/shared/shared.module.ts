import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputFieldComponent } from './custom-controls/input-field/input-field.component';
import { CheckboxModule } from 'primeng/checkbox';

@NgModule({
  declarations: [InputFieldComponent, InputFieldComponent],
  imports: [CommonModule, CheckboxModule],
  exports: [InputFieldComponent, CheckboxModule],
})
export class SharedModule {}
