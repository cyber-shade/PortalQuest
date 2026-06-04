# PortalQuest

A fantasy RPG backend built with ASP.NET Core, inspired by Dungeons & Dragons.

PortalQuest provides a foundation for managing characters, spells, races, classes, items, and homebrew content across multiple campaign settings.

## Features

* Character creation and management
* Character builder
* Spell library
* Race and class catalog
* Item management
* Support for SRD and homebrew content
* User profiles

## Architecture

* Clean Architecture
* CQRS with MediatR
* Domain-Driven Design (DDD)

## Technologies

* ASP.NET Core
* Entity Framework Core
* PostgreSQL
* MediatR

## Running Locally

1. Clone the repository
2. Configure the database connection string
3. Apply EF Core migrations
4. Run the application

```bash
dotnet ef database update
dotnet run
```
