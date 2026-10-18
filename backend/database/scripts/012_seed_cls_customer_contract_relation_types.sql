INSERT INTO public.cls_customer_contract_relation_types
(
    code,
    name
)
VALUES
    ('OWNER', 'Owner'),
    ('AUTHORIZED', 'Authorized')
ON CONFLICT (code) DO NOTHING;
