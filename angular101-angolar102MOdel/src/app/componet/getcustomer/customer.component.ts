import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../sevice/customer.service';
import { CommonModule } from '@angular/common'; // <-- Import CommonModule here
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css'],
  imports:[CommonModule, RouterModule]
})
export class CustomerComponent implements OnInit {
  customers: any[] = [];

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe(
      (data) => {
        console.log('Data received from API:', data); // Log the data to the console
        this.customers = data;
      },
      (error) => {
        console.error('Error fetching data:', error); // Log any errors to the console
      }
    );
  }
}
