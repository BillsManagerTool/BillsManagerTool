import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputFieldComponent } from './custom-controls/input-field/input-field.component';

@NgModule({
  declarations: [InputFieldComponent, InputFieldComponent],
  imports: [CommonModule],
  exports: [InputFieldComponent],
})
export class SharedModule {}