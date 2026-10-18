INSERT INTO public.cls_currencies
(
    code,
    name
)
VALUES
    ('USD', 'US Dollar')
ON CONFLICT (code) DO NOTHING;
