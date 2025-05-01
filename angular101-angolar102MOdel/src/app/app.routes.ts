import { Routes } from '@angular/router';
import { DowndloadComponent } from './componet/downdload/downdload.component';
import { CustomerComponent } from './componet/getcustomer/customer.component';
import { FormComponent } from './componet/form/form.component';
import { GetbyidComponent } from './componet/getbyid/getbyid.component';

export const routes: Routes = [
    {
        path: 'Download/:id',  // รับค่า id จาก URL
        component: DowndloadComponent,
        title: 'Home Page'
    },{
        path: '',  // รับค่า id จาก URL
        component: CustomerComponent,
        title: 'Home Page'
    }   ,{
        path: 'form/:id',  // รับค่า id จาก URL
        component: FormComponent,
        title: 'Home Page'
    },
    {
        path: 'Customer/:id',  // รับค่า id จาก URL
        component: GetbyidComponent,
        title: 'Home Page'
    }
];
