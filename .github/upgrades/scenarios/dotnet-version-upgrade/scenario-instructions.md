# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0
- **Commit Strategy**: Single Commit at End

## Source Control
- **Source Branch**: master
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: Single Commit at End
- **Branch Sync**: Auto (Merge)

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

## Strategy
**Selected**: All-at-Once
**Rationale**: One SDK-style project on modern .NET with no dependencies, no incompatible APIs, and one recommended package update.

### Execution Constraints
- Upgrade the project framework and package references in one atomic pass.
- Restore dependencies after all project changes are applied.
- Build once and fix all compilation errors in one bounded pass.
- Run tests only after the atomic upgrade builds successfully.
- Validate the complete solution before committing.

## Key Decisions Log
- Upgrade the complete solution from .NET 8 to .NET 10 LTS.
- Use the confirmed All-at-Once strategy for the single-project solution.

## Build Tool Decisions
- **TestSMTP.csproj**: `dotnet build` (SDK-style console project targeting modern .NET with no special full-MSBuild requirements).
