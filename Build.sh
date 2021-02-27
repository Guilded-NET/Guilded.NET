# Start
echo "Compiling all libraries"
# Define all directories to build in order
DirGn=("Guilded.NET.Objects" "Guilded.NET.API" "Guilded.NET")
# Build all of them
for i in "${DirGn[@]}"
do
    # Tell us what it is building at the moment
    echo "Building $i"
    echo "---------------"
    # Build & pack that library
    dotnet pack "./src/$i" -c Release
    # If it failed
    if [ $? -ne 0 ]; then
        # Tell us that it failed
        echo "Failed to compile $i. Stopping the process"
        # And break the loop
        break
    fi
    # Tell us that it is done
    echo "---------------"
    echo "Build completed"
done
# Tells us that building ended
echo "Ended"