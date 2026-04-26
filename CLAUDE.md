# CLAUDE.md

## Purpose of the Solution

This solution contains API components and demo for the Cadmus editor. The editor is based on a MongoDB database and is focused on dynamic models based on "items" and "parts". Records are items, which 'include' parts, which are the actual data. In MongoDB items are in the items collection and parts in the parts collection.

Items have a fixed set of metadata, while every part has a model of its own. This way the items model is dynamically built by composition, and changes whenever you add or remove parts. Facets define which parts can be included in an item, and their metadata for the frontend part of the editor.

This solution provides API components to be used in a third-party API solution, which will just be a light wrapper around these components.

## Projects Overview

- `Cadmus.Api.Config`: API configuration (services and database seeding).
- `Cadmus.Api.Controllers`: core API controllers.
- `Cadmus.Api.Controllers.Export`: API controllers for data export.
- `Cadmus.Api.Controllers.Import`: API controllers for data import.
- `Cadmus.Api.Models`: API models (DTOs).
- `Cadmus.Api.Services`: API services (business logic and infrastructure).
- `CadmusApi`: a demo API project, which is a light wrapper around the API components.

Some dependencies can be found in these folders:

- Cadmus.Core: `\Projects\Cadmus\Cadmus`
- Cadmus.Migration: `\Projects\Cadmus\Cadmus.Migration`
- Cadmus.Bricks: `\Projects\Cadmus\CadmusBricks`
- Cadmus.General.Parts: `\Projects\Cadmus\Cadmus.General.Parts`
- Cadmus.Philology.Parts: `\Projects\Cadmus\Cadmus.Philology.Parts`
- Fusi.Antiquity: `\Projects\Antiquity`
- Fusi.Api.Auth: `\Projects\Auth`
- Fusi.Cli: `Projects\Fusi.Cli`
- Fusi.DbManager: `\Projects\DbManager`
- Fusi.Tools: `\Projects\Tools`
- MessagingApi: `Projects\MessagingApi`
- Proteus: `\Projects\Proteus`

## Coding Conventions

As for C# code:

- Use modern, idiomatic, robust C#.
- Avoid systematic use of `var`; use explicit types unless required.
- Use nullable reference types (`string?`).
- Apply single responsibility principle.
- Write clear, robust code with appropriate error handling.
- Validate non-null arguments (`ArgumentNullException.ThrowIfNull`).
- Comment public classes and methods.
- Keep lines under ~80 characters; split logically.
