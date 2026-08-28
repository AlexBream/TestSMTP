# Upgrade Options — TestSMTP

Assessment: 1 SDK-style project on net8.0, with low upgrade difficulty, no API incompatibilities, and one recommended package update.

## Strategy

### Upgrade Strategy
The single modern .NET project can be upgraded atomically without dependency sequencing.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade the complete solution in one atomic pass. |
| Top-Down | Upgrade entry-point applications first and temporarily multi-target shared libraries where required. |
