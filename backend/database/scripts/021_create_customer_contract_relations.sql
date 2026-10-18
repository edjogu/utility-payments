CREATE TABLE public.customer_contract_relations
(
    id                                  BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    client_id                           BIGINT NOT NULL,
    contract_id                         BIGINT NOT NULL,
    customer_contract_relation_type_id  BIGINT NOT NULL,
    is_active                           BOOLEAN NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_customer_contract_relations
        UNIQUE
        (
            client_id,
            contract_id,
            customer_contract_relation_type_id
        ),

    CONSTRAINT fk_customer_contract_relations_client
        FOREIGN KEY (client_id)
        REFERENCES public.clients(id),

    CONSTRAINT fk_customer_contract_relations_contract
        FOREIGN KEY (contract_id)
        REFERENCES public.contracts(id),

    CONSTRAINT fk_customer_contract_relations_relation_type
        FOREIGN KEY (customer_contract_relation_type_id)
        REFERENCES public.cls_customer_contract_relation_types(id)
);

CREATE INDEX ix_customer_contract_relations_client_id
    ON public.customer_contract_relations (client_id);

CREATE INDEX ix_customer_contract_relations_contract_id
    ON public.customer_contract_relations (contract_id);

CREATE INDEX ix_customer_contract_relations_relation_type_id
    ON public.customer_contract_relations (customer_contract_relation_type_id);

CREATE INDEX ix_customer_contract_relations_client_id_is_active
    ON public.customer_contract_relations (client_id, is_active);
