# AGENTS.md — PortalQuest Backend

This file gives AI assistants (Claude, Copilot, etc.) the context needed to work effectively on the PortalQuest backend. Read this before making changes.

## Project Overview

**PortalQuest** is a fantasy RPG backend inspired by Dungeons & Dragons, supporting rules content for **5e and 5.5e**, plus **homebrew** content. It powers a website with features including:

- **Character Builder** (Aurora-like) — guided character creation
- **Glossary** — spells, classes/subclasses, items, conditions, races, and all reference rules
- **Fight Manager** — combat/encounter tracking
- **AI Assistant** — uses a user's built characters and active rules to give contextual advice
- **Bilingual content** — all rules content exists in **English and Farsi** (architecture should not hardcode to a single language; more languages may be added later)

The project is in **early-stage development** — core domain and structure exist, but many features are still being built out. Expect incomplete implementations; don't assume a pattern is "final" just because it's the only example in the codebase.

## Tech Stack

- **.NET 8**, ASP.NET Core
- **Entity Framework Core** — data access
- **PostgreSQL** — database
- **MediatR** — CQRS mediator
- **Docker** — containerized local dev / deployment (see `Dockerfile`, `docker-compose.yml`)

## Architecture

PortalQuest follows **Clean Architecture** with **Domain-Driven Design (DDD)** and **CQRS**. Layers, from innermost to outermost:

| Project | Responsibility |
|---|---|
| `PortalQuest.Domain` | Entities, value objects, domain logic, domain events. No dependencies on other layers. |
| `PortalQuest.Application` | Use cases via CQRS (Commands/Queries + Handlers using MediatR), DTOs, interfaces for infrastructure (repositories, services) that outer layers implement. |
| `PortalQuest.Infrastructure` | Implementations of external concerns — e.g. external services, AI assistant integration, email, file storage. |
| `PortalQuest.Persistence` | EF Core `DbContext`, entity configurations, migrations, repository implementations. |
| `PortalQuest.Web` | ASP.NET Core Web API — REST controllers, request/response models, DI composition root, middleware. |
| `PortalQuest.Console` | Console app — likely for seeding, admin scripts, or data import (e.g. SRD/homebrew content import). |

**Dependency rule:** dependencies point inward only. `Domain` depends on nothing. `Application` depends only on `Domain`. `Infrastructure` and `Persistence` depend on `Application`/`Domain` (implementing their interfaces), never the other way around. `Web` composes everything at startup but business logic never lives in `Web`.

### CQRS / MediatR conventions

- Each use case is a **Command** (writes) or **Query** (reads) in `Application`, with a matching `Handler`.
- Controllers in `Web` should be thin: map HTTP request → Command/Query → `IMediator.Send(...)` → map result to HTTP response. No business logic in controllers.
- Validation belongs in the Application layer (e.g. via FluentValidation pipeline behaviors, if/when added) — not in controllers, not in Domain entities directly unless it's a true invariant.

### API style

- `PortalQuest.Web` exposes a **REST API** using controllers (not Minimal APIs). Follow standard REST conventions for routes and HTTP verbs (`GET /api/spells`, `POST /api/characters`, etc.) and keep controller actions consistent with the existing route/naming patterns already in the codebase.

## Domain Concepts to Respect

When modeling or extending domain content, keep these distinctions clear:

- **Rule content vs. homebrew content** — SRD/official content and user-created homebrew should be modeled so they can coexist (e.g. via a shared abstraction/interface, or a source/origin flag) without homebrew polluting official data.
- **Localization** — any user-facing rules text (spell descriptions, class features, item text, etc.) needs to support multiple languages (currently English + Farsi). Prefer a translation/localization table or pattern over hardcoded language-specific fields, so adding a language later doesn't require schema rework.
- **Versioning of rules** — 5e and 5.5e have overlapping but distinct rules for the same named things (e.g. a spell or class may differ between editions). Model this explicitly rather than assuming one edition.

## Testing

No testing framework has been chosen yet. Default to **xUnit** (the standard for ASP.NET Core/.NET projects) unless told otherwise, and flag this assumption when writing tests. Prefer:
- Unit tests for `Domain` and `Application` (handlers, business rules)
- Integration tests for `Persistence`/`Web` where EF Core or HTTP behavior matters

## Coding Conventions

- Follow standard C#/.NET naming conventions (PascalCase for public members, camelCase for locals/parameters, `I`-prefixed interfaces).
- Keep entities in `Domain` free of EF Core or infrastructure attributes where possible — configure persistence mapping in `Persistence` (Fluent API), not via data annotations on domain classes.
- New use cases go in `Application` as `SomeAction` + `SomeActionHandler` pairs, following whatever pattern already exists in the codebase for consistency.
- Prefer async all the way through (`async`/`await`, `Task<T>` returns) for I/O-bound operations (DB, external calls).

## When Making Changes

- Check which layer a change belongs to before writing code — if you're unsure, start from `Domain` outward and ask: "does this represent a business rule, a use case, an external concern, or an HTTP concern?"
- Since the project is early-stage, if no existing pattern covers a new feature, propose a pattern consistent with Clean Architecture/DDD/CQRS rather than inventing something ad hoc.
- Don't introduce a new library or framework (e.g. a validation library, mapping library) without flagging it — the project doesn't have one chosen yet for several cross-cutting concerns (validation, object mapping, etc.).

## Open Decisions (flag if relevant to your task)

- Testing framework: not yet chosen (defaulting to xUnit above)
- Validation library: not yet chosen (e.g. FluentValidation vs. manual)
- Object mapping: not yet chosen (e.g. Mapster/AutoMapper vs. manual mapping)
- AI Assistant integration architecture: not yet defined (which layer owns the LLM call, how character/rules context is assembled)