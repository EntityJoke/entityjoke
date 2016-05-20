Write "`n========= [Build] Install ========="
Write "Installing Nuget Packages of EntityJoke.sln"
nuget restore .\src\EntityJoke.sln -Verbosity Quiet
Write "Completed installation of Nuget Packages`n"
Write ""