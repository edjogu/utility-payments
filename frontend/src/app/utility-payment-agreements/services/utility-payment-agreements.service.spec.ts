import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

import { UtilityPaymentAgreementsService } from './utility-payment-agreements.service';
import { UtilityPaymentAgreement } from '../models/utility-payment-agreement';
import { UtilityPaymentRequiredInfo } from '../models/utility-payment-required-info';

describe('UtilityPaymentAgreementsService', () => {
  let service: UtilityPaymentAgreementsService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });

    service = TestBed.inject(UtilityPaymentAgreementsService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should return utility payment agreements', () => {
    const expectedAgreements: UtilityPaymentAgreement[] = [
      {
        id: 1,
        code: 'ELECTRICITY',
        name: 'Electricity',
        description: 'Electricity utility payment',
      },
    ];

    service.getAll().subscribe((agreements) => {
      expect(agreements).toEqual(expectedAgreements);
    });

    const request = httpTestingController.expectOne(
      'http://localhost:5074/api/utility-payment-agreements',
    );

    expect(request.request.method).toBe('GET');

    request.flush(expectedAgreements);
  });

  it('should return required info for an agreement', () => {
    const expectedRequiredInfo: UtilityPaymentRequiredInfo[] = [
      {
        id: 4,
        code: 'ACCOUNT_NUMBER',
        label: 'Account Number',
        dataType: 'text',
        isRequired: true,
        displayOrder: 1,
        minLength: null,
        maxLength: 20,
        validationPattern: null,
        placeholder: null,
      },
    ];

    service.getRequiredInfo(2).subscribe((requiredInfo) => {
      expect(requiredInfo).toEqual(expectedRequiredInfo);
    });

    const request = httpTestingController.expectOne(
      'http://localhost:5074/api/utility-payment-agreements/2/required-info',
    );

    expect(request.request.method).toBe('GET');

    request.flush(expectedRequiredInfo);
  });
});
