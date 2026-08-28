# .NET Version Upgrade — Report

**Scenario:** Upgrade TestSMTP from .NET 8 to .NET 10 LTS using an all-at-once strategy.
**Outcome:** ✅ Fully completed
**Projects affected:** 1
**Tasks:** 1/1 completed

---

## Summary

The TestSMTP solution was upgraded from .NET 8 to .NET 10 LTS. The single SDK-style console project now targets `net10.0`, and its explicit `Microsoft.Extensions.Configuration.UserSecrets` dependency was updated to the assessment-recommended .NET 10 version. Restore, project and solution builds, output verification, and package vulnerability checks completed successfully with no errors or warnings.

---

## What Changed

### Packages

| Project | Package | Change | From → To |
|---------|---------|--------|-----------|
| `TestSMTP/TestSMTP.csproj` | `Microsoft.Extensions.Configuration.UserSecrets` | Updated | `8.0.0` → `10.0.11` |

### Project File Changes

- **Target framework** — Updated `TestSMTP/TestSMTP.csproj` from `net8.0` to `net10.0`.
- **Dependency alignment** — Updated the explicit UserSecrets package reference to the stable version selected for the .NET 10 upgrade.
- **Application code** — No source changes were required because the assessment found no source or binary API incompatibilities.

### Build and Tooling

- Confirmed the installed .NET SDK `10.0.400` supports the target framework.
- Confirmed no `global.json` constrains SDK selection.
- Used `dotnet build` for the SDK-style console project and complete solution.
- Added assessment, planning, task research, and validation artifacts under `.github/upgrades/scenarios/dotnet-version-upgrade/`.

### Git Commits

| SHA | Message |
|-----|---------|
| `5b5a20b` | `upgrade: move TestSMTP to .NET 10` |
| `e71f42d` | `Save work before starting .NET 10 upgrade` |

---

## Task Breakdown

- **`01-upgrade-testsmtp` — Upgrade and validate TestSMTP on .NET 10:** ✅ Updated the framework and package reference, then completed all applicable validation without issues. See [task.md](tasks/01-upgrade-testsmtp/task.md) and [progress-details.md](tasks/01-upgrade-testsmtp/progress-details.md).

---

## Decisions Made

- **Target .NET 10 LTS** — Upgrade the complete solution from .NET 8 to `net10.0`.
- **Use an all-at-once strategy** — The solution contains one modern SDK-style project with no project dependencies or incompatible APIs.
- **Use stable package versions** — Updated UserSecrets to `10.0.11`; the available .NET 11 preview package was outside the confirmed scope.
- **Use `dotnet build`** — No WPF, WinForms, .NET Framework, COM, VSIX, or other full-MSBuild requirements were detected.
- **Use a single end-of-upgrade commit** — Changes were committed on the `upgrade-dotnet-10` working branch.

---

## Build & Test Results

| Project | Restore | Build | Tests | Warnings | Vulnerabilities |
|---------|---------|-------|-------|----------|-----------------|
| `TestSMTP/TestSMTP.csproj` | ✅ Successful | ✅ Successful | N/A — no test project discovered | 0 | None reported |
| `TestSMTP.sln` | ✅ Successful | ✅ Successful | N/A — no test project discovered | 0 | None reported |

The build produced `TestSMTP/bin/Debug/net10.0/TestSMTP.dll`.

---

## Known Gaps & Follow-up Items

- **Automated tests** — No test project exists in the solution, so upgrade validation was limited to restore, compilation, output verification, and package vulnerability checks. No known upgrade issues remain.
