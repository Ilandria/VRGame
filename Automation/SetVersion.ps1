param($unityExe)

$projectRoot = Split-Path -Path $PSScriptRoot -Parent
$previousLocation = Get-Location

Set-Location $projectRoot
$semVer = $(GitVersion | ConvertFrom-Json).FullSemVer
Set-Location $previousLocation

Write-Output "Setting version for $projectRoot to $semVer"
& $unityExe -quit -batchMode -projectPath $projectRoot -logFile - -executeMethod CCB.MechGame.Editor.Packaging.Builder.SetVersion -semVer $semVer | Write-Output