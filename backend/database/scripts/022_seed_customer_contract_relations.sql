INSERT INTO public.customer_contract_relations
(
    client_id,
    contract_id,
    customer_contract_relation_type_id,
    is_active
)
SELECT
    c.id,
    ct.id,
    rt.id,
    TRUE
FROM
(
    VALUES
        ('CL000001', '001234567890', 'OWNER'),
        ('CL000001', '009876543210', 'OWNER'),
        ('CL000001', '005555555555', 'OWNER'),
        ('CL000001', '001111111111', 'OWNER')
) AS seed(client_code, contract_number, relation_type_code)
JOIN public.clients c
    ON c.client_code = seed.client_code
JOIN public.contracts ct
    ON ct.contract_number = seed.contract_number
JOIN public.cls_customer_contract_relation_types rt
    ON rt.code = seed.relation_type_code
ON CONFLICT
(
    client_id,
    contract_id,
    customer_contract_relation_type_id
)
DO NOTHING;
