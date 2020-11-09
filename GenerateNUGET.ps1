param(
[Switch]$skipBuild,
[Switch]$skipPublish)

#declaracao de funcoes
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

function ExecNugetPack {
    param (
        $nuspecPath
    )
	
	$returnMsg = $(nuget pack $nuspecPath -Symbols -OutputFileNamesWithoutVersion -SymbolPackageFormat snupkg)
  
	$packageName = GetPackageName($returnMsg)
	 
	 Write-Host 'Pacote ' $packageName ' gerado com sucesso.';
	
	
	
    Write-Output  $packageName
}

function PublishNugetIfNotSkipped {
    param (
        $packageName		
    )
    if($skipPublish -eq $False){
	
      Write-Host '';
	  Write-Host 'Enviando pacote ' $packageName ' para o nuget'
	  Write-Host '#######################################################################'
      dotnet nuget push --source "HabilTecnologia" --api-key az $packageName --interactive --skip-duplicate
	  Write-Host '#######################################################################'
	  Write-Host ''
    }
	else{
		Write-Host 'A publicacao para o nuget do pacote '$packageName' foi desabilitada pelo parametro -skipPublish';
		Write-Host '';
	}
}

#inico do script
 Write-Host '';
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
 Write-Host '';
 
 if($skipPublish -eq $False){
   Write-Host 'Adicionando autenticacao do feed do nuget...';
   Write-Host '#######################################################################'
   iex "& { $(irm https://aka.ms/install-artifacts-credprovider.ps1) } -AddNetfx"
   $returnMsg = $(dotnet nuget add source "https://pkgs.dev.azure.com/HabilTecnologia/_packaging/HabilTecnologia/nuget/v3/index.json" --name "HabilTecnologia")
   if($returnMsg.GetType().Name -eq 'Object[]'){
     Write-Host $returnMsg[0]
   }
   else {
     Write-Host $returnMsg
   }
   Write-Host '#######################################################################'
   
 }

 Write-Host '';
 Write-Host 'Gerando os pacotes do nuget....';

 $packageName = ExecNugetPack("Nuget\nfenfcedll\Zeus.Net.NFe.NFCe.nuspec")
 PublishNugetIfNotSkipped($packageName)

 $packageName = ExecNugetPack("NuGet\ctedll\Zeus.Net.CTe.nuspec")
 PublishNugetIfNotSkipped($packageName)

 $packageName = ExecNugetPack("NuGet\mdfedll\Zeus.Net.MDFe.nuspec")
 PublishNugetIfNotSkipped($packageName)

 Write-Host '';
 Write-Host 'Os pacotes nupkg gerados estarao na raiz do diretorio da solution.';
 Write-Host 'Processo concluido, aperte qualquer tecla para fechar.';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');






