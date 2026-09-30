BEGIN;

INSERT INTO public.utility_payment_agreements
(
    code,
    name,
    description,
    integration_key,
    allow_information_request,
    is_active
)
VALUES
(
    'ELECTRICITY',
    'Electricity',
    'Electricity utility payment',
    'ELECTRICITY_MOCK',
    TRUE,
    TRUE
),
(
    'WATER',
    'Water',
    'Water utility payment',
    'WATER_MOCK',
    TRUE,
    TRUE
),
(
    'MOBILE',
    'Mobile Phone',
    'Mobile phone utility payment',
    'MOBILE_MOCK',
    TRUE,
    TRUE
);

ON CONFLICT (code) DO NOTHING;

COMMIT;
