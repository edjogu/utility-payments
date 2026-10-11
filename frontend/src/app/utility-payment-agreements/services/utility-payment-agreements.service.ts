import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { UtilityPaymentAgreement } from '../models/utility-payment-agreement';
import { UtilityPaymentRequiredInfo } from '../models/utility-payment-required-info';

@Injectable({
  providedIn: 'root',
})
export class UtilityPaymentAgreementsService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'http://localhost:5074/api/utility-payment-agreements';

  getAll(): Observable<UtilityPaymentAgreement[]> {
    return this.http.get<UtilityPaymentAgreement[]>(this.apiUrl);
  }

  getRequiredInfo(agreementId: number): Observable<UtilityPaymentRequiredInfo[]> {
    return this.http.get<UtilityPaymentRequiredInfo[]>(
      `${this.apiUrl}/${agreementId}/required-info`,
    );
  }
}
