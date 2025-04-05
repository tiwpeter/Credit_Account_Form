import { Routes } from '@angular/router';
import { TestComponent } from './componet/test/test.component';
import { DowndloadComponent } from './componet/downdload/downdload.component';

export const routes: Routes = [
    {
        path: 'testRoute/:id',  // รับค่า id จาก URL
        component: TestComponent,
        title: 'Home Page'
    },{
        path: 'Download/:id',  // รับค่า id จาก URL
        component: DowndloadComponent,
        title: 'Home Page'
    }
];
