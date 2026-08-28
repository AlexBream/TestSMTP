# 01-upgrade-testsmtp: Upgrade and validate TestSMTP on .NET 10

Verify that the .NET 10 SDK and solution-level configuration support the target framework, then upgrade `TestSMTP/TestSMTP.csproj` from `net8.0` to `net10.0`. Apply the assessment's recommended `Microsoft.Extensions.Configuration.UserSecrets` package update from 8.0.0 to 10.0.11 and address any restore or compilation issues caused by the atomic framework upgrade.

The assessment reports no incompatible packages, no source or binary API incompatibilities, and no project dependencies. Validate the complete solution after the framework and package changes, and run all available tests.

**Done when**: The solution targets .NET 10, restores successfully, builds with zero errors, all discovered tests pass, and package vulnerability checks report no known vulnerabilities.

## Research Findings

- **Affected project**: `TestSMTP/TestSMTP.csproj`, a single SDK-style console application with no project references and one source file.
- **Framework action**: Change `TargetFramework` from `net8.0` to `net10.0`. A compatible .NET 10 SDK is installed, and no `global.json` constrains SDK selection.
- **Package action**: Update the explicit `Microsoft.Extensions.Configuration.UserSecrets` reference from `8.0.0` to the assessment-recommended stable version `10.0.11`. The package-version lookup also returned an 11.0 preview, which is outside the confirmed .NET 10 scope and will not be used.
- **Assessment issues**: `Project.0002` requires the TFM change; `NuGet.0002` recommends the UserSecrets package update. No source or binary API incompatibilities or affected technologies were reported.
- **Dependencies and stubs**: No project dependencies and no `// STUB:` markers were found.
- **Tests**: No test projects were discovered in the solution.
- **Build approach**: Use `dotnet build` because the project is SDK-style and targets modern .NET without WPF, WinForms, .NET Framework, COM, VSIX, or other full-MSBuild requirements.
- **Decomposition**: Not required; this is one project with one atomic framework/package concern and no decision or dependency gates.

