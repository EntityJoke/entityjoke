Write-Host ""
Write-Host ""
Write-Host "==== [Tests] Install Packages ====="

Write-Host ""
Write-Host "Installing OpenCover 4.6.519"
nuget install OpenCover -Version 4.6.519 -Verbosity Quiet -OutputDirectory .\test-coverage
Write-Host "Completed installation of OpenCover 4.6.519"

Write-Host ""
Write-Host "Installing ReportGenerator 2.4.5"
nuget install ReportGenerator -Version 2.4.5 -Verbosity Quiet -OutputDirectory .\test-coverage
Write-Host "Completed installation ReportGenerator 2.4.5"

Write-Host ""
Write-Host "Installing Coveralls.net 0.6.0"
nuget install Coveralls.net -Version 0.6.0 -Verbosity Quiet -OutputDirectory .\test-coverage
Write-Host "Completed installation of Coveralls.net 0.6.0"

Write-Host ""
Write-Host "Packages... Ok"
Write-Host ""

Write-Host "=========== [Tests] Run ==========="
$openCover = (Resolve-Path ".\test-coverage\OpenCover.4.6.519\tools\OpenCover.Console.exe").ToString()
& $openCover -target:nunit-console.exe "-targetargs:"".\src\EntityJokeTests\bin\$env:CONFIGURATION\EntityJokeTests.dll"" /noshadow" -register:user -output:.\test-coverage\opencoverCoverage.xml
Write-Host ""

Write-Host "======== [Tests] Reporter ========="
$reportGenerator = (Resolve-Path ".\test-coverage\ReportGenerator.2.4.5.0\tools\ReportGenerator.exe").ToString()
& $reportGenerator -reports:.\test-coverage\opencoverCoverage.xml -targetdir:coverage -verbosity:Info 

Write-Host ""

Write-Host "===== [Tests]Publish Coverage ====="
(Get-Content .\test-coverage\opencoverCoverage.xml) |  Foreach-Object { $_ -replace 'fullPath="c:\\projects\\entityjoke\\', 'fullPath="' } |  Set-Content .\test-coverage\opencoverCoverage.xml

$coveralls = (Resolve-Path ".\test-coverage\coveralls.net.0.6.0\tools\csmacnz.Coveralls.exe").ToString()
& $coveralls --opencover -i .\test-coverage\opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_BUILD_VERSION --serviceName appveyor

Write-Host "Publish Coverage... Ok"
Write-Host ""

Write-Host "Tests, Coverage and Reporter... Ok"
Write-Host ""
