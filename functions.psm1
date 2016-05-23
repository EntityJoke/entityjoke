function RestorePackages{
    Write "`n== [Build]Restore Nuget Packages =="
    Write "Installing Nuget Packages of EntityJoke.sln"
    nuget restore .\src\EntityJoke.sln -Verbosity Quiet
    Write "Completed installation of Nuget Packages`n"
}

function InstallPackage{
    param($namePackage)
    Write "`nInstalling $namePackage"
    nuget install $namePackage -ExcludeVersion -Verbosity Quiet -OutputDirectory .\test-coverage
    Write "Completed installation of $namePackage"
}

function InstallPackages{
    Write "`n===== [Tests]Install Packages ====="
    InstallPackage("OpenCover")
    InstallPackage("ReportGenerator")
    InstallPackage("Coveralls.net")
}

function ExecuteTests(){
    Write "`n=========== [Tests] Run ==========="
    $openCover = (Resolve-Path ".\test-coverage\OpenCover\tools\OpenCover.Console.exe").ToString()
    & $openCover -target:nunit-console.exe "-targetargs:"".\src\EntityJokeTests\bin\$env:CONFIGURATION\EntityJokeTests.dll"" /noshadow" -register:user -output:.\test-coverage\opencoverCoverage.xml -filter:+[EntityJoke]*
}

function GenerateReport(){
    Write "`n======== [Tests] Reporter ========="
    $reportGenerator = (Resolve-Path ".\test-coverage\ReportGenerator\tools\ReportGenerator.exe").ToString()
    & $reportGenerator -reports:.\test-coverage\opencoverCoverage.xml -targetdir:coverage -verbosity:Info
}

function PublishCoverage(){
    Write "`n===== [Tests]Publish Coverage ====="
    (Get-Content .\test-coverage\opencoverCoverage.xml) |  Foreach-Object { $_ -replace 'fullPath="c:\\projects\\entityjoke\\', 'fullPath="' } |  Set-Content .\test-coverage\opencoverCoverage.xml

    $coveralls = (Resolve-Path ".\test-coverage\coveralls.net\tools\csmacnz.Coveralls.exe").ToString()
    & $coveralls --opencover -i .\test-coverage\opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_BUILD_VERSION --serviceName appveyor
}

function RevertCommit(){
    Write "`n=======[Tests]Revert Commit ======="
    git config --global credential.helper store
    Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:access_token):x-oauth-basic@github.com`n"
    git config --global user.email "$env:BUILD_USER_EMAIL"
    git config --global user.name "$env:BUILD_USER_NAME"
    #git revert $env:APPVEYOR_REPO_COMMIT --no-edit
    git status
    git reset HEAD --hard
    rm -r .\coverage
    rm -r .\teste-coverage
    git status
    git remote show origin
    git revert 8d7bbd2225131c36ab354f9d31cf1c4f4a947b94 --no-edit
    git push origin develop
    #git push origin $env:APPVEYOR_REPO_BRANCH 
}

function VerifyFailedTests(){
    Write "`n====[Tests]Verify Failed Tests ===="
    $failures = Select-String -Path .\TestResult.xml -Pattern "result=`"Failure"
    if($failures -ne $null){
      RevertCommit
    }else{
      Write "No failures"
    }
}