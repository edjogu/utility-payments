# Utility Payments — Roadmap por fases

## Objetivo del proyecto

Construir una aplicación realista de pago de servicios/colectores para aprender progresivamente:

- Angular.
- Arquitectura de software.
- .NET moderno.
- Minimal APIs.
- Testing.
- PostgreSQL.
- Docker.
- Diseño de APIs.
- Integraciones con servicios externos.

El proyecto se desarrollará por iteraciones pequeñas y funcionales.  
La prioridad no es terminar rápido, sino entender las decisiones, conceptos y código que se van incorporando.

---

# Reglas de trabajo

## Enfoque

Cada fase debe avanzar con este ciclo:

1. Definir el problema.
2. Explicar los conceptos nuevos.
3. Implementar una parte pequeña.
4. Validar que funcione.
5. Probar lo que tenga sentido probar.
6. Revisar arquitectura y dependencias.
7. Documentar decisiones relevantes.
8. Cerrar formalmente la fase antes de continuar.

## Uso de Codex

Codex se utilizará principalmente para tareas rutinarias o mecánicas cuando la decisión técnica ya haya sido tomada.

Ejemplos:

- Crear proyectos repetitivos.
- Agregar referencias.
- Ejecutar validaciones.
- Crear scaffolding previamente definido.
- Revisar estructura.
- Actualizar documentación cuando se le indique.

Codex no debe:

- Cambiar arquitectura por iniciativa propia.
- Agregar patrones no solicitados.
- Hacer commit.
- Hacer push.
- Ejecutar migraciones o llamadas externas sin autorización explícita.

## Formato de cierre de Codex

Cuando Codex participe en el cierre de una fase, debe reportar:

```text
Phase:
Status:
Branch:
Commit: NO
Pushed: NO
Files:
Build:
Tests:
Validation:
Documentation:
Working tree:
Pending:
Stop.
```

---

# Arquitectura base

```text
utility-payments/
│
├── backend/
│   ├── UtilityPayments.slnx
│   ├── src/
│   │   ├── UtilityPayments.Api/
│   │   ├── UtilityPayments.Application/
│   │   ├── UtilityPayments.Domain/
│   │   ├── UtilityPayments.Infrastructure/
│   │   └── UtilityPayments.Services/
│   │
│   └── tests/
│       ├── UtilityPayments.Api.Tests/
│       ├── UtilityPayments.Application.Tests/
│       ├── UtilityPayments.Domain.Tests/
│       ├── UtilityPayments.Infrastructure.Tests/
│       └── UtilityPayments.Services.Tests/
│
├── frontend/
├── docker/
├── docs/
├── compose.yaml
├── global.json
└── README.md
```

## Dependencias permitidas

```text
Domain
└── no depende de ninguna otra capa

Application
└── Domain

Infrastructure
├── Application
└── Domain

Services
├── Application
└── Domain

Api
├── Application
├── Infrastructure
└── Services
```

`Api` actúa como Composition Root.

---

# Fase 0 — Bootstrap técnico — CLOSED

## Estado

```text
CLOSED
```

## Objetivo

Dejar lista una base técnica estable para empezar a desarrollar funcionalidades reales.

## Alcance

### Repositorio y solución

- [x] Crear repositorio.
- [x] Inicializar Git.
- [x] Rama principal `main`.
- [x] Crear estructura raíz.
- [x] Crear `global.json` con .NET 10.
- [x] Crear `UtilityPayments.slnx`.

### Proyectos productivos

- [x] `UtilityPayments.Domain`
- [x] `UtilityPayments.Application`
- [x] `UtilityPayments.Infrastructure`
- [x] `UtilityPayments.Services`
- [x] `UtilityPayments.Api`

### Proyectos de pruebas

- [x] `UtilityPayments.Domain.Tests`
- [x] `UtilityPayments.Application.Tests`
- [x] `UtilityPayments.Infrastructure.Tests`
- [x] `UtilityPayments.Services.Tests`
- [x] `UtilityPayments.Api.Tests`

