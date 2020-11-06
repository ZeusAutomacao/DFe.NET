param(
[Switch]$skipBuild,
[Switch]$skipPublish)
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

function PublishNugetIfNotSkipped {
    param (
        $packageName		
    )
    if($skipPublish -eq $False){
      dotnet nuget push --source "HabilTecnologia" --api-key az $packageName --interactive
    }
	else{
		Write-Host 'A publicacao para o nuget do pacote '$packageName' foi desabilitada pelo parametro -skipPublish';
	}
}
if($skipBuild -eq $False){
   Write-Host 'Fazendo o build da solution....';
   New-Alias -Name vswhere -Value "${Env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
   $MSBuildPath = vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
   $MSBuildPath = $MSBuildPath + "\MSBuild\Current\Bin\MsBuild.exe"
   &$MSBuildPath "Zeus NFe.sln" /t:"Clean;Rebuild" -property:"Configuration=Release;IncludeSymbols=true" /noconsolelogger /nologo
}
else{
  Write-Host 'O build da solution foi desabilitado pelo parametro --skipBuild';		
}
if($skipPublish -eq $False){
  Write-Host 'Adicionando autenticacao do feed do nuget...';
  iex "& { $(irm https://aka.ms/install-artifacts-credprovider.ps1) } -AddNetfx"
  # nuget sources add -Name "HabilTecnologia" -Source "https://pkgs.dev.azure.com/HabilTecnologia/_packaging/HabilTecnologia/nuget/v3/index.json"
  $returnMsg = dotnet nuget add source "https://pkgs.dev.azure.com/HabilTecnologia/_packaging/HabilTecnologia/nuget/v3/index.json" --name "HabilTecnologia"
  if($rMsg.GetType().Name -eq 'Object[]'){
    Write-Host $rMsg[0]
  }
  else {
    Write-Host $rMsg
  }
   
}

Write-Host 'Gerando os pacotes do nuget....';

$packageName = GetPackageName($(nuget pack Nuget\nfenfcedll\Zeus.Net.NFe.NFCe.nuspec -Symbols -SymbolPackageFormat snupkg))
PublishNugetIfNotSkipped($packageName)

$packageName = GetPackageName($(nuget pack NuGet\ctedll\Zeus.Net.CTe.nuspec -Symbols -SymbolPackageFormat snupkg))
PublishNugetIfNotSkipped($packageName)

$packageName = GetPackageName($(nuget pack NuGet\mdfedll\Zeus.Net.MDFe.nuspec -Symbols -SymbolPackageFormat snupkg))
PublishNugetIfNotSkipped($packageName)

Write-Host 'Os pacotes nupkg gerados estarao na raiz do diretorio da solution.';
Write-Host 'Processo concluído, aperte qualquer tecla para fechar.';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');






