**Biblioteca em C# para emissão e impressão de NFe, NFCe, MDF-e e CT-e**

Novo Canal Para Integração da galera , Troca de ideias experiencias etc. => https://discord.gg/EE4TGKAkkG no grupo falaremos não somente do Zeus.NET, mas também de varios outros frameworks que são criados no dia a dia.

[![Build status](https://ci.appveyor.com/api/projects/status/7igb6s48sw2p95o3/branch/master?svg=true)](https://ci.appveyor.com/project/adeniltonbs/zeus-net-nfe-nfce/branch/master) 
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


DFe.NET
=================

Biblioteca gratuita para Geração de NFe 3.10/4.00, NFCe 3.10/4.00, MDF-e 3.0 e CT-e 3.0 e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx, https://mdfe-portal.sefaz.rs.gov.br e www.cte.fazenda.gov.br/portal.

A biblioteca foi desenvolvida em **C#** utilizando Visual Studio Community 2019. Atualmente é compatível com **.NetFramework 4.5** e **.NET5+/.NetCore/.NetStandard 2.0**.

Está licenciada sobre a *LGPL* (https://pt.wikipedia.org/wiki/GNU_Lesser_General_Public_License).

**O que a biblioteca faz:**
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

**Como usar a ferramenta:**
-----------
Antes de qualquer coisa leia os manuais e conheça à fundo o(s) projetos que pretende usar, entenda o que é um DFe (documento fiscal eletrônico), o que é um certificado, como funciona um webservice, o que é obrigatório ser informado no DFe que pretende emitir, entre outras informações. Isso vai ajudar na construção do seu software e na integração com a biblioteca.

Com o conhecimento prévio adquirido, agora você precisa estudar a biblioteca. A linguagem utilizada é C#, logo um conhecimento basico da linguagem pode te ajudar bastante, mesmo que você use apenas as dlls com VB.Net ou outra linguagem compatível.

Para facilitar o seus estudos a biblioteca oferece projetos do tipo DEMO, sendo eles (por ordem alfabética):
- *CTe.AppTeste:* Projeto em WPF para demonstração de uso do CTe;
- *CTe.AppTeste.NetCore:* Projeto em Console para demonstração de uso do CTe em .NET5;
- *CTe.Dacte.AppTeste:* Projeto em Winforms para demonstração de uso da impressão do CTe (necessita do FastReport.Net¹);
- *MDFe.AppTeste:* Projeto em WPF para demonstração de uso do MDFe;
- *MDFe.Damdfe.AppTeste:* Projeto em Winforms para demonstração de uso da impressão do MDFe (necessita do FastReport.Net¹);
- *NFe.AppTeste:* Projeto em WPF para demonstração de uso do NFe;
- *NFe.AppTeste.NetCore:* Projeto em Console para demonstração de uso do NFe e NFCe em .NET5;
- *NFe.Danfe.AppTeste:* Projeto em WPF para demonstração de uso da impressão da NFe e NFCe (A NFe e NFCe estão disponíveis em FastReport.Net¹. A NFC-e também está disponível de forma nativa, entretanto para O DEMO é necessária as DLLs do FastReport.Net¹. *A utilização do DANFe da NFCe de forma nativa fora do DEMO não depende do FastReports.Net*);
- *NFe.Danfe.AppTeste.NetCore:* Projeto em Console em .NET5 para demonstração de uso de impressão da NFe, NFCe, como DANFE de xml não registrado e registrado ou Eventos como carta de correção e cancelamento.(A NFe utiliza o FastReport.OpenSource (https://github.com/FastReports/FastReport). Não é necessário nenhuma DLL externa, tudo está incluído no pacote nuget.);

**Impressão (.NetFramework):**
----------
- Exemplo no Projeto *NFe.Danfe.AppTeste*.
- A impressão de forma nativa (sem dependências de bibliotecas de terceiros) está disponível somente para a *NFCe*¹.
- O projeto conta também com a impressão em FastReport.Net¹ (https://www.fast-report.com/pt/product/fast-report-net/) para *NFe*, *NFCe²* _(térmica)_, *CTe* _(modal rodoviário)_ e *MDFe*.

>¹ As dlls do FastReport.Net disponibilizadas na biblioteca são da versão de demonstração³ do mesmo. A versão de demonstração coloca uma marca d'água "DEMO VERSION" na impressão do relatório. Se você possui licença FastReport.Net, substitua as dlls do FastReport.Net nos projetos NFe.Danfe.Fast\Dll, CTe.Dacte.Fast\DLLs e MDFe.Damdfe.Fast\Dlls pelas dlls de sua versão licenciada, antes de compilar sua aplicação para distribuição.

>² Obs: Visando abranger o maior número possível de impressoras térmicas, a impressão é feita via spooler do windows. A impressão térmica via spooler, dependendo da impressora, pode sair com má qualidade. Para sanar isso, no relatório são utilizadas duas fontes condensadas que possuem boa legibilidade em tamanho pequeno, a saber a OpenSans e UbuntuCondensed, ambas de uso livre podendo ser obtidas em https://www.google.com/fonts;
As fontes estão anexadas ao projeto em Shared.NFe.Danfe.Base\Fontes_;
Instale as fontes informadas no PC que for imprimir o DANFE da NFCe_;

>³ Atualmente existe um esforço da comunidade para migrar o projeto para o .NetStandard (https://github.com/ZeusAutomacao/DFe.NET/issues/1001). Entre as mudanças, esta adicionar suporte ao Fast Reports Open Source (https://github.com/FastReports/FastReport). A principal limitação do FastReports nessa versão é não ter acesso à direct print, o que pode ser ruim para NFCe, mas pode ser facilmente contornado para os outros documentos, comentem na issue ideias e opiniões, e se possíve, colaborem com o branch.

**Impressão (.NET5+/.NetCore/.NetStandard):**
----------
- Exemplos no Projeto *NFe.Danfe.AppTeste.NetCore*.
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

Tambem foi necessário copiar algumas **fontes**, o relatório de Danfe atual utiliza **Times New Roman**, as fontes contem royalties e não existe repositório online com as mesmas, porem as mesmas estão disponíveis na pasta do windows. (fontes instaladas: times.ttf, timesbd.ttf, timesbi.ttf, timesi.ttf)

- (Exemplo abaixo para Ubuntu 18.x)

>sudo apt-get install ttf-mscorefonts-installer

- (Exemplo para Debian 10)

>apt-get install -y ttf-mscorefonts-installer fontconfig

- (Exemplo abaixo para DockerFile Ubuntu 18.x, porem diferente do exemploa anterior, copiando fontes ja existentes em uma pasta para a pasta de destino da imagem docker, não recomendamos essa opção por possíveis problemas porem a imagem de saída fica menor)

>RUN mkdir -p /usr/share/fonts/truetype/times \
COPY suapastadasfontes/* /usr/share/fonts/truetype/times/

O FastReport.OpenSource é pesado na geração de PDF, por isso não recomendamos o mesmo. Para melhor utilização de memoria/cpu, utilize o FastReport.OpenSource para geração em HTML. Na conversão de HTML para PDF, recomendamos o uso do projeto https://github.com/fpanaccia/Wkhtmltopdf.NetCore 

**Suporte:**
---------
O uso dessa biblioteca não lhe dá quaisquer garantias de suporte. No entanto se tiver dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github ou pergunte no grupo skype.

**Colaborando:**
---------
Mantenha seu projeto atualizado para evitar issues desnecessárias, reporte bugs e soluções para problemas comuns, compartilhe suas ideias de melhorias, se tiver condições ajude enviando um pull request ou responda issues de outros colegas.

Ao enviar um PR explique brevemente o que foi alterado e o motivo. Teste amplamente as alterações antes de submeter, não remova funcionalidades ou mude regras de métodos já existentes sem aviso prévio e com tempo para adaptações.

Colabore, a bibloteca é open source e seu sucesso depende unicamente de sua comunidade.
