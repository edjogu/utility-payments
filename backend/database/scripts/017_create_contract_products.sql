CREATE TABLE public.contract_products
(
    id                BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    contract_type_id  BIGINT NOT NULL,
    currency_id       BIGINT NOT NULL,
    code              VARCHAR(50) NOT NULL,
    name              VARCHAR(100) NOT NULL,
    is_active         BOOLEAN NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_contract_products_code
        UNIQUE (code),

    CONSTRAINT fk_contract_products_contract_type
        FOREIGN KEY (contract_type_id)
        REFERENCES public.cls_contract_types(id),

    CONSTRAINT fk_contract_products_currency
        FOREIGN KEY (currency_id)
        REFERENCES public.cls_currencies(id)
);

CREATE INDEX ix_contract_products_contract_type_id
    ON public.contract_products (contract_type_id);

CREATE INDEX ix_contract_products_currency_id
    ON public.contract_products (currency_id);
