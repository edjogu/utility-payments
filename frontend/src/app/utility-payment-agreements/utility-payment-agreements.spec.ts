import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, Subject, throwError } from 'rxjs';

import { UtilityPaymentAgreements } from './utility-payment-agreements';
import { UtilityPaymentAgreementsService } from './services/utility-payment-agreements.service';
import { UtilityPaymentAgreement } from './models/utility-payment-agreement';
import { UtilityPaymentRequiredInfo } from './models/utility-payment-required-info';

describe('UtilityPaymentAgreements', () => {
  let component: UtilityPaymentAgreements;
  let fixture: ComponentFixture<UtilityPaymentAgreements>;

  const agreements: UtilityPaymentAgreement[] = [
    {
      id: 1,
      code: 'ELECTRICITY',
      name: 'Electricity',
      description: 'Electricity utility payment',
    },
  ];

  let utilityPaymentAgreementsServiceMock: jasmine.SpyObj<UtilityPaymentAgreementsService>;

  beforeEach(async () => {
    utilityPaymentAgreementsServiceMock = jasmine.createSpyObj<UtilityPaymentAgreementsService>(
      'UtilityPaymentAgreementsService',
      ['getAll', 'getRequiredInfo'],
    );

    utilityPaymentAgreementsServiceMock.getAll.and.returnValue(of(agreements));

    await TestBed.configureTestingModule({
      imports: [UtilityPaymentAgreements],
      providers: [
        {
          provide: UtilityPaymentAgreementsService,
          useValue: utilityPaymentAgreementsServiceMock,
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(UtilityPaymentAgreements);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render available agreements', () => {
    const element: HTMLElement = fixture.nativeElement;

    expect(element.textContent).toContain('Electricity');
  });

  it('should render an error message when loading agreements fails', () => {
    utilityPaymentAgreementsServiceMock.getAll.and.returnValue(
      throwError(() => new Error('Request failed')),
    );

    fixture = TestBed.createComponent(UtilityPaymentAgreements);
    component = fixture.componentInstance;
    fixture.detectChanges();

    const element: HTMLElement = fixture.nativeElement;

    expect(element.textContent).toContain('Unable to load available services.');
  });

  it('should show loading while agreements are being loaded', () => {
    const agreementsSubject = new Subject<UtilityPaymentAgreement[]>();

    utilityPaymentAgreementsServiceMock.getAll.and.returnValue(agreementsSubject.asObservable());

    fixture = TestBed.createComponent(UtilityPaymentAgreements);
    component = fixture.componentInstance;
    fixture.detectChanges();

    const element: HTMLElement = fixture.nativeElement;

    expect(element.textContent).toContain('Loading services...');
  });

  it('should render dynamic required info fields when an agreement is selected', () => {
    // Arrange
    const requiredInfo: UtilityPaymentRequiredInfo[] = [
      {
        id: 1,
        code: 'CONTRACT_NUMBER',
        label: 'Contract Number',
        dataType: 'text',
        isRequired: true,
        displayOrder: 1,
        minLength: null,
        maxLength: 20,
        validationPattern: null,
        placeholder: null,
      },
      {
        id: 2,
        code: 'CUSTOMER_NUMBER',
        label: 'Customer Number',
        dataType: 'text',
        isRequired: true,
        displayOrder: 2,
        minLength: null,
        maxLength: 20,
        validationPattern: null,
        placeholder: null,
      },
    ];

    utilityPaymentAgreementsServiceMock.getRequiredInfo.and.returnValue(of(requiredInfo));

    const element: HTMLElement = fixture.nativeElement;
    const agreementButton = element.querySelector<HTMLButtonElement>('li button');

    // Act
    agreementButton?.click();
    fixture.detectChanges();

    // Assert
    expect(utilityPaymentAgreementsServiceMock.getRequiredInfo).toHaveBeenCalledWith(1);

    expect(element.querySelector('#CONTRACT_NUMBER')).toBeTruthy();
    expect(element.querySelector('#CUSTOMER_NUMBER')).toBeTruthy();
  });

  it('should enable Continue only when all required fields are valid', () => {
    // Arrange
    const requiredInfo: UtilityPaymentRequiredInfo[] = [
      {
        id: 1,
        code: 'CONTRACT_NUMBER',
        label: 'Contract Number',
        dataType: 'text',
        isRequired: true,
        displayOrder: 1,
        minLength: null,
        maxLength: 20,
        validationPattern: null,
        placeholder: null,
      },
      {
        id: 2,
        code: 'CUSTOMER_NUMBER',
        label: 'Customer Number',
        dataType: 'text',
        isRequired: true,
        displayOrder: 2,
        minLength: null,
        maxLength: 20,
        validationPattern: null,
        placeholder: null,
      },
    ];

    utilityPaymentAgreementsServiceMock.getRequiredInfo.and.returnValue(of(requiredInfo));

    const element: HTMLElement = fixture.nativeElement;
    const agreementButton = element.querySelector<HTMLButtonElement>('li button');

    // Act - select agreement
    agreementButton?.click();
    fixture.detectChanges();

    const continueButton = element.querySelector<HTMLButtonElement>('form button');

    // Assert - form starts invalid
    expect(continueButton?.disabled).toBeTrue();

    // Act - fill both required fields
    const contractNumber = element.querySelector<HTMLInputElement>('#CONTRACT_NUMBER');

    const customerNumber = element.querySelector<HTMLInputElement>('#CUSTOMER_NUMBER');

    contractNumber!.value = 'ABC123';
    contractNumber!.dispatchEvent(new Event('input'));

    customerNumber!.value = 'CUSTOMER001';
    customerNumber!.dispatchEvent(new Event('input'));

    fixture.detectChanges();

    // Assert - form is now valid
    expect(continueButton?.disabled).toBeFalse();
  });
});