### Dependencias

- [x] Application → Domain.
- [x] Infrastructure → Application + Domain.
- [x] Services → Application + Domain.
- [x] Api → Application + Infrastructure + Services.
- [x] Cada proyecto de tests referencia su proyecto productivo correspondiente.

### API base

- [x] ASP.NET Core 10 funcionando.
- [x] Minimal APIs funcionando.
- [x] Health Checks de ASP.NET Core.
- [x] OpenAPI nativo.
- [x] Scalar como UI de documentación.

### Validación

- [x] `dotnet build UtilityPayments.slnx`
- [x] `dotnet test UtilityPayments.slnx`
- [x] API ejecuta correctamente.
- [x] `/health` responde `200 OK`.
- [x] `/openapi/v1.json` responde correctamente.
- [x] Scalar funciona.

## Infraestructura local y persistencia

### PostgreSQL y Docker

- [x] Crear `compose.yaml`.
- [x] Levantar PostgreSQL con Docker Compose.
- [x] Definir base de datos, usuario y credenciales locales.
- [x] Validar conexión al contenedor.

### EF Core

- [x] Agregar Entity Framework Core.
- [x] Agregar el provider PostgreSQL de Npgsql.
- [x] Crear `UtilityPaymentsDbContext`.
- [x] Definir `Infrastructure/Persistence/Configurations` como ubicación de las configuraciones de entidades.
- [x] Adoptar scripts SQL versionados en `backend/database/scripts` para los cambios de base de datos, sin EF Core migrations por ahora.

### Configuración

- [x] Configurar la connection string `UtilityPaymentsDatabase`.
- [x] Mantener archivos `.env` fuera del repositorio cuando corresponda.
- [x] Configurar los valores locales de desarrollo de forma consistente con `compose.yaml`.

### Health Check de base de datos

- [x] Agregar PostgreSQL al sistema de Health Checks.
- [x] Verificar comportamiento con PostgreSQL disponible: `/health` responde `200 Healthy`.
- [x] Verificar comportamiento con PostgreSQL detenido: `/health` responde `503 Unhealthy`.

## Definition of Done

La Fase 0 termina cuando:

```text
dotnet build UtilityPayments.slnx
```

y:

```text
dotnet test UtilityPayments.slnx
```

terminan correctamente, PostgreSQL puede levantarse con Docker Compose y la API verifica su disponibilidad mediante Health Checks. EF Core usa Npgsql y `UtilityPaymentsDbContext`; los cambios de base de datos se gestionan mediante scripts SQL versionados, sin EF Core migrations por ahora.

Estado: `CLOSED`.

---

# Fase 1 — Listar Utility Payment Agreements — CLOSED

## Estado

```text
CLOSED
```

## Objetivo

Implementar el primer vertical slice funcional del sistema.

## Caso de uso

```text
List Utility Payment Agreements
```

## Flujo

```text
GET /api/utility-payment-agreements
        ↓
Api
        ↓
Application
        ↓
Infrastructure
        ↓
PostgreSQL
```

## Alcance

### Domain

- [x] Modelar únicamente lo necesario para `UtilityPaymentAgreement`.
- [x] Crear la entidad `UtilityPaymentAgreement`.
- [x] No agregar entidades futuras sin necesidad.

### Application

- [x] Crear el caso de uso `GetActiveUtilityPaymentAgreements`.
- [x] Definir `UtilityPaymentAgreementResponse` como modelo de respuesta.
- [x] Definir la abstracción de persistencia `IUtilityPaymentAgreementRepository`.

### Infrastructure

- [x] Crear la tabla PostgreSQL `public.utility_payment_agreements`.
- [x] Crear `backend/database/scripts/001_create_utility_payment_agreements.sql`.
- [x] Crear `backend/database/scripts/002_seed_utility_payment_agreements.sql`.
- [x] Configurar el mapping de EF Core y el `DbSet` de `UtilityPaymentAgreement`.
- [x] Implementar `UtilityPaymentAgreementRepository`.
- [x] Agregar datos iniciales controlados mediante un script SQL versionado.

