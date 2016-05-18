Write-Host ""
Write-Host ""
Write-Host "====== [Tests] Extract files ======"
$shell = new-object -com shell.application
$zip = $shell.NameSpace("C:\projects\entityjoke\test-coverage.zip")
foreach($item in $zip.items())
{
  $shell.Namespace("C:\projects\entityjoke\").copyhere($item)
}

Write-Host "Extract files... Ok"
Write-Host ""

Write-Host "=========== [Tests] Run ==========="
.\test-coverage\OpenCover\OpenCover.Console.exe -target:nunit-console.exe "-targetargs:"".\src\EntityJokeTests\bin\$env:CONFIGURATION\EntityJokeTests.dll"" /noshadow" -register:user -output:.\test-coverage\opencoverCoverage.xml
Write-Host ""

Write-Host "======== [Tests] Reporter ========="
.\test-coverage\ReportGenerator\ReportGenerator.exe -reports:.\test-coverage\opencoverCoverage.xml -targetdir:coverage -verbosity:Info 

Write-Host ""

Write-Host "==== [Tests] Publish Coverage ====="
(Get-Content .\test-coverage\opencoverCoverage.xml) |  Foreach-Object { $_ -replace 'fullPath="c:\\projects\\entityjoke\\', 'fullPath="' } |  Set-Content .\test-coverage\opencoverCoverage.xml

$coveralls = (Resolve-Path ".\test-coverage\Coveralls\csmacnz.Coveralls.exe").ToString()
    
& $coveralls --opencover -i .\test-coverage\opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_BUILD_VERSION --serviceName appveyor

Write-Host "Publish Coverage... Ok"
Write-Host ""

Write-Host "Tests, Coverage and Reporter... Ok"