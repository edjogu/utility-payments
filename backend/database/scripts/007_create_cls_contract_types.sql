CREATE TABLE public.cls_contract_types
(
    id          BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code        VARCHAR(50) NOT NULL,
    name        VARCHAR(100) NOT NULL,
    is_active   BOOLEAN NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_cls_contract_types_code UNIQUE (code)
);