### Api

- [x] Crear endpoint:

```text
GET /api/utility-payment-agreements
```

- [x] Documentarlo en OpenAPI.
- [x] Validarlo desde Scalar.

### Frontend

- [x] Crear el modelo TypeScript `UtilityPaymentAgreement`.
- [x] Crear `UtilityPaymentAgreementsService` para consumir:

```text
GET /api/utility-payment-agreements
```

- [x] Crear el componente `UtilityPaymentAgreements`.
- [x] Manejar el estado de carga.
- [x] Mostrar la lista de agreements cuando la consulta es exitosa.
- [x] Manejar el estado de error.
- [x] Usar el control flow de Angular (`@if` / `@for`) en el template.
- [x] Mantener una interfaz visual simple; el styling se abordará en una iteración posterior.

### Integración backend/frontend

- [x] Permitir mediante CORS el frontend Angular local en `http://localhost:4200`.

### Datos iniciales

Ejemplos:

- Electricity.
- Water.
- Mobile Phone.

### Tests

- [x] Tests de Application para el mapping de agreements y la colección vacía.
- [x] Tests de Infrastructure contra PostgreSQL real que filtran activos, ordenan por `Name` y limpian los datos de prueba.
- [x] Tests de API para respuestas `200 OK` con una colección vacía y con datos.
- [x] Los tests de integración y API usan `utility_payments_tests`.
- [x] Los tests de API preparan y limpian su propio estado.
- [x] Ejecutar los tests de Angular con Karma + Firefox.
- [x] Probar el componente con un mock de `UtilityPaymentAgreementsService`.
- [x] Probar de forma independiente el comportamiento HTTP del servicio.
- [x] Suite frontend: `7 SUCCESS`.

## Decisiones

- No usar migraciones de EF Core por ahora.
- Manejar los cambios de base de datos mediante scripts SQL versionados.
- Usar `snake_case` en PostgreSQL.
- `IntegrationKey` no es unique.
- Usar Application Use Cases en lugar de Service + IService cuando no existe una necesidad real de esa abstracción.
- No exponer directamente las entidades de Domain por HTTP.
- Usar `AsNoTracking()` en las consultas de solo lectura.
- Mantener `Microsoft.EntityFrameworkCore` y `Microsoft.EntityFrameworkCore.Relational` alineados en la versión `10.0.12`.
- Usar `utility_payments_tests` para los tests de integración y API.

## Validación

- [x] `dotnet build UtilityPayments.slnx` exitoso.
- [x] Tests exitosos.
- [x] Endpoint validado con `curl`.
- [x] Endpoint visible en Scalar.
- [x] El frontend consume `GET /api/utility-payment-agreements`.
- [x] Los estados de carga, éxito y error están cubiertos por el componente.
- [x] Los 7 tests del frontend pasan con Karma + Firefox.

## Cierre funcional

```text
Backend y base del vertical slice:
4ef79ac feat(agreements): add utility payment agreements vertical slice

Frontend y cierre del vertical slice completo:
bf4e53d feat(agreements): add Angular agreements vertical slice
```

## Definition of Done

El backend retorna desde PostgreSQL los agreements activos disponibles y el frontend los consulta y presenta al usuario, incluyendo los estados de carga y error. Las pruebas relevantes de backend y frontend pasan.

Estado: `CLOSED`.

El vertical slice completo queda cerrado siguiendo la secuencia:

```text
backend feature
→ frontend feature
→ next feature
```

El siguiente feature planificado es `UtilityPaymentRequiredInfo`.

---

# Fase 2 — Información requerida por Agreement — CLOSED

## Estado

```text
CLOSED
```

## Objetivo

Permitir que cada colector defina qué información debe ingresar el usuario antes de realizar una consulta.

## Ejemplos

Un agreement puede requerir:

- Número de contrato.
- Número de cliente.
- Número de teléfono.
- NIC/NIS.
- Número de medidor.
- Otra referencia.

