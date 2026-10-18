INSERT INTO public.contracts
(
    contract_number,
    contract_product_id,
    contract_status_id,
    contract_date
)
SELECT
    seed.contract_number,
    cp.id,
    cs.id,
    seed.contract_date
FROM
(
    VALUES
        ('001234567890', 'SAVINGS_PLUS',     'ACTIVE', DATE '2025-03-15'),
        ('009876543210', 'CURRENT_STANDARD', 'ACTIVE', DATE '2024-08-01'),
        ('005555555555', 'CREDIT_GOLD',      'ACTIVE', DATE '2026-01-10'),
        ('001111111111', 'SAVINGS_NORMAL',   'CLOSED', DATE '2023-02-20')
) AS seed(contract_number, contract_product_code, contract_status_code, contract_date)
JOIN public.contract_products cp
    ON cp.code = seed.contract_product_code
JOIN public.cls_contract_statuses cs
    ON cs.code = seed.contract_status_code
ON CONFLICT (contract_number) DO NOTHING;
