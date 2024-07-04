**Biblioteca em C# para emissão e impressão de NFe, NFCe, MDF-e e CT-e**

DFe.NET
==================

Biblioteca gratuita para Geração de NFe 3.10/4.00, NFCe 3.10/4.00, MDF-e 3.0 e CT-e 3.0/4.0 e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx, https://mdfe-portal.sefaz.rs.gov.br e www.cte.fazenda.gov.br/portal.

A biblioteca foi desenvolvida em **C#** utilizando Visual Studio Community 2022 com os SDKs net462, netstandard2.0 e net6.0 instalados.


## Versões suportadas:
|  Escopos  |  Frameworks Suportados  |
| ------------------- | ------------------- |
| NFe, NFCe, CTe, MDFe | .NET 4.6.2, .NET 4.7, .NET 4.7.1, .NET 4.7.2, .NET 4.8, .NetStandard 2.0, .NET 6.0 .NET 7.0 .NET 8.0 |
| Impressões com OpenFastReport (NFe, NFCe, CTe, MDFe) | ..NET 4.6.2, .NET 4.7, .NET 4.7.1, .NET 4.7.2, .NET 4.8, .NetStandard 2.0, .NET 6.0(windows+linux) .NET 7.0+(windows) |
| Impressões com FastReport(versão paga) (NFe, NFCe, CTe, MDFe) | .NET 4.6.2, .NET 4.7, .NET 4.7.1, .NET 4.7.2, .NET 4.8 .NetStandard 2.0, .NET 6.0 .NET 7.0+(windows) |
| Impressões com FastReport.Skia(versão paga) (NFe) | .NET 7.0+(windows+linux+mobile)  |

