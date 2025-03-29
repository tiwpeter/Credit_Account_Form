import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { RoComponent } from './componet/ro/ro.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: '/test',  // Redirect to the 'test' route
        pathMatch: 'full'
    },
    {
        path: 'test', 
        component: RoComponent,
    }
];
