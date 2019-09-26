param($unityExe = "D:\Program Files (x86-64)\Unity\2019.2.5f1\Editor\Unity.exe", $buildDropPath = "D:\Repos\MechGame\Builds")

& "$PSScriptRoot\SetVersion.ps1" -unityExe $unityExe
& "$PSScriptRoot\Build.ps1" -unityExe $unityExe -buildDropPath $buildDropPath