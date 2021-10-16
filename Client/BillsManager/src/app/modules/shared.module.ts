import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  declarations: [],
  imports: [FormsModule, CommonModule, InputTextModule, ReactiveFormsModule],
  exports: [FormsModule, InputTextModule, ReactiveFormsModule],
})
export class SharedModule {}
