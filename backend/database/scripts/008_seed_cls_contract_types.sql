INSERT INTO public.cls_contract_types
(
    code,
    name
)
VALUES
    ('SAVINGS_ACCOUNT', 'Savings Account'),
    ('CURRENT_ACCOUNT', 'Current Account'),
    ('CREDIT_CARD', 'Credit Card')
ON CONFLICT (code) DO NOTHING;
