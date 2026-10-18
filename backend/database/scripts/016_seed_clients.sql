INSERT INTO public.clients
(
    client_type_id,
    client_code,
    document_number,
    first_name,
    last_name
)
SELECT
    ct.id,
    'CL000001',
    '123456789',
    'Test',
    'Customer'
FROM public.cls_client_types ct
WHERE ct.code = 'NATURAL_PERSON'
ON CONFLICT (client_code) DO NOTHING;
