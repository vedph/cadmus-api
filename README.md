# Cadmus API

👀 [Cadmus Page](https://myrmex.github.io/overview/cadmus/)

👉 This is version 10+ of the Cadmus API. The [previous version](https://github.com/vedph/cadmus_api) is meant to be used for legacy projects before the upgrade to .NET 9. This upgrade implies many infrastructural changes which are better implemented in a separate repository. New Cadmus projects will use this version.

🐋 Quick **Docker** image build: `docker build . -t vedph2020/cadmus-api:10.1.0 -t vedph2020/cadmus-api:latest` (replace with the current version).

API layer for the Cadmus content editor.

This API is the default API serving general and philological parts, and contains all the shared components which can be used to compose your own API:

- `Cadmus.Api.Models`: API data models.
- `Cadmus.Api.Services`: API services.
- `Cadmus.Api.Controllers`: API controllers.
- `Cadmus.Api.Controllers.Import`: API controllers for data import.
- `Cadmus.Api.Config`: API services configuration helpers.

The API application proper just adds a couple of application-specific services implementations:

- `AppPartSeederFactoryProvider` implementing `IPartSeederFactoryProvider`;
- `AppRepositoryProvider` implementing `IRepositoryProvider`.

Both these services depend on the parts you choose to support, so they are implemented at the application level.

## History

- 2024-12-23: updated packages.

### 10.1.6

- 2024-12-02: added more claims to `AuthController` after updating packages, so that the JWT token also contains first and last name for the user.

### 10.1.5

- 2024-11-30: updated packages.

### 10.1.4

- 2024-11-29: updated packages to deal with added `isAdmin` in flag definitions.
- 2024-11-22: updated packages for `CadmusApi`.

### 10.1.3

- 2024-11-20: updated packages.

### 10.1.2

- 2024-11-19:
  - changed name of database seeder delay to `Seed:Delay` (thus `SEED__DELAY` in Docker compose) in `Cadmus.Api.Config`.
  - added `ASPNETCORE_URLS=http://+:8080` to the sample Docker compose script. This is required to let the API listen on its designated 8080 port, because the default behavior for ASP.NET core is to listen on 5000 (HTTP) and 5001 (HTTPS) ports in development mode. Outside development mode, like in Docker, we need to specify the port explicitly.

### 10.1.0

- 2024-11-18:
  - replaced Swagger with [Scalar](https://github.com/scalar/scalar). This is a consequence of the upgrade to .NET 9 with its open API capabilities, allowing to drop Swagger whose development was somewhat left behind.
  - refactored API by moving shared services and their configuration to the new `Cadmus.Api.Config` library. This makes the API app much lighter and more focused on its own services, while the shared services are now in a separate library.
  - refactored API to use new style without the legacy `Startup` class.
  - added rate limiting option.
  - ⚠️ moved authentication to PostgreSQL rather than Mongo, thus removing the dependency on `AspNetCore.Identity.Mongo`. This dependency was introduced because originally Cadmus was required to use a single database type just to avoid complicating the hosting environment. Now that a RDBMS (PostgreSQL) has become a full citizen in the Cadmus system (as it is used not only for indexes but also for LOD graphs), and Docker has been adopted, this is no longer a requirement. So moving to PostgreSQL for authentication is a step toward a more flexible and modular architecture, removing the need for a specific database type (and library) for authentication. This is not a breaking change from the point of view of consumer code, but it implies that if you want to use version 10 API you will need to migrate your users from the old MongoDB into PostgreSQL, or just re-create their accounts in the Cadmus UI.
  - fixed `ItemController` get part methods return values which were broken after upgrade. As these methods return an object of any type from JSON, the return value is now built via a `JsonDocument`.

### 10.0.0

- 2024-11-13: ⚠️ upgraded to .NET 9.

For the rest of history (before version 10) see <https://github.com/vedph/cadmus_api#history>.
