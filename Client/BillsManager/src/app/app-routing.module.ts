import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BaseComponent } from './pages/base/base.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/BillsManager',
    pathMatch: 'full',
  },
  {
    path: 'BillsManager',
    component: BaseComponent,
  },
  {
    path: 'BillsManager/Auth',
    loadChildren: () =>
      import('./modules/auth/auth.module').then((module) => module.AuthModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
