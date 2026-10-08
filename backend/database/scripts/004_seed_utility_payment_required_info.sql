BEGIN;

INSERT INTO public.utility_payment_required_info
(
    utility_payment_agreement_id,
    code,
    label,
    data_type,
    is_required,
    display_order,
    min_length,
    max_length,
    validation_pattern,
    placeholder,
    is_active
)
SELECT
    agreement.id,
    seed.code,
    seed.label,
    seed.data_type,
    seed.is_required,
    seed.display_order,
    seed.min_length,
    seed.max_length,
    seed.validation_pattern,
    seed.placeholder,
    TRUE
FROM
(
    VALUES
        ('ELECTRICITY', 'CONTRACT_NUMBER', 'Contract Number', 'text', TRUE, 1, NULL::INTEGER, 20, NULL::VARCHAR, NULL::VARCHAR),
        ('ELECTRICITY', 'CUSTOMER_NUMBER', 'Customer Number', 'text', TRUE, 2, NULL::INTEGER, 20, NULL::VARCHAR, NULL::VARCHAR),

        ('WATER', 'ACCOUNT_NUMBER', 'Account Number', 'text', TRUE, 1, NULL::INTEGER, 20, NULL::VARCHAR, NULL::VARCHAR),
        ('WATER', 'METER_NUMBER', 'Meter Number', 'text', TRUE, 2, NULL::INTEGER, 20, NULL::VARCHAR, NULL::VARCHAR),

        ('MOBILE', 'PHONE_NUMBER', 'Phone Number', 'text', TRUE, 1, NULL::INTEGER, 15, NULL::VARCHAR, NULL::VARCHAR)
) AS seed
(
    agreement_code,
    code,
    label,
    data_type,
    is_required,
    display_order,
    min_length,
    max_length,
    validation_pattern,
    placeholder
)
INNER JOIN public.utility_payment_agreements agreement
    ON agreement.code = seed.agreement_code
ON CONFLICT (utility_payment_agreement_id, code) DO NOTHING;

COMMIT;
