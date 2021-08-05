# Command-line parameters
param (
    [Parameter(ParameterSetName = 'c')]
    [string]$configuration = 'Debug'
)
# Start
Write-Output "Compiling all libraries: $configuration"
# Define all directories to build in order
$dir_gn = @(
    'Guilded.NET.Base'
    'Guilded.NET'
)
# Builds project in the specific path
function Build-Project {
    param (
        # Configuration type:
        # Debug, Release, ...
        [Parameter(Mandatory = $true)][string]$config,
        # Directory of the project to build
        [Parameter(Mandatory = $true)][string]$dir
    )
    # Invoke command
    $result = dotnet pack -c "$config" "$dir" *>&1
    # Exit code of that command
    $ex = $?
    # Checks if it failed or not
    if(!$?) {
        # If it did, throw an exception
        throw "Command failed."
    }
    # Return its output
    Write-Output "-------------" $result "-------------`n`n"
}
# Build all of them
foreach($dir in $dir_gn)
{
    Write-Output "Building $dir"
    # Builds the project
    Build-Project "$configuration" "./src/$dir"
}
# Tells us that it ended
Write-Output 'Ended'