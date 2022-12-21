$exit_code = 0
$sln_root = $PSScriptRoot

function Invoke-Exit {
  Pop-Location -StackName 'build_locations'
  exit $exit_code
}
$ErrorActionPreference = 'Stop'

# Set location to solution root pushing it into named stack, so we could return to it before exit
Push-Location -LiteralPath $sln_root -StackName 'build_locations'

# Do not output useless dotnet warnings and stuff
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_NOLOGO = '1'

# Restore dotnet tools in solution
. dotnet tool restore
$exit_code = $LASTEXITCODE
if ($exit_code -ne 0) {
  # Early exit if dotnet tool restore failed
  Invoke-Exit
}

# Invoke Cake build using provided arguments
. dotnet cake build.cake @args
$exit_code = $LASTEXITCODE
Invoke-Exit
