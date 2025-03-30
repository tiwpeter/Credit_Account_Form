import { Routes } from "@angular/router";
import { HomeComponent } from "./componet/home/home.component";
import { PostApiComponent } from "./componet/post-api/post-api.component";
import { ItemDetailComponent } from "./componet/item-detail/item-detail.component";
import { FormComponent } from "./componet/form/form.component";
import { UserDetailComponent } from "./user-detail/user-detail.component";

const routeConfig: Routes = [
    {
        path: 'rr',
        component: HomeComponent,
        title: 'Home Page'
    },
    {
        path: 'postget',
        component: PostApiComponent,
        title: 'PostApi'
  },
     {
        path: 'form',
        component: FormComponent,
        title: 'PostApi'
    },
  {
    path: 'user/:id', // 👈 เพิ่ม path สำหรับรายละเอียด
    component: UserDetailComponent, // หรือเปลี่ยนเป็นอีก component ก็ได้ เช่น ItemDetailComponent
    title: 'Item Detail'
  }
];

export default routeConfig;