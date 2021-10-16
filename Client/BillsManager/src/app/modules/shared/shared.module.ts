import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';

@NgModule({
  declarations: [],
  imports: [CommonModule, MatCheckboxModule, MatChipsModule],
  exports: [MatCheckboxModule, MatChipsModule],
})
export class SharedModule {}
