BEGIN;

CREATE TABLE public.utility_payment_required_info
(
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,

    utility_payment_agreement_id BIGINT NOT NULL,

    code VARCHAR(50) NOT NULL,
    label VARCHAR(100) NOT NULL,
    data_type VARCHAR(20) NOT NULL,

    is_required BOOLEAN NOT NULL DEFAULT TRUE,
    display_order INTEGER NOT NULL,

    min_length INTEGER NULL,
    max_length INTEGER NULL,

    validation_pattern VARCHAR(300) NULL,
    placeholder VARCHAR(150) NULL,

    is_active BOOLEAN NOT NULL DEFAULT TRUE,

    created_at_utc TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at_utc TIMESTAMPTZ NULL,

    CONSTRAINT fk_utility_payment_required_info_agreement
        FOREIGN KEY (utility_payment_agreement_id)
        REFERENCES public.utility_payment_agreements (id),

    CONSTRAINT uq_utility_payment_required_info_agreement_code
        UNIQUE (utility_payment_agreement_id, code),

    CONSTRAINT uq_utility_payment_required_info_agreement_order
        UNIQUE (utility_payment_agreement_id, display_order),

    CONSTRAINT ck_utility_payment_required_info_data_type
        CHECK (data_type IN ('text', 'number', 'date')),

    CONSTRAINT ck_utility_payment_required_info_display_order
        CHECK (display_order > 0),

    CONSTRAINT ck_utility_payment_required_info_min_length
        CHECK (min_length IS NULL OR min_length >= 0),

    CONSTRAINT ck_utility_payment_required_info_max_length
        CHECK (max_length IS NULL OR max_length >= 0),

    CONSTRAINT ck_utility_payment_required_info_length_range
        CHECK (
            min_length IS NULL
            OR max_length IS NULL
            OR min_length <= max_length
        )
);

CREATE INDEX ix_utility_payment_required_info_agreement_id
    ON public.utility_payment_required_info (utility_payment_agreement_id);

COMMIT;
