CREATE TABLE public.clients
(
    id              BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    client_type_id  BIGINT NOT NULL,
    client_code     VARCHAR(30) NOT NULL,
    document_number VARCHAR(30) NOT NULL,
    first_name      VARCHAR(100) NOT NULL,
    last_name       VARCHAR(100) NOT NULL,
    is_active       BOOLEAN NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_clients_client_code
        UNIQUE (client_code),

    CONSTRAINT uq_clients_document_number
        UNIQUE (document_number),

    CONSTRAINT fk_clients_client_type
        FOREIGN KEY (client_type_id)
        REFERENCES public.cls_client_types(id)
);

CREATE INDEX ix_clients_client_type_id
    ON public.clients (client_type_id);
