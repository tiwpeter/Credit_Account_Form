import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { ItemDetailComponent } from './item-detail/item-detail.component';  // Import your component

// Declare routes
const routes: Routes = [
  { path: 'item/:id', component: ItemDetailComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],  // Configure the router with the routes
  exports: [RouterModule]  // Export RouterModule to make routing available throughout the app
})
export class AppRoutingModule { }
