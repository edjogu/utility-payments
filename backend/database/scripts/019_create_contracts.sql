CREATE TABLE public.contracts
(
    id                  BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    contract_number     VARCHAR(50) NOT NULL,
    contract_product_id BIGINT NOT NULL,
    contract_status_id  BIGINT NOT NULL,
    contract_date       DATE NOT NULL,

    CONSTRAINT uq_contracts_contract_number
        UNIQUE (contract_number),

    CONSTRAINT fk_contracts_contract_product
        FOREIGN KEY (contract_product_id)
        REFERENCES public.contract_products(id),

    CONSTRAINT fk_contracts_contract_status
        FOREIGN KEY (contract_status_id)
        REFERENCES public.cls_contract_statuses(id)
);

CREATE INDEX ix_contracts_contract_product_id
    ON public.contracts (contract_product_id);

CREATE INDEX ix_contracts_contract_status_id
    ON public.contracts (contract_status_id);
