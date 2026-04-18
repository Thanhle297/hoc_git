# AGENTS.md

This document gives coding agents the operational rules for this repository.
It is intentionally practical and specific to the current codebase.

## Project Snapshot

- Stack: ASP.NET Core MVC
- Language: C#
- SDK style project: `Microsoft.NET.Sdk.Web`
- Target framework: `net8.0`
- Nullable reference types: enabled
- Implicit global usings: enabled
- Solution: `DATN.sln`
- Main project: `DATN.csproj`

## Repository Layout

- `Program.cs`: app bootstrap and middleware pipeline
- `Controllers/`: MVC controllers
- `Models/`: view/data models
- `Views/`: Razor views
- `wwwroot/`: static assets
- `Properties/`: launch settings and project metadata

## Source of Truth for Rules

- `.cursor/rules/`: not found
- `.cursorrules`: not found
- `.github/copilot-instructions.md`: not found

If these files are added later, treat them as higher-priority instructions than this guide.

## Build / Run / Test Commands

Run commands from repository root: `D:\XLSS\DATN`.

### Restore

```bash
dotnet restore DATN.sln
```

### Build

```bash
dotnet build DATN.sln
dotnet build DATN.csproj -c Release
```

### Run locally

```bash
dotnet run --project DATN.csproj
```

### Watch mode (development)

```bash
dotnet watch --project DATN.csproj run
```

### Lint / Formatting

There is no dedicated lint config currently (no `.editorconfig` or StyleCop file found).
Use `dotnet format` as the default formatting/lint pass:

```bash
dotnet format DATN.sln
dotnet format DATN.sln --verify-no-changes
```

### Tests (current state)

There is currently no test project in the repository.
If a test project is added, use:

```bash
dotnet test DATN.sln
```

### Run a single test (important)

Preferred patterns once tests exist:

```bash
# By fully qualified name substring
dotnet test --filter "FullyQualifiedName~Namespace.ClassName.TestName"

# By exact test method name
dotnet test --filter "Name=TestMethodName"

# By class
dotnet test --filter "FullyQualifiedName~Namespace.ClassName"
```

If multiple test projects are added later, target one explicitly:

```bash
dotnet test path/to/Project.Tests.csproj --filter "Name=TestMethodName"
```

## Coding Conventions (C# / ASP.NET Core MVC)

Follow existing project patterns and default .NET conventions.

### Imports and namespaces

- Keep `using` directives minimal and relevant.
- Rely on implicit usings where available; do not add redundant usings.
- Place explicit usings at the top of the file.
- Use file/namespace structure consistent with folders (for example `DATN.Controllers`).

### Formatting

- Use 4 spaces for indentation; no tabs.
- Use standard C# brace style (opening brace on next line for types/methods).
- Keep lines readable; avoid overly dense one-liners.
- Ensure files end with a newline.
- Run `dotnet format` after non-trivial edits.

### Types and nullability

- Nullable is enabled: model nullability intentionally.
- Use `string?` (or nullable reference types) only when null is a valid state.
- Prefer explicit, meaningful types in public APIs.
- Use `var` when the type is obvious from the right-hand side.
- Avoid null-forgiving (`!`) unless there is a strong guarantee and comment-worthy reason.

### Naming

- `PascalCase`: classes, methods, properties, action methods.
- `camelCase`: local variables, parameters.
- `_camelCase`: private readonly fields (as used in `HomeController`).
- Controller names should end with `Controller`.
- Action names should be verb-like and user-facing where appropriate (`Index`, `Privacy`, etc.).

### Controllers and actions

- Return `IActionResult` for MVC actions unless a stronger type is beneficial.
- Keep controllers thin: orchestration in controller, logic in services (when services are introduced).
- Validate inputs and fail fast for invalid request states.
- Use attributes intentionally (`[ResponseCache]`, `[HttpGet]`, `[HttpPost]`, etc.).

### Models and view models

- Keep view models focused on UI/view needs.
- Prefer simple, immutable-by-default models when practical.
- Add validation attributes when accepting user input.
- Do not overload `ErrorViewModel`-style types with unrelated concerns.

### Error handling and logging

- Use global exception handling middleware configured in `Program.cs`.
- Do not swallow exceptions silently.
- Log errors with contextual data using `ILogger<T>`.
- Avoid logging sensitive data (credentials, secrets, personal data).
- For recoverable paths, return appropriate result/view instead of throwing.

### Dependency injection

- Register services in `Program.cs`.
- Prefer constructor injection over service locator patterns.
- Depend on abstractions for non-trivial services.

### Razor views and frontend assets

- Keep business logic out of `.cshtml`; use view models.
- Use tag helpers and built-in HTML helpers where appropriate.
- Keep static assets under `wwwroot/` and reference them via standard MVC conventions.

### Security basics

- Never commit secrets to source control.
- Prefer configuration via `appsettings.*` plus environment overrides.
- Use anti-forgery protection for form POST endpoints.
- Validate and encode untrusted input rendered in views.

## Agent Workflow Expectations

- Read relevant files before editing; preserve existing behavior unless change request says otherwise.
- Make minimal, targeted diffs.
- Do not refactor unrelated code in the same change.
- Update docs when behavior or usage changes.
- Run build after code changes; run tests when test projects exist.

## Pre-PR Checklist for Agents

- Project restores successfully.
- Build passes (`dotnet build`).
- Formatting pass is clean (`dotnet format --verify-no-changes` when feasible).
- No secrets or machine-specific artifacts added.
- Changes are consistent with naming and nullability rules above.

## Notes for Future Expansion

When test projects are introduced, extend this file with:

- Exact test project paths
- Required test data setup
- Integration vs unit test command examples
- CI-equivalent command sequence

Keep this file updated as the repository gains infrastructure (linters, analyzers, CI, or agent rules).
