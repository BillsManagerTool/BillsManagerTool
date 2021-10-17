import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  declarations: [],
  imports: [
    FormsModule,
    CommonModule,
    InputTextModule,
    ReactiveFormsModule,
    DropdownModule,
  ],
  exports: [FormsModule, InputTextModule, ReactiveFormsModule, DropdownModule],
})
export class SharedModule {}
