import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UtilityPaymentAgreements } from './utility-payment-agreements/utility-payment-agreements';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UtilityPaymentAgreements],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Utility Payments');
}
