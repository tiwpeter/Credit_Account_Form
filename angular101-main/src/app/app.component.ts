import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './componet/nav/nav.component';
import { DetailComponent } from './componet/detail/detail.component';
import { GetApiComponent } from './componet/get-api/get-api.component';
import { ConnectCComponent } from './componet/connect-c/connect-c.component';


@Component({
  selector: 'app-root',
  imports: [NavComponent,DetailComponent,GetApiComponent,ConnectCComponent],
  template: '<app-nav></app-nav><app-detail></app-detail><app-get-api></app-get-api><app-connect-c></app-connect-c>',

  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'my-project';
}
