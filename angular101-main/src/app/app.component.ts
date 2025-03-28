import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './componet/nav/nav.component';
import { DetailComponent } from './componet/detail/detail.component';
import { GetApiComponent } from './componet/get-api/get-api.component';
import { ConnectCComponent } from './componet/connect-c/connect-c.component';
import { FormsModule } from '@angular/forms'; // 👈 ต้อง import ตรงนี้
import { PostApiComponent } from './componet/post-api/post-api.component';


@Component({
  selector: 'app-root',
  imports: [NavComponent,GetApiComponent,ConnectCComponent,FormsModule,PostApiComponent],
  template: '<app-nav></app-nav><app-get-api></app-get-api><app-post-api></app-post-api>',

  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'my-project';
}