## Alcance

- [x] Modelar `UtilityPaymentRequiredInfo`.
- [x] Relacionarlo con `UtilityPaymentAgreement`.
- [x] Consultar información requerida por agreement.
- [x] Definir orden, etiqueta, obligatoriedad y tipo básico de campo.
- [x] Exponer endpoint correspondiente.
- [x] Agregar pruebas de backend.
- [x] Consumir el endpoint desde Angular.
- [x] Mostrar dinámicamente los campos requeridos al seleccionar un agreement.

### Base de datos

- [x] Crear `backend/database/scripts/003_create_utility_payment_required_info.sql`.
- [x] Crear `backend/database/scripts/004_seed_utility_payment_required_info.sql`.
- [x] Crear la tabla `utility_payment_required_info` con FK a `utility_payment_agreements`.
- [x] Definir unicidad por `(utility_payment_agreement_id, code)`.
- [x] Definir unicidad por `(utility_payment_agreement_id, display_order)`.
- [x] Agregar checks para `data_type`, `display_order`, `min_length`, `max_length` y el rango de longitudes.
- [x] Agregar índice por `utility_payment_agreement_id`.
- [x] Agregar datos iniciales para los agreements existentes:
  - `ELECTRICITY`: `CONTRACT_NUMBER`, `CUSTOMER_NUMBER`.
  - `WATER`: `ACCOUNT_NUMBER`, `METER_NUMBER`.
  - `MOBILE`: `PHONE_NUMBER`.

### Domain

- [x] Crear `UtilityPaymentRequiredInfo`.
- [x] Crear `RequiredInfoDataType`.

### Application

- [x] Crear `IUtilityPaymentRequiredInfoRepository`.
- [x] Crear el caso de uso `GetActiveUtilityPaymentRequiredInfos`.
- [x] Crear `UtilityPaymentRequiredInfoResponse`.
- [x] Registrar las dependencias de Application.

### Infrastructure

- [x] Crear `UtilityPaymentRequiredInfoConfiguration`.
- [x] Agregar `DbSet<UtilityPaymentRequiredInfo>`.
- [x] Implementar `UtilityPaymentRequiredInfoRepository`.
- [x] Registrar las dependencias de Infrastructure.

### Api

- [x] Crear endpoint:

```text
GET /api/utility-payment-agreements/{agreementId}/required-info
```

- [x] Mantener Minimal APIs sin introducir Controllers.
- [x] Organizar los endpoints por feature en `Api/Endpoints`.
- [x] Agregar `EndpointsExtensions` como agregador y dejar `Program.cs` con `app.MapEndpoints();`.

### Frontend

- [x] Crear el modelo TypeScript `UtilityPaymentRequiredInfo`.
- [x] Actualizar `UtilityPaymentAgreementsService` para consumir:

```text
GET /api/utility-payment-agreements/{agreementId}/required-info
```

- [x] Consultar dinámicamente la información requerida al seleccionar un agreement.
- [x] Construir un Reactive Form dinámico a partir de los metadatos retornados por el backend.
- [x] Crear cada `FormControl` usando el `code` de su required-info.
- [x] Aplicar dinámicamente `Validators.required`, `Validators.minLength` y `Validators.pattern` cuando corresponda.
- [x] Aplicar `maxLength` en el input HTML y preparar dinámicamente el placeholder.
- [x] Renderizar los campos mediante `[formGroup]` y `[formControlName]`.
- [x] Mostrar errores usando `touched` y `hasError()`.
- [x] Deshabilitar el botón `Continue` mientras `requiredInfoForm.invalid` sea `true`.
- [x] Mantener una interfaz visual simple; el styling se abordará después de completar más flujo funcional.

### Tests y validación

