# Cadmus API

👀 [Cadmus Page](https://myrmex.github.io/overview/cadmus/)

👉 This is version 10+ of the Cadmus API. The [previous version](https://github.com/vedph/cadmus_api) is meant to be used for legacy projects before the upgrade to .NET 9. This upgrade implies many infrastructural changes which are better implemented in a separate repository. New Cadmus projects will use this version.

🐋 Quick Docker image build (you need to have a `buildx` container):

```bash
docker buildx create --use

docker buildx build . --platform linux/amd64,linux/arm64,windows/amd64,windows/arm64 -t vedph2020/cadmus-api:13.0.4 -t vedph2020/cadmus-codicology-api:latest --push
```

(replace with the current version).

API layer for the Cadmus content editor. This API is the default API serving general and philological parts, and contains all the shared components which can be used to compose your own API:

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

- 2026-02-02:
  - updated packages.
  - 🆕 added endpoint for `GetThesaurusAliases` in `ThesaurusController`.
- 2026-01-26: 🆕 added [taxonomies store](https://github.com/vedph/taxo-store) to the API demo. This is an optional component, but it is used in the demo to show how to manage taxonomies in Cadmus.
- 2026-01-17: updated packages.
- 2025-11-23: ⚠️ upgraded to NET 10.
- 2025-11-18: updated `Cadmus.Export` package after fix.
- 2025-11-02: updated packages.
- 2025-10-28: updated packages.
- 2025-10-08: updated packages.

### 13.0.4

- 2025-09-15: updated packages.
- 2025-08-31: updated packages and version numbers of affected libraries.

### 13.0.3

- 2025-08-07: updated packages and added flags part to profile.
- 2025-08-02:
  - added `w`eek and `M`onth to stats controller time interval units.
  - updated packages.
- 2025-07-24: updated packages.

### 13.0.2

- 2025-07-24:
  - refactored proxy controller. 
  - updated packages.

### 13.0.0

- 2025-07-14:
  - added local controller `WalkerDemoGraphController` to the API app to provide mock data for the graph walker. This is targeted to this app in its role as a development shell and will not be included in real-world Cadmus APIs. All the controllers which will be used there are rather found in libraries. This controller being just a local resource, it also has its own lazy seeder for its mock data database.
  - updated packages.
- 2025-06-22:
  - updated packages (affecting all libraries except `Cadmus.Api.Models`).
  - added `Cadmus.Api.Controllers.Export`. This uses Cadmus migration export components to provide interactive statistics about items editing.
- 2025-06-16: use `AddPreferredSecuritySchemes` in program.

### 12.0.1

- 2025-06-05: updated packages.
- 2025-05-30: fix to languages thesaurus.

### 12.0.0

- 2025-05-02:
  - updated packages, including [Cadmus migration V3](https://github.com/vedph/cadmus-migration-v3/tree/master).
  - updated `preview-profile.json` accordingly.

### 11.0.3

- 2025-04-18:
  - updated packages.
  - added config for new `AssertedHistoricalDatesPart`.

### 11.0.2

- 2025-03-26:
  - added more entries to the `model-types` thesaurus for improved part badge.
  - updated packages.

### 11.0.1

- 2025-03-09: updated packages (with fix to query builder).

### 11.0.0

- 2025-03-02:
  - upgraded Cadmus migration packages to V2. This implied:
    - changing the API endpoint corresponding to `PreviewController.GetTextSpans`.
    - adapting to minor names changes in the migration packages.
  - fixed a minor issue in `ItemController`.
  - updated packages.

While this is a breaking change for exporters, it does not affect the API and its consumers except for the changed API endpoint and its models.

### 10.1.10

- 2025-02-14: updated packages.

### 10.1.9

- 2025-02-13:
  - avoid error when the `settings` section does not exist in profile.
  - updated packages.

### 10.1.8

- 2025-02-08: updated packages and added editor settings:
  - new settings controller in API.
  - seed settings from profile in `HostSeedExtensions`.
  - the updated Cadmus packages added repository methods for settings.

Editor settings are in the root's `settings` property, which is a single object where each property is an object setting: the property name is the setting key, and the property value is the setting value.

By convention, each setting refers to an editor and its ID is the editor's type ID optionally followed by its role ID prefixed by an underscore. For instance, categories editor's settings are under `it.vedph.categories`, and the role-specific settings are under `it.vedph.categories_role`. In MongoDB, each setting is stored as a document in the `settings` collection, with an ID equal to this identifier.

This allows adding specific settings for configurable editors in the UI. Until now, this was possible via thesauri used for this purpose, but this forced settings to be structured as flat string entries, which is not very flexible except for simple cases. Now, each editor can have its own settings, and these can be structured as needed in a freely modeled object.

### 10.1.7

- 2025-01-28: updated packages.
- 2025-01-25: updated packages.
- 2025-01-01: updated packages.
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
