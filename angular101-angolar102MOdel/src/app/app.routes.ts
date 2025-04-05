import { Routes } from '@angular/router';
import { TestComponent } from './componet/test/test.component';
import { DowndloadComponent } from './componet/downdload/downdload.component';
import { CustomerComponent } from './componet/getcustomer/customer.component';
import { FormComponent } from './componet/form/form.component';

export const routes: Routes = [
    {
        path: 'testRoute/:id',  // รับค่า id จาก URL
        component: TestComponent,
        title: 'Home Page'
    },{
        path: 'Download/:id',  // รับค่า id จาก URL
        component: DowndloadComponent,
        title: 'Home Page'
    },{
        path: 'getCustomer',  // รับค่า id จาก URL
        component: CustomerComponent,
        title: 'Home Page'
    }   ,{
        path: 'form',  // รับค่า id จาก URL
        component: FormComponent,
        title: 'Home Page'
    }
];
