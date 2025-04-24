import { Routes } from '@angular/router';
import { ProductFormComponent } from './products/components/product-form/product-form.component';

export const routes: Routes = [
  { path: '', redirectTo: '/productos', pathMatch: 'full' },
  {
    path: 'productos',
    loadComponent: () =>
      import('./products/components/product-list/product-list.component').then(
        (c) => c.ProductListComponent,
      ),
  },
  { path: 'productos/nuevo', component: ProductFormComponent },
  { path: 'productos/:id', component: ProductFormComponent },
];