- [x] Actualizar los tests de integración existentes para limpiar también `utility_payment_required_info`.
- [x] Agregar colecciones xUnit `Database` en `Api.Tests` e `Infrastructure.Tests` para evitar ejecución paralela sobre `utility_payments_tests`.
- [x] `Application.Tests` pasando.
- [x] `Infrastructure.Tests` pasando.
- [x] `Api.Tests` pasando.
- [x] Suite backend: 9 tests, 9 correctos, 0 errores.
- [x] Validar manualmente el endpoint con `curl`; los resultados se retornan ordenados por `displayOrder`.
- [x] Validar manualmente el formulario dinámico con `Water` y `Mobile Phone`.
- [x] Suite frontend: `10 SUCCESS`.
- [x] `ng build` exitoso y sin errores.

`Domain.Tests` y `Services.Tests` todavía no contienen tests reales y muestran la advertencia `no tests available`.

## Decisiones

- Mantener la UI simple por ahora; el estilizado se realizará después de completar más flujo funcional.
- Seguir usando Reactive Forms para los campos dinámicos.
- No introducir Angular Material ni otra librería de componentes todavía.
- Mantener el frontend dentro del feature `utility-payment-agreements` por ahora.
- No refactorizar todavía el `apiUrl` hardcoded ni la configuración CORS.

## Próximo paso

Continuar con la Fase 3 — Cuentas bancarias.

## Definition of Done

Dado un agreement, la API puede informar qué datos necesita para realizar un Inquiry y el frontend puede presentar dinámicamente esos campos al usuario.

Estado: `CLOSED`.

---

# Fase 3 — Cuentas bancarias

## Objetivo

Permitir seleccionar una cuenta desde la cual posteriormente se realizará el pago.

## Alcance

- [ ] Revisar necesidad real de `Customer`.
- [ ] Modelar `BankAccount`.
- [ ] Definir moneda.
- [ ] Definir saldo.
- [ ] Definir estado de cuenta.
- [ ] Listar cuentas disponibles.
- [ ] Agregar reglas de dominio que realmente correspondan.
- [ ] Agregar pruebas.

## Posible endpoint

```text
GET /api/bank-accounts
```

## Definition of Done

La aplicación puede consultar cuentas utilizables para pagos.

---

# Fase 4 — Inquiry de servicio

## Objetivo

Consultar cuánto debe un cliente a un colector.

## Flujo conceptual

```text
Agreement
    ↓
Required Info
    ↓
Application
    ↓
Collector abstraction
    ↓
Services
    ↓
Collector simulado
```

## Alcance

- [ ] Definir contrato de Inquiry.
- [ ] Definir abstracción para colectores.
- [ ] Implementar primer colector simulado.
- [ ] Validar datos requeridos.
- [ ] Retornar datos como:
  - nombre del servicio,
  - referencia,
  - monto,
  - fecha,
  - información adicional.
- [ ] Manejar resultados no encontrados.
- [ ] Manejar errores.
- [ ] Agregar tests de Application y Services.

## Posible endpoint

```text
POST /api/utility-payments/inquiries
```

## Definition of Done

La API puede realizar una consulta de deuda contra al menos un colector simulado.

---

# Fase 5 — Confirmar pago

## Objetivo

Realizar el primer pago completo.

## Flujo conceptual

```text
Cuenta seleccionada
        ↓
Inquiry válido
        ↓
Validaciones
        ↓
Débito
        ↓
Collector
        ↓
Registro del pago
        ↓
Resultado
```

## Alcance

- [ ] Modelar `UtilityPayment`.
- [ ] Definir reglas de fondos.
- [ ] Definir estados de pago.
- [ ] Confirmar pago.
- [ ] Actualizar saldo de cuenta.
- [ ] Registrar transacción.
- [ ] Simular confirmación con colector.
- [ ] Manejar errores de negocio.
- [ ] Manejar transacción de base de datos.
- [ ] Agregar pruebas.

## Posible endpoint

```text
POST /api/utility-payments
```

## Definition of Done

Un usuario puede confirmar un pago y el sistema registra correctamente su resultado.

---

# Fase 6 — Historial de pagos

## Objetivo

Consultar pagos realizados previamente.

## Alcance

