INSERT INTO public.cls_client_types
(
    code,
    name
)
VALUES
    ('NATURAL_PERSON', 'Natural Person'),
    ('LEGAL_ENTITY', 'Legal Entity')
ON CONFLICT (code) DO NOTHING;
