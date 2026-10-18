INSERT INTO public.cls_contract_statuses
(
    code,
    name
)
VALUES
    ('NEW', 'New'),
    ('ACTIVE', 'Active'),
    ('CLOSED', 'Closed')
ON CONFLICT (code) DO NOTHING;
