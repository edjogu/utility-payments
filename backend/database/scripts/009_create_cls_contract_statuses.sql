CREATE TABLE public.cls_contract_statuses
(
    id          BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code        VARCHAR(30) NOT NULL,
    name        VARCHAR(100) NOT NULL,
    is_active   BOOLEAN NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_cls_contract_statuses_code UNIQUE (code)
);
