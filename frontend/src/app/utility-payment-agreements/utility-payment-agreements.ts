import { Component, inject, OnInit, signal } from '@angular/core';
import { UtilityPaymentAgreement } from './models/utility-payment-agreement';
import { UtilityPaymentAgreementsService } from './services/utility-payment-agreements.service';

@Component({
  selector: 'app-utility-payment-agreements',
  imports: [],
  templateUrl: './utility-payment-agreements.html',
  styleUrl: './utility-payment-agreements.css',
})
export class UtilityPaymentAgreements implements OnInit {
  private readonly utilityPaymentAgreementsService =
    inject(UtilityPaymentAgreementsService);

  protected readonly agreements = signal<UtilityPaymentAgreement[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadAgreements();
  }

  private loadAgreements(): void {
    this.utilityPaymentAgreementsService.getAll().subscribe({
      next: (agreements) => {
        this.agreements.set(agreements);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Unable to load available services.');
        this.loading.set(false);
      }
    });
  }
}