- [ ] Definir modelo de consulta.
- [ ] Consultar pagos.
- [ ] Obtener detalle.
- [ ] Incorporar filtros si realmente hacen falta.
- [ ] Agregar pruebas.

## Posibles endpoints

```text
GET /api/utility-payments
GET /api/utility-payments/{id}
```

## Definition of Done

La API permite consultar el historial y detalle de pagos registrados.

---

# Fase 7 — Robustez del backend

## Objetivo

Fortalecer comportamientos que se vuelven importantes cuando el flujo principal ya funciona.

## Temas a evaluar

- [ ] Idempotencia.
- [ ] Reintentos.
- [ ] Timeouts.
- [ ] Cancelación.
- [ ] Manejo global de errores.
- [ ] Problem Details.
- [ ] Logging estructurado.
- [ ] Correlation IDs.
- [ ] Concurrencia.
- [ ] Transacciones.
- [ ] Seguridad de secretos.
- [ ] Validaciones consistentes.
- [ ] Readiness vs liveness.

No se implementarán todos automáticamente. Se incorporarán solo donde el proyecto lo justifique.

## Definition of Done

El backend tiene un manejo consistente de errores y de los escenarios técnicos relevantes identificados hasta ese punto.

---

# Fase 8 — Primera integración HTTP

## Objetivo

Reemplazar o complementar un colector simulado con una integración HTTP controlada.

## Alcance

- [ ] Definir API externa ficticia o sandbox.
- [ ] Utilizar `HttpClient`.
- [ ] Configurar cliente tipado o estrategia equivalente.
- [ ] Manejar timeout.
- [ ] Manejar respuestas HTTP.
- [ ] Mapear contrato externo a contrato interno.
- [ ] Evitar filtrar modelos externos al dominio.
- [ ] Agregar tests del adapter/cliente.

## Definition of Done

Al menos un colector funciona mediante HTTP y está desacoplado de Application mediante una abstracción.

---

# Fase 9 — Inicio del frontend Angular — CLOSED

## Estado

```text
CLOSED
```

## Objetivo

Comenzar Angular cuando el backend ya tenga suficiente funcionalidad útil, sin esperar a que esté completamente terminado.

## Stack

- Angular 20.
- TypeScript.
- HTML.
- CSS/SCSS.
- Standalone Components.

## Forma de aprendizaje

Antes de introducir cada concepto:

1. Qué problema resuelve.
2. Cómo funciona.
3. Por qué lo usamos.
4. Ejemplo pequeño.
5. Aplicación al proyecto.

## Primeros conceptos

- [x] Estructura del proyecto Angular.
- [x] Standalone Components.
- [ ] Routing.
- [x] Services.
- [x] Dependency Injection.
- [x] HttpClient.
- [x] Reactive Forms.
- [x] Observables.
- [x] RxJS.
- [x] Signals.
- [x] Manejo de errores.

## Primer flujo

```text
Angular
   ↓
GET /api/utility-payment-agreements
   ↓
Mostrar Agreements
```

## Definition of Done

Angular consume el primer endpoint real del backend y muestra la información al usuario.

Estado: `CLOSED`.

---

# Fase 10 — Flujo frontend de pago

## Objetivo

Construir progresivamente el flujo completo desde Angular.

## Iteraciones

- [x] Listar agreements.
- [x] Seleccionar agreement.
- [x] Mostrar información requerida.
- [ ] Seleccionar cuenta.
- [ ] Crear formulario de Inquiry.
- [ ] Consultar deuda.
- [ ] Mostrar resultado.
- [ ] Confirmar pago.
- [ ] Mostrar resultado de transacción.
- [ ] Consultar historial.

## Conceptos Angular que aparecerán según necesidad

- Components.
- Inputs/outputs.
- Services.
- Reactive Forms.
- HttpClient.
- Observables.
- RxJS.
- Signals.
- Interceptors.
- Guards.
- Routing.
- Manejo de estado.
- Validaciones.
- Testing.

## Definition of Done

El flujo principal de pago puede ejecutarse desde la interfaz Angular.

