param($unityExe, $buildDropPath)

$projectRoot = Split-Path -Path $PSScriptRoot -Parent

Write-Output "Building $projectRoot in $buildDropPath"
& $unityExe -quit -batchMode -projectPath $projectRoot -logFile - -executeMethod CCB.MechGame.Editor.Packaging.Builder.Build -buildDropPath $buildDropPath | Write-Output