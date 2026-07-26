# AGENTS.md - PortalQuest Backend

This file gives AI assistants the context needed to work effectively on the PortalQuest backend. It was revised on 2026-07-26 to make the product context easier to use while preserving the existing technical and architectural rules.

## Agent Operating Principles

- Scope changes tightly to the user request.
- Preserve Clean Architecture, DDD, and CQRS boundaries.
- Prefer existing project patterns over introducing new abstractions or libraries.
- Treat local ignored files, generated files, and unrelated user changes as user-owned. Do not revert them.
- Fix known rough edges only when they are directly in scope or required for correctness.
- Do not perform the future .NET 10 upgrade or NoSQL database work unless the user explicitly asks for those tasks.

## Product Context

PortalQuest is an early-stage Dungeons & Dragons reference and gameplay backend. The first product goal is a bilingual rules reference site. Later phases expand toward character building, monster/lore content, user profiles, character state management, and AI-assisted dungeon-master workflows.

The backend should be designed as a long-lived rules and gameplay platform, not only as a simple content catalog.

## Product Roadmap Context

The exact phase order may change, but the expected product areas are:

### Phase 1 - D&D Rules Reference

The first phase is a reference site for D&D rules content:

- Spells
- Items
- Classes
- Rules text
- Other core game-reference content
- English and Farsi presentation

Current database structure and imported data are based on free data extracted from 5e.tools. The importer work belongs to `PortalQuest.Console`.

Important bilingual behavior:

- Spell names may remain the same as English.
- Farsi descriptions should be easier and more natural for Farsi-speaking users.
- Do not hardcode English-only rules structures where translated explanations are product requirements.

The database structure must strongly support homebrew content alongside official/SRD/basic-rules content.

### Phase 2 - Character Builder

The next major product area is a powerful, fast, feature-rich bilingual character builder. It should be designed to become stronger than common D&D character-builder tools such as Aurora, especially in rules calculation.

Expected character-builder qualities:

- Fast character creation and editing
- Rich feature support
- Bilingual user-facing content
- Strong rules calculations
- Computable spell-slot/resource spending
- Logical derivation of character stats, abilities, inventory effects, and combat-relevant state
- A gameplay feel closer to Baldur's Gate-style character systems, where character mechanics are consistently computable

### Phase 3 - Monsters, Setups, Lore, and RAG Content

The project is expected to load monsters, well-known setups, and multiple lore sources.

This content should be:

- Searchable and discoverable by users
- Stored with enough structure and provenance for later AI/RAG usage
- Compatible with multiple lore sources or campaign settings
- Kept separate from user-created homebrew unless intentionally linked

### Phase 4 - User Profiles, Character Sheets, Backgrounds, and AI DM

Users are expected to have profiles and saved characters.

User-owned content may include:

- Character creation and management
- Character sheets
- Character background stories
- Character state and progression
- User-authored campaign or roleplay context

Character sheets and backgrounds should be structured so a future RAG layer can help an AI dungeon master use that data. The AI DM should be able to answer at different player knowledge levels and should be able to use rules, character, background, lore, and state data together.

### Character Management Application

Another expected product area is a character-management application, potentially web-based and possibly also a standalone application. This backend is responsible for supporting that application.

This area is closer to Baldur's Gate 3 style character management:

- Character health
- Current status/effects
- Inventory
- Character and NPC management
- DM access to manage player characters and NPCs
- Shared state needed for gameplay and sessions

Product-quality priorities:

- Rules data must be trustworthy, source-aware, and edition-aware.
- Official/SRD/basic-rules content must coexist with homebrew without being overwritten or mutated by user content.
- Names alone are not reliable identifiers for rules content because editions, sources, and homebrew variants can overlap.
- User-facing rules text should remain localizable.
- Gameplay workflows should support both players and game masters, even when a specific role model has not been fully implemented yet.
- AI-facing behavior should preserve provenance and avoid fabricating rules details when source data is missing.

## Current Implementation Snapshot

Current implemented source code is mostly focused on core rules content import and lookup:

- Books
- Classes and subclasses
- Effects/conditions
- Spells
- Translation support for English and Farsi
- A Web API with a spells endpoint
- A console importer/utility that reads local JSON data and sends MediatR commands

Expect incomplete features and placeholder code. Do not assume the only implementation in the repo is the final pattern unless it is reinforced elsewhere.

## Solution Layout

The solution is `PortalQuest.sln` and targets .NET 8.

| Project | Current responsibility |
|---|---|
| `PortalQuest.Domain` | Domain entities, value objects, enums, and small domain-facing interfaces such as `IGuidService`, `ITranslatable<TTranslation>`, and `ITranslation`. |
| `PortalQuest.Application` | DTOs, CQRS requests/handlers, repository/service interfaces, specifications, AutoMapper profile, response helpers, and MediatR pipeline behaviors. Depends only on `Domain`. |
| `PortalQuest.Infrastructure` | External/infrastructure service implementations. Currently only registers `IGuidService` via `GuidService`. |
| `PortalQuest.Persistence` | EF Core `PortalQuestDbContext`, migrations, repositories, unit of work, and specification evaluator. Uses PostgreSQL via Npgsql. |
| `PortalQuest.Web` | ASP.NET Core Web API composition root, controllers, middleware, Swagger, Serilog, and request language resolution. |
| `PortalQuest.Console` | Interactive console utility/importer. Uses app host DI, MediatR, local ignored JSON data, and commands such as `get-books`, `get-classes`, `get-effects`, and `get-spells`. |

## Technology Stack

- .NET 8 with nullable reference types and implicit usings enabled
- ASP.NET Core controllers, not Minimal APIs
- Entity Framework Core 9 packages with Npgsql/PostgreSQL
- MediatR 14 for CQRS
- AutoMapper 16 for DTO/entity mapping
- FluentValidation package and validation pipeline behavior are present, but validator assembly registration is currently commented out
- Serilog in `PortalQuest.Web`
- Swagger/Swashbuckle in `PortalQuest.Web`
- Newtonsoft.Json and HtmlAgilityPack in `PortalQuest.Console`
- Docker and Docker Compose for app + PostgreSQL

Do not introduce a new cross-cutting library without calling it out. The repo already has AutoMapper, MediatR, FluentValidation, EF Core, Serilog, and Newtonsoft.Json.

## Architecture Rules

PortalQuest follows Clean Architecture with DDD and CQRS.

- `Domain` must not depend on other PortalQuest projects.
- `Application` depends on `Domain` only.
- `Infrastructure` and `Persistence` implement interfaces from `Application`/`Domain`.
- `Web` and `Console` compose the application at startup.
- Business logic belongs in `Domain` when it is an invariant, or in `Application` handlers when it is use-case orchestration.
- Controllers should stay thin: HTTP input -> request DTO/command/query -> `IMediator.Send(...)` -> HTTP response.
- Keep EF Core-specific behavior in `Persistence` where possible. Some existing domain classes use `[ForeignKey]`; avoid adding more persistence attributes unless you are intentionally following an existing local constraint.

## Product-Aware Modeling Rules

- Treat rules entities as long-lived product data, not just CRUD records.
- Model source, edition, language, and ownership explicitly when they affect product behavior.
- Preserve official/SRD/basic-rules provenance. Do not merge homebrew data into official records.
- Prefer stable IDs and source metadata over name-based matching.
- Keep descriptions, names, class features, spell text, item text, and rules text localizable.
- When adding gameplay features, separate rules definitions from character/campaign state.
- Character-builder and character-management features should be computable from rules, character choices, inventory, effects, and state rather than stored as opaque manual values whenever practical.
- Store user-owned character sheets, backgrounds, and campaign state in a way that can later feed RAG/AI DM workflows without mixing private user data into global rules content.
- When adding AI-assistant-facing features later, expose enough provenance for answers to cite or trace source content.
- Keep monster, lore, setup, and campaign-setting data source-aware and searchable so it can support both direct user lookup and future vector/RAG indexing.

## Current CQRS Pattern

Feature code lives under `PortalQuest.Application/Features`.

Existing examples:

- `Features/Core/Spell/Query/GetAllSpellsRequest.cs`
- `Features/Core/Spell/Command/UpsertSpellRequest.cs`
- `Features/Core/Book/Query/GetBooksListRequest.cs`
- `Features/Core/Class/Query/GetClassesListRequst.cs` (typo exists in filename/class name)
- `Features/Core/Effect/Query/GetEffectsListRequest.cs`

Requests implement `IRequest<TResponse>` and usually contain an internal handler class in the same file. Handlers depend on repository interfaces, `IUnitOfWork`, `IMapper`, `IGuidService`, and current-language services as needed.

Write handlers async all the way through and pass cancellation tokens to repository methods whenever available.

## Responses, DTOs, and Mapping

Common response wrappers are:

- `ResponseDto<T>` for command/single-result style responses
- `PagedResultDto<T>` for list queries
- `ResponseFactory` for common error/success response creation

