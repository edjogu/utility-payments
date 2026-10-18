INSERT INTO public.contract_products
(
    contract_type_id,
    currency_id,
    code,
    name
)
SELECT
    ct.id,
    c.id,
    seed.code,
    seed.name
FROM
(
    VALUES
        ('SAVINGS_ACCOUNT', 'USD', 'SAVINGS_NORMAL', 'Savings Normal'),
        ('SAVINGS_ACCOUNT', 'USD', 'SAVINGS_PLUS', 'Savings Plus'),
        ('CURRENT_ACCOUNT', 'USD', 'CURRENT_STANDARD', 'Current Standard'),
        ('CREDIT_CARD', 'USD', 'CREDIT_GOLD', 'Credit Gold')
) AS seed(contract_type_code, currency_code, code, name)
JOIN public.cls_contract_types ct
    ON ct.code = seed.contract_type_code
JOIN public.cls_currencies c
    ON c.code = seed.currency_code
ON CONFLICT (code) DO NOTHING;
