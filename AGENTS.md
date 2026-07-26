# AGENTS.md - PortalQuest Backend

This file gives AI assistants the context needed to work effectively on the PortalQuest backend. It was updated after scanning the repository on 2026-07-26.

## Project Overview

PortalQuest is an early-stage fantasy RPG backend inspired by Dungeons & Dragons. The intended product includes a character builder, rules glossary, fight manager, AI assistant, official/SRD content, homebrew content, and bilingual rules data.

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

## When Making Changes

Before editing, identify the layer:

- Domain invariant or model? `PortalQuest.Domain`
- Use case, DTO, specification, interface, mapping? `PortalQuest.Application`
- EF Core/repository/migration/unit of work? `PortalQuest.Persistence`
- External service implementation? `PortalQuest.Infrastructure`
- HTTP endpoint/middleware/composition? `PortalQuest.Web`
- JSON import/parsing/admin utility? `PortalQuest.Console`

Keep changes scoped. If no existing pattern covers a new feature, choose a Clean Architecture/CQRS-compatible pattern and briefly call out the decision.

Never revert unrelated user changes. This repository may have local ignored files and generated output.
