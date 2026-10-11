export interface UtilityPaymentRequiredInfo {
  id: number;
  code: string;
  label: string;
  dataType: string;
  isRequired: boolean;
  displayOrder: number;
  minLength: number | null;
  maxLength: number | null;
  validationPattern: string | null;
  placeholder: string | null;
}