---

# Fase 11 — Autenticación y autorización

## Objetivo

Agregar identidad únicamente cuando el flujo funcional ya esté suficientemente maduro.

## Temas

- [ ] Autenticación.
- [ ] Autorización.
- [ ] Usuario actual.
- [ ] Protección de endpoints.
- [ ] Guards en Angular.
- [ ] Interceptor de autenticación.
- [ ] Manejo seguro de tokens según la estrategia escogida.

## Definition of Done

Las operaciones protegidas requieren una identidad válida y respetan las reglas de autorización definidas.

---

# Fase 12 — Calidad y evolución

## Objetivo

Preparar el proyecto para seguir creciendo sin convertirlo en una demostración innecesaria de patrones.

## Posibles temas

- [ ] Mejorar documentación.
- [ ] Architecture Decision Records.
- [ ] Testcontainers.
- [ ] Cobertura de tests útil.
- [ ] CI.
- [ ] Dockerizar backend.
- [ ] Dockerizar frontend.
- [ ] Compose completo.
- [ ] Observabilidad.
- [ ] Métricas.
- [ ] OpenTelemetry.
- [ ] Versionado de API.
- [ ] Rate limiting.
- [ ] Caché.
- [ ] Nuevos colectores.

Cada elemento se evaluará por necesidad real.

---

# Estado actual

## Fase activa

```text
Fase 3 — Cuentas bancarias
```

## Completado

- Fase 0 — Bootstrap técnico: `CLOSED`.
- Fase 1 — Listar Utility Payment Agreements: `CLOSED`.
- Fase 2 — Información requerida por Agreement: `CLOSED`.
- Fase 9 — Inicio del frontend Angular: `CLOSED`.
- Repositorio y estructura inicial.
- .NET 10.
- Solution `.slnx`.
- 5 proyectos productivos.
- 5 proyectos de tests.
- Dependencias base.
- Minimal API.
- Health Checks.
- OpenAPI.
- Scalar.
- Build completo.
- Test completo.
- Backend de `List Utility Payment Agreements`.
- Frontend Angular 20 de `List Utility Payment Agreements`.
- Integración local backend/frontend mediante CORS.
- Estados de carga, éxito y error en el componente.
- Pruebas de componente con el servicio mockeado.
- Pruebas HTTP independientes del servicio.
- Suite frontend: `7 SUCCESS` con Karma + Firefox.
- Vertical slice `List Utility Payment Agreements`: `CLOSED`.
- Backend y frontend de `UtilityPaymentRequiredInfo`.
- Formulario reactivo dinámico con validaciones basadas en metadatos.
- Suite backend de Fase 2: 9 tests correctos.
- Suite frontend: `10 SUCCESS`.
- Build de Angular exitoso.

## En progreso transversal

```text
Fase 10 — Flujo frontend de pago
```

Las iteraciones de listar agreements, seleccionar un agreement y mostrar su información requerida están completadas. Los pasos restantes continúan abiertos y avanzarán con los siguientes vertical slices.

## Próximo punto al retomar

```text
Cuentas bancarias
```

El proyecto continúa por vertical slices:

```text
backend feature
→ frontend feature
→ next feature
```

---

# Checklist de cierre de una fase

Antes de declarar una fase como terminada:

- [ ] El objetivo funcional/técnico fue cumplido.
- [ ] La solución compila.
- [ ] Las pruebas relevantes pasan.
- [ ] No existen cambios accidentales.
- [ ] Las dependencias entre proyectos siguen las reglas definidas.
- [ ] No se agregaron patrones o paquetes sin justificación.
- [ ] La documentación relevante está actualizada.
- [ ] Se revisaron pendientes conocidos.
- [ ] Codex reportó estado final si participó.
- [ ] El usuario decide cuándo hacer commit/push.

---

# Principio rector

> Construir solo la complejidad necesaria para el siguiente problema real, entenderla antes de continuar y mantener siempre visibles las fronteras entre dominio, aplicación, infraestructura, integraciones y presentación.
