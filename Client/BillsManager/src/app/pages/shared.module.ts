import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DropdownModule } from 'primeng/dropdown';
import { TooltipModule } from 'primeng/tooltip';

@NgModule({
  declarations: [],
  imports: [
    FormsModule,
    CommonModule,
    InputTextModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    DropdownModule,
    TooltipModule,
  ],
  exports: [
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    DropdownModule,
    TooltipModule,
  ],
})
export class SharedModule {}
