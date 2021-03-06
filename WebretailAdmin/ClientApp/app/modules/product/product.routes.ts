import { Routes, RouterModule } from '@angular/router';

import { ProductsComponent } from './components/products.component';
import { ProductComponent } from './components/product.component';

const routes: Routes = [
    {
        path: '',
        children: [
            { path: '', component: ProductsComponent },
            { path: ':id', component: ProductComponent }
        ]
    }
];

export const ProductRoutes = RouterModule.forChild(routes);