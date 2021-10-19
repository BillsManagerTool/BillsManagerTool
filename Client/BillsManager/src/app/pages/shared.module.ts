import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  declarations: [],
  imports: [
    FormsModule,
    CommonModule,
    InputTextModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    DropdownModule,
  ],
  exports: [
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    DropdownModule,
  ],
})
export class SharedModule {}
