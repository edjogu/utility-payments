CREATE TABLE public.cls_currencies
(
    id          BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code        VARCHAR(3) NOT NULL,
    name        VARCHAR(100) NOT NULL,
    is_active   BOOLEAN NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_cls_currencies_code UNIQUE (code)
);