DTOs live under `PortalQuest.Application/DTOs`. AutoMapper mappings are centralized in `PortalQuest.Application/Profiles/MappingProfile.cs`.

When adding DTOs:

- Keep them in `Application`, not `Web`.
- Add explicit AutoMapper mappings when mapping is not trivial.
- Be careful with translated entities: current mappings often read `Translations.FirstOrDefault()!`, assuming specifications included only the requested language.

## Specifications and Repositories

The repo uses a simple specification pattern:

- Interfaces/base classes are in `PortalQuest.Application/Specifications`.
- Concrete specs live near features, such as spell/effect list specs.
- `PortalQuest.Persistence/Specifications/SpecificationEvaluator.cs` applies criteria, includes, ordering, paging, no-tracking, and split queries.

Repositories are in `PortalQuest.Persistence/Repository` and implement interfaces in `PortalQuest.Application/Interfaces/Repository`.

`GenericRepository<T>` supports:

- `Get`
- `FirstOrDefault(ISpecification<T>)`
- `GetAll(ISpecification<T>)`
- `GetAll(Expression<Func<T,bool>>?)`
- `Any`
- `Count`
- `Add` / `AddRange`
- `Update` / `UpdateRange`
- `SoftDelete`
- `Remove` / `RemoveRange`

Use repository interfaces from handlers. Call `IUnitOfWork.SaveChangesAsync()` after writes.

## Domain Modeling Notes

Core entities inherit from:

- `BaseEntity`: `Id`, `IsDeleted`
- `BaseRuleEntity` (file is currently `BaseCoreContentEntity.cs`): SRD/basic-rules/source metadata
- `BaseCoreEntity`: `Name`, `Content`

Implemented translation pattern:

- Translatable entities implement `ITranslatable<TTranslation>`.
- Translation entities implement `ITranslation` and include a `LanguageCodeEnum`.
- `LanguageCodeEnum` currently contains `En` and `Fa`.
- `TranslatableSpecification<T,TTranslation>` includes only translations for the requested language.

User-facing rules text should stay localizable. Do not add hardcoded English-only/Farsi-only fields for descriptions, names, class features, spell text, item text, or rules text. Prefer translation entities or an equivalent extensible pattern.

Rule content source/version concerns matter:

- Many rule entities include `SourceId`, `SourcePage`, `SRD`, `NameInSRD`, and `BasicRules`.
- 5e and 5.5e/2024 content can overlap by name. Model edition/source differences explicitly instead of relying on names alone.
- Homebrew should coexist with official/SRD content without mutating official data.

## Persistence Details

`PortalQuestDbContext` currently exposes DbSets for:

- `Classes`
- `Effects`
- `Books`
- `Spells`
- `Logs`

Important EF behavior:

- All subclasses of `BaseEntity` get `Id` as a non-generated required key.
- `BaseCoreEntity.Content` is configured as `jsonb`.
- `Spell.Range`, `Spell.Durations`, and `Spell.CastingTimes` are owned value objects.
- Spell durations are stored in `SpellDurations`.
- Spell casting times are stored in `SpellCastingTimes`.
- Global query filters exclude soft-deleted `Class`, `Effect`, `Book`, and `Spell` rows.
- `SaveChangesAsync` coerces `DateTime` and nullable `DateTime` values to UTC.

When adding entities, remember to:

- Add DbSets where needed.
- Add Fluent API configuration in `OnModelCreating` or a future configuration class.
- Add/update migrations in `PortalQuest.Persistence/Migrations`.
- Register repositories in `PortalQuest.Persistence/DependencyInjection.cs` if using a specific repository.

## Web API Notes

`PortalQuest.Web` currently has a controller-based API with route style:

- `api/v1/[controller]`
- Example: `SpellsController`

Startup flow in `Program.cs`:

1. `AddApplicationServices()`
2. `AddInfrastructureServices()`
3. `AddPersistenceServices(configuration)`
4. `AddWebServices(configuration)`
5. Swagger/SwaggerUI
6. Custom middleware
7. `MapControllers()`

Registered middleware order:

1. `CorrelationIdMiddleware`
2. Serilog request logging
3. `ExceptionHandlingMiddleware`
4. `LanguageResolverMiddleware`

Language is resolved from `?lang=` first, then `Accept-Language`, then defaults to `LanguageCodeEnum.En`. Application queries that need localized content should use `ICurrentLanguageService`.

## Console Importer Notes

`PortalQuest.Console` is an interactive utility:

- Registers the same Application/Infrastructure/Persistence services as the Web app.
- Registers all `IConsoleCommand` implementations manually.
- Prompts for a command name in `CommandRunner`.
- Reads JSON data from `PortalQuest.Console/data`, which is ignored by git.

If adding an importer command:

- Implement `IConsoleCommand`.
- Register it in `PortalQuest.Console/Program.cs`.
- Prefer sending Application commands/queries through MediatR instead of writing directly to repositories from the console layer.
- Keep source-specific parsing details inside `PortalQuest.Console`.

## Configuration and Docker

Local ignored config/data:

- `.env` is ignored.
- `appsettings.Development.json` is ignored.
- `data/` is ignored.

Docker Compose expects these environment variables:

- `DB_USER`
- `DB_PASSWORD`
- `DB_NAME`
- `DB_PORT`
- `PORT`

`docker-compose.yml` starts:

- `db`: `postgres:16`
- `web`: built from `Dockerfile`, exposing container port `8080`

The Dockerfile restores/builds/publishes `PortalQuest.Web`. It uses .NET 8 SDK/runtime images and sets the final entrypoint to `dotnet PortalQuest.Web.dll`.

Note: `PortalQuest.Web/appsettings.json` currently contains a SQL Server-style local connection string, while persistence uses Npgsql/PostgreSQL. Docker Compose overrides the connection string with PostgreSQL. Prefer environment-specific configuration rather than relying on the checked-in default for local PostgreSQL work.

## Build and Run Commands

Useful commands from the repository root:

```bash
dotnet restore PortalQuest.sln
dotnet build PortalQuest.sln
dotnet run --project PortalQuest.Web/PortalQuest.Web.csproj
dotnet run --project PortalQuest.Console/PortalQuest.Console.csproj
docker compose build
docker compose up -d
```

EF Core migration/update commands usually need the Web project as startup and Persistence as migrations project:

```bash
dotnet ef migrations add <Name> --project PortalQuest.Persistence --startup-project PortalQuest.Web
dotnet ef database update --project PortalQuest.Persistence --startup-project PortalQuest.Web
```

## Testing

There are currently no test projects in the solution. Default to xUnit for new tests unless the user chooses another framework.

Prefer:

- Unit tests for `Domain` and `Application` behavior.
- Handler tests around MediatR request handlers and specifications.
- Integration tests for EF Core mappings/repositories and Web API behavior.

If adding tests, create a clear test project and include it in `PortalQuest.sln`.

## Coding Conventions

- Follow standard C#/.NET naming conventions.
- Keep nullable reference warnings in mind; initialize required collection properties or make nullability explicit.
- Prefer primary constructors only where they match the surrounding style.
- Use `async`/`await` for I/O-bound work.
- Pass `CancellationToken` through public async flows.
- Keep controllers thin and avoid business logic in `Web`.
- Keep parsing/import concerns in `Console`, not `Application`.
- Keep persistence mechanics in `Persistence`, not `Domain`.
- Use existing response DTOs and `ResponseFactory` for application responses.
- Use existing repository/specification patterns for query filtering, includes, ordering, and paging.

## Known Rough Edges to Preserve Carefully

These are current repo facts, not necessarily desired end-state:

- `GetClassesListRequst` is misspelled.
- `SpellListSpec .cs` has a trailing space in the filename.
- `LoggingPipelineBehavior.cs` lives under `Features/Common/PipelineBehaviors` but its namespace is `PortalQuest.Application.Features.Common.Pipeline`.
- FluentValidation package and pipeline exist, but validator registration is commented out.
- Some domain classes currently use data annotations such as `[ForeignKey]`.
- `SpellsController.Get(Guid id)` is a placeholder returning `Ok()` with no data.
- There are ignored/generated `bin`, `obj`, `data`, and local configuration files in the workspace.

When touching nearby code, fix rough edges only if they are in scope for the task or necessary to make the requested change correct. Avoid unrelated cleanup churn.

## Change Placement Guide

Before editing, identify the layer:

- Domain invariant or model? `PortalQuest.Domain`
- Use case, DTO, specification, interface, mapping? `PortalQuest.Application`
- EF Core/repository/migration/unit of work? `PortalQuest.Persistence`
- External service implementation? `PortalQuest.Infrastructure`
- HTTP endpoint/middleware/composition? `PortalQuest.Web`
- JSON import/parsing/admin utility? `PortalQuest.Console`

Keep changes scoped. If no existing pattern covers a new feature, choose a Clean Architecture/CQRS-compatible pattern and briefly call out the decision.
