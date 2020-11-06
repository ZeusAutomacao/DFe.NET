cd C:\Habil\Repo\DFe.NET
iex "& { $(irm https://aka.ms/install-artifacts-credprovider.ps1) } -AddNetfx"
New-Alias -Name vswhere -Value "${Env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
function GetPackageName {
    param (
        $nugetMessage
    )

    $nugetMessage = $nugetMessage.Split([Environment]::NewLine) | Select -Last 1
    $nugetMessage = $nugetMessage.substring($nugetMessage.IndexOf("'")+1)
    $packageName =  $nugetMessage.remove($nugetMessage.length-2,2) 
    $packageName =  $packageName.replace("snupkg", "nupkg")
    $packageName = $packageName.substring($packageName.lastIndexOf("\")+1)
    
    Write-Output $packageName 
}

$MSBuildPath = vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
$MSBuildPath = $MSBuildPath + "\MSBuild\Current\Bin\MsBuild.exe"
&$MSBuildPath "Zeus NFe.sln" /t:"Clean;Rebuild" -property:Configuration=Release
nuget sources add -Name "HabilTecnologia" -Source "https://pkgs.dev.azure.com/HabilTecnologia/_packaging/HabilTecnologia/nuget/v3/index.json"
$packageName = GetPackageName($(nuget pack Nuget\nfenfcedll\Zeus.Net.NFe.NFCe.nuspec -Symbols -SymbolPackageFormat snupkg))
dotnet nuget push --source "HabilTecnologia" --api-key az $packageName --interactive
$packageName = GetPackageName($(nuget pack NuGet\ctedll\Zeus.Net.CTe.nuspec -Symbols -SymbolPackageFormat snupkg))
dotnet nuget push --source "HabilTecnologia" --api-key az $packageName --interactive
$packageName = GetPackageName($(nuget pack NuGet\mdfedll\Zeus.Net.MDFe.nuspec -Symbols -SymbolPackageFormat snupkg))
dotnet nuget push --source "HabilTecnologia" --api-key az $packageName --interactive

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');






