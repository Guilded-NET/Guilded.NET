# Command-line parameters
param (
    [Parameter(ParameterSetName = 'c')]
    [string]$configuration = 'Debug'
)
Write-Output "Compiling all libraries: $configuration"
$dir_gn = @(
    'Guilded.NET.Base'
    'Guilded.NET'
)
function Build-Project {
    param (
        # Configuration type:
        # Debug, Release, Docs, ...
        [Parameter(Mandatory = $true)][string]$config,
        # Directory of the project to build
        [Parameter(Mandatory = $true)][string]$dir
    )
    $result = dotnet build -c "$config" "$dir" *>&1
    # Whether the command has succeeded
    $ex = $?

    Write-Output "Succeeded: $ex`n-------------" $result "-------------`n`n"
    # Checks if it failed or not
    if(!$?) {
        throw "Command failed."
    }
}
# Build all of them
foreach($dir in $dir_gn)
{
    Write-Output "Building $dir"

    Build-Project "$configuration" "./src/$dir"
}
Write-Output 'Ended'