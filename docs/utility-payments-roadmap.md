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

# Fase 0 — Bootstrap técnico

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

## Pendiente para cerrar Fase 0

### PostgreSQL y Docker

- [ ] Crear `compose.yaml`.
- [ ] Levantar PostgreSQL con Docker Compose.
- [ ] Definir base de datos, usuario y credenciales locales.
- [ ] Validar conexión al contenedor.

### EF Core

- [ ] Agregar Entity Framework Core.
- [ ] Agregar provider PostgreSQL.
- [ ] Crear `UtilityPaymentsDbContext`.
- [ ] Definir ubicación de configuraciones de entidades.
- [ ] Preparar estrategia inicial de migraciones.

### Configuración

- [ ] Configurar connection string.
- [ ] Mantener secretos locales fuera del repositorio cuando corresponda.
- [ ] Validar configuración por ambiente.

### Health Check de base de datos

- [ ] Agregar PostgreSQL al sistema de Health Checks.
- [ ] Verificar comportamiento con PostgreSQL disponible.
- [ ] Verificar comportamiento con PostgreSQL detenido.

## Definition of Done

La Fase 0 termina cuando:

```text
dotnet build UtilityPayments.slnx
```

y:

```text
dotnet test UtilityPayments.slnx
```

terminan correctamente, PostgreSQL puede levantarse con Docker Compose y la API puede verificar su disponibilidad mediante Health Checks.

---

# Fase 1 — Listar Utility Payment Agreements

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

- [ ] Modelar únicamente lo necesario para `UtilityPaymentAgreement`.
- [ ] Definir invariantes reales si aparecen.
- [ ] No agregar entidades futuras sin necesidad.

### Application

- [ ] Crear caso de uso para listar agreements.
- [ ] Definir DTO/result apropiado.
- [ ] Definir abstracción de persistencia si es necesaria.

### Infrastructure

- [ ] Configurar persistencia de `UtilityPaymentAgreement`.
- [ ] Implementar acceso a datos.
- [ ] Crear migración inicial necesaria.
- [ ] Agregar datos iniciales controlados.

### Api

- [ ] Crear endpoint:

```text
GET /api/utility-payment-agreements
```

- [ ] Documentarlo en OpenAPI.
- [ ] Validarlo desde Scalar.

### Datos iniciales

Ejemplos:

- Electricity.
- Water.
- Mobile Phone.

### Tests

- [ ] Tests de dominio si existen reglas de dominio reales.
- [ ] Tests de Application.
- [ ] Tests de Infrastructure contra PostgreSQL cuando corresponda.
- [ ] Test de API del endpoint.

## Definition of Done

El endpoint retorna desde PostgreSQL los agreements disponibles y las pruebas relevantes pasan.

---

# Fase 2 — Información requerida por Agreement

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

- [ ] Modelar `UtilityPaymentRequiredInfo`.
- [ ] Relacionarlo con `UtilityPaymentAgreement`.
- [ ] Consultar información requerida por agreement.
- [ ] Definir orden, etiqueta, obligatoriedad y tipo básico de campo.
- [ ] Exponer endpoint correspondiente.
- [ ] Agregar pruebas.

## Definition of Done

Dado un agreement, la API puede informar qué datos necesita para realizar un Inquiry.

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

# Fase 9 — Inicio del frontend Angular

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

- [ ] Estructura del proyecto Angular.
- [ ] Standalone Components.
- [ ] Routing.
- [ ] Services.
- [ ] Dependency Injection.
- [ ] HttpClient.
- [ ] Reactive Forms.
- [ ] Observables.
- [ ] RxJS.
- [ ] Signals.
- [ ] Manejo de errores.

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

---

# Fase 10 — Flujo frontend de pago

## Objetivo

Construir progresivamente el flujo completo desde Angular.

## Iteraciones

- [ ] Listar agreements.
- [ ] Seleccionar agreement.
- [ ] Mostrar información requerida.
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
Fase 0 — Bootstrap técnico
```

## Completado

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

## Próximo punto al retomar

```text
PostgreSQL + Docker Compose
```

Después:

```text
EF Core
→ DbContext
→ conexión
→ Health Check PostgreSQL
→ cierre de Fase 0
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