***Não temos suporte para .NetFramework 4.5.2 ou 4.5 ou menor. A Biblioteca irá seguir o [ciclo de vida de versões da microsoft](https://dotnet.microsoft.com/en-us/learn/dotnet/what-is-dotnet-framework), sendo retirado a compatibilidade de versoes específicas e antigas do .Net caso a microsoft retire seu suporte.***

Licenciada sobre a **LGPL** (https://pt.wikipedia.org/wiki/GNU_Lesser_General_Public_License).

Entre no nosso **Discord** -> https://discord.gg/EE4TGKAkkG no grupo falaremos não somente do Zeus.NET, mas também de varios outros frameworks que são criados no dia-a-dia.

## Pacotes Nugets
------------------
A melhor maneira de você ter a última versão do Zeus em seu projeto é utilizando os pacotes Nugets abaixo

[![Build status](https://github.com/ZeusAutomacao/DFe.NET/actions/workflows/DFe.NET_build.yml/badge.svg?branch=master)](https://github.com/ZeusAutomacao/DFe.NET/actions/workflows/DFe.NET_build.yml)
[![Issues](https://img.shields.io/github/issues/ZeusAutomacao/DFe.NET.svg?style=flat-square)](https://github.com/ZeusAutomacao/DFe.NET/issues)


[![Nuget downloads](https://img.shields.io/nuget/dt/Zeus.Net.NFe.NFCe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
[![Nuget count](http://img.shields.io/nuget/v/Zeus.Net.NFe.NFCe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
 Zeus.NFe.NFCe

[![Nuget downloads](https://img.shields.io/nuget/dt/Zeus.Net.MDFe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
[![Nuget count](https://img.shields.io/nuget/v/Zeus.Net.MDFe.svg)](http://www.nuget.org/packages/Zeus.Net.MDFe/)
 Zeus.MDFe  

[![Nuget downloads](https://img.shields.io/nuget/dt/Zeus.Net.CTe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
[![Nuget count](https://img.shields.io/nuget/v/Zeus.Net.CTe.svg)](http://www.nuget.org/packages/Zeus.Net.CTe/)
 Zeus.CTe  
 
## O que a biblioteca faz:
------------------
O projeto traz classes construídas de forma manual que extraem a complexidade dos XSDs. Com isso é possível preencher objetos nativos em .NET e gerar o XML na estrutura exigida para seu DFe, assim como o processo inverso de ler um XML de um DFe e obter objetos nativos em .NET.

Além da serialização e desserialização, o projeto também conta com os métodos de consumo dos webservices (consultar, transmitir, cancelar, inutilizar, etc.), ou seja, com a biblioteca você preenche um objeto nativo em .NET e transmite o seu DFe de forma totalmente transparente, sem se preocupar coma serialização e consumo do webservice.

A bibliteca também conta com a impressão dos DFes suportados, onde basicamente basta fazer a desserialização (ou preencher manualmente o(s) objeto(s) do DFe em questão) e chamar seu projeto de impressão.

Exemplo: 
```cs
var proc = new nfeProc().CarregarDeArquivoXml(Caminho_do_arquivo_XML);
var danfe = new DanfeFrNfce(proc, new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, null/*Logomarca em byte[]*/), "00001", "XXXXXXXXXXXXXXXXXXXXXXXXXX");
danfe.Visualizar();
//danfe.Imprimir();
//danfe.ExibirDesign();
```
## Como usar a ferramenta:
-----------
Antes de qualquer coisa leia os manuais e conheça à fundo o(s) projetos que pretende usar, entenda o que é um DFe (documento fiscal eletrônico), o que é um certificado, como funciona um webservice, o que é obrigatório ser informado no DFe que pretende emitir, entre outras informações. Isso vai ajudar na construção do seu software e na integração com a biblioteca.

Com o conhecimento prévio adquirido, agora você precisa estudar a biblioteca. A linguagem utilizada é C#, logo um conhecimento basico da linguagem pode te ajudar bastante, mesmo que você use apenas as dlls com VB.Net ou outra linguagem compatível.

Para facilitar o seus estudos a biblioteca oferece projetos do tipo DEMO, sendo eles (por ordem alfabética):
- *CTe.AppTeste:* Projeto em WPF para demonstração de uso do CTe;
- *CTe.AppTeste.NetCore:* Projeto em Console para demonstração de uso do CTe em .NET6;
- *CTe.Dacte.AppTeste:* Projeto em Winforms para demonstração de uso da impressão do CTe (necessita do FastReport.Net¹);
- *MDFe.AppTeste:* Projeto em WPF para demonstração de uso do MDFe;
- *MDFe.Damdfe.AppTeste:* Projeto em Winforms para demonstração de uso da impressão do MDFe (necessita do FastReport.Net¹);
- *NFe.AppTeste:* Projeto em WPF para demonstração de uso do NFe;
- *NFe.AppTeste.NetCore:* Projeto em Console para demonstração de uso do NFe e NFCe em .NET6;
- *NFe.Danfe.AppTeste.Fast:* Projeto em WPF para demonstração de uso da impressão da NFe e NFCe (A NFe e NFCe estão disponíveis em FastReport.Net¹. A NFC-e também está disponível de forma nativa, entretanto para O DEMO é necessária as DLLs do FastReport.Net¹. *A utilização do DANFe da NFCe de forma nativa fora do DEMO não depende do FastReports.Net*);
- *NFe.Danfe.AppTeste.OpenFast:* Projeto em Console em .NET6 para demonstração de uso de impressão da NFe, NFCe, como DANFE de xml não registrado e registrado ou Eventos como carta de correção e cancelamento.(A NFe utiliza o FastReport.OpenSource (https://github.com/FastReports/FastReport). Não é necessário nenhuma DLL externa, tudo está incluído no pacote nuget.);

## Impressão (FastReport):

- Exemplo no Projeto *NFe.Danfe.AppTeste.Fast*.
- A impressão de forma nativa (sem dependências de bibliotecas de terceiros) está disponível somente para a *NFCe*¹.
- O projeto conta também com a impressão em FastReport.Net¹ (https://www.fast-report.com/pt/product/fast-report-net/) para *NFe*, *NFCe²* _(térmica)_, *CTe* _(modal rodoviário)_ e *MDFe*.

>¹ As dlls do FastReport.Net disponibilizadas na biblioteca são da versão de demonstração³ do mesmo. A versão de demonstração coloca uma marca d'água "DEMO VERSION" na impressão do relatório. Se você possui licença FastReport.Net, substitua as dlls do FastReport.Net nos projetos NFe.Danfe.Fast\Dll, CTe.Dacte.Fast\DLLs e MDFe.Damdfe.Fast\Dlls pelas dlls de sua versão licenciada, antes de compilar sua aplicação para distribuição.

>² Obs: Visando abranger o maior número possível de impressoras térmicas, a impressão é feita via spooler do windows. A impressão térmica via spooler, dependendo da impressora, pode sair com má qualidade. Para sanar isso, no relatório são utilizadas duas fontes condensadas que possuem boa legibilidade em tamanho pequeno, a saber a OpenSans e UbuntuCondensed, ambas de uso livre podendo ser obtidas em https://www.google.com/fonts;
As fontes estão anexadas ao projeto em Shared.NFe.Danfe.Base\Fontes_;
Instale as fontes informadas no PC que for imprimir o DANFE da NFCe_;

## Impressão (OpenFastReport):

- Exemplos no Projeto *NFe.Danfe.AppTeste.OpenFast*.
- A impressão da NFe utiliza o FastReport.OpenSource (https://github.com/FastReports/FastReport), sendo ele instalado automatico ao utilizar o pacote nuget do Zeus.
- A impressão requer que o arquivo .frx seja indicado, ou seja, ao publicar os binarios de seu projeto os arquivos .frx devem estar juntos e passado o caminho do arquivo para que seja gerado a impressão.
- As saídas suportadas pelo FastReport.OpenSource são Stream ou Byte[], sendo elas em PDF, HTML e PNG.
- Para Impressão de NFCe tambem existe a seguinte opção ESC/POS (direto na impressora): https://github.com/marcosgerene/Gerene.DFe.EscPos.

#### Impressão em Linux (Nativo ou Docker)

Para a geração de impressão no Linux, alguns detalhes devem ser compreendidos...

Foi necessário a instalação da biblioteca **libgdiplus** 

- (Exemplo abaixo para Ubuntu 18.x)
	
> apt-get install -y --no-install-recommends libgdiplus libc6-dev

- (Exemplo abaixo para DockerFile Ubuntu 18.x)

> RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus libc6-dev \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*
    
Caso aconteça algum erro de System.OutOfMemoryException, utilize a versão 6.0.5, o código acima instala a versão padrão dependendo da versão do SO (6.0.4), para instalar a 6.0.5 utilize o seguinte código, nesse caso para Debian 10:

> RUN apt-get update && apt-get remove libgdiplus -y && apt autoremove -y && apt-get install -y apt-transport-https dirmngr gnupg ca-certificates \
 RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
 RUN echo "deb https://download.mono-project.com/repo/debian stable-buster main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
 RUN apt-get update && apt-get install -y libgdiplus=6.0.5-0xamarin1+debian10b1 \
 RUN apt show libgdiplus && rm -rf /var/lib/apt/lists/* 

Tambem foi necessário copiar algumas **fontes**, o relatório de Danfe atual utiliza **Times New Roman**, as fontes contem royalties e não existe repositório online com as mesmas, porem as mesmas estão disponíveis na pasta do windows. (fontes instaladas: times.ttf, timesbd.ttf, timesbi.ttf, timesi.ttf)

- (Exemplo abaixo para Ubuntu 18.x)

>sudo apt-get install ttf-mscorefonts-installer

- (Exemplo para Debian 10)

>apt-get install -y ttf-mscorefonts-installer fontconfig

- (Exemplo abaixo para DockerFile Ubuntu 18.x, porem diferente do exemploa anterior, copiando fontes ja existentes em uma pasta para a pasta de destino da imagem docker, não recomendamos essa opção por possíveis problemas porem a imagem de saída fica menor)

>RUN mkdir -p /usr/share/fonts/truetype/times \
COPY suapastadasfontes/* /usr/share/fonts/truetype/times/

O FastReport.OpenSource é pesado na geração de PDF, por isso não recomendamos o mesmo. Para melhor utilização de memoria/cpu, utilize o FastReport.OpenSource para geração em HTML. Na conversão de HTML para PDF, recomendamos o uso do projeto https://github.com/fpanaccia/Wkhtmltopdf.NetCore 

## Suporte:

O uso dessa biblioteca não lhe dá quaisquer garantias de suporte. No entanto se tiver dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github.

## Colaborando:

Mantenha seu projeto atualizado para evitar issues desnecessárias, reporte bugs e soluções para problemas comuns, compartilhe suas ideias de melhorias, se tiver condições ajude enviando um pull request ou responda issues de outros colegas.

Ao enviar um PR explique brevemente o que foi alterado e o motivo. Teste amplamente as alterações antes de submeter, não remova funcionalidades ou mude regras de métodos já existentes sem aviso prévio e com tempo para adaptações.

Colabore, a bibloteca é open source e seu sucesso depende unicamente de sua comunidade.

## Reconhecimento de código

Este projeto incorpora código e melhorias do projeto [ZeusFiscal](https://github.com/Hercules-NET/ZeusFiscal). Agradecemos a todos os contribuidores por suas valiosas contribuições. Para detalhes específicos sobre as mudanças e os autores, consulte o [histórico de commits](https://github.com/ZeusAutomacao/DFe.NET/commits/master/).