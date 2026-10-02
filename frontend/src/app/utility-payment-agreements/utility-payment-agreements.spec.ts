import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, Subject, throwError } from 'rxjs';

import { UtilityPaymentAgreements } from './utility-payment-agreements';
import { UtilityPaymentAgreementsService } from './services/utility-payment-agreements.service';
import { UtilityPaymentAgreement } from './models/utility-payment-agreement';

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
      ['getAll'],
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
});
