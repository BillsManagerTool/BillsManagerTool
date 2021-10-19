import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { AutoCompleteModule } from 'primeng/autocomplete';

@NgModule({
  declarations: [],
  imports: [
    FormsModule,
    CommonModule,
    InputTextModule,
    ReactiveFormsModule,
    AutoCompleteModule,
  ],
  exports: [
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    AutoCompleteModule,
  ],
})
export class SharedModule {}
