import { Component, inject, OnInit, signal } from '@angular/core';
import { UtilityPaymentAgreement } from './models/utility-payment-agreement';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { UtilityPaymentAgreementsService } from './services/utility-payment-agreements.service';
import { UtilityPaymentRequiredInfo } from './models/utility-payment-required-info';

@Component({
  selector: 'app-utility-payment-agreements',
  imports: [ReactiveFormsModule],
  templateUrl: './utility-payment-agreements.html',
  styleUrl: './utility-payment-agreements.css',
})
export class UtilityPaymentAgreements implements OnInit {
  private readonly utilityPaymentAgreementsService = inject(UtilityPaymentAgreementsService);

  protected requiredInfoForm = new FormGroup({});

  protected readonly agreements = signal<UtilityPaymentAgreement[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  protected readonly selectedAgreementId = signal<number | null>(null);
  protected readonly requiredInfo = signal<UtilityPaymentRequiredInfo[]>([]);
  protected readonly requiredInfoLoading = signal(false);
  protected readonly requiredInfoError = signal<string | null>(null);

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
      },
    });
  }

  protected selectAgreement(agreementId: number): void {
    this.selectedAgreementId.set(agreementId);
    this.requiredInfo.set([]);
    this.requiredInfoError.set(null);
    this.requiredInfoLoading.set(true);

    this.utilityPaymentAgreementsService.getRequiredInfo(agreementId).subscribe({
      next: (requiredInfo) => {
        this.requiredInfo.set(requiredInfo);
        this.buildRequiredInfoForm(requiredInfo);
        this.requiredInfoLoading.set(false);
      },
      error: () => {
        this.requiredInfoError.set('Unable to load required information.');
        this.requiredInfoLoading.set(false);
      },
    });
  }

  private buildRequiredInfoForm(requiredInfo: UtilityPaymentRequiredInfo[]): void {
    const controls: Record<string, FormControl<string>> = {};

    for (const info of requiredInfo) {
      const validators = [];

      if (info.isRequired) {
        validators.push(Validators.required);
      }

      if (info.minLength !== null) {
        validators.push(Validators.minLength(info.minLength));
      }

      if (info.validationPattern !== null) {
        validators.push(Validators.pattern(info.validationPattern));
      }

      controls[info.code] = new FormControl('', {
        nonNullable: true,
        validators,
      });
    }

    this.requiredInfoForm = new FormGroup(controls);
  }
}
