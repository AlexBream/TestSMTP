# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade TestSMTP from .NET 8 to .NET 10 LTS.
**Scope**: One SDK-style application project with 70 lines of code and no project dependencies.

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: One project on modern .NET with no dependency graph, no incompatible APIs, and one recommended package update.

## Tasks

### 01-upgrade-testsmtp: Upgrade and validate TestSMTP on .NET 10

Verify that the .NET 10 SDK and solution-level configuration support the target framework, then upgrade `TestSMTP/TestSMTP.csproj` from `net8.0` to `net10.0`. Apply the assessment's recommended `Microsoft.Extensions.Configuration.UserSecrets` package update from 8.0.0 to 10.0.11 and address any restore or compilation issues caused by the atomic framework upgrade.

The assessment reports no incompatible packages, no source or binary API incompatibilities, and no project dependencies. Validate the complete solution after the framework and package changes, and run all available tests.

**Done when**: The solution targets .NET 10, restores successfully, builds with zero errors, all discovered tests pass, and package vulnerability checks report no known vulnerabilities.
