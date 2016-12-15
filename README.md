Zeus.NFe.NFCe  [![Nuget count](http://img.shields.io/nuget/v/Zeus.Net.NFe.NFCe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
Zeus.MDFe  [![Nuget count](https://img.shields.io/nuget/v/Zeus.Net.MDFe.svg)](http://www.nuget.org/packages/Zeus.Net.MDFe/)

Zeus.Net.NFe.NFCe
=================
Grupo [Skype](https://join.skype.com/CJbtNPlvbycL) para discussão

Biblioteca gratuita para Geração de NFe 2.0 e 3.10 e NFCe 3.10 e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx

A biblioteca foi desenvolvida com o Visual Studio Community 2013 e é compatível com o Visual Studio Community 2015 e 2015 Update 1.
Está licenciada sobre a LGPL.

**Instruções para compilar a solução**
- No visual studio, abra o arquivo "Zeus NFe.sln", defina o "NFe.AppTeste" como projeto de inicialização, compile e execute;
- Os projetos NFe.Danfe.AppTeste e NFe.Danfe.Base utilizam o FastReports.NET;
- Uma versão demo do FastReport pode ser baixada em https://www.fast-report.com/pt/product/fast-report-net/;
- As dlls de runtime do FastReports.NET estão em NFe.Danfe.Fast\Dlls.

**Projetos na Solução**
- NFe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Classes: Biblioteca com todas as classes para montagem da NFe/NFCe, de acordo com os manuais vigentes até 14/04/2015;
- NFe.Integracao: Aplicação console que fornece acesso aos recursos do Zeus via linha de comando.
- NFe.Servicos: Biblioteca que implementa o consumo e retorno dos serviços da NFe/NFCe;
- NFe.Utils: Biblioteca com classes de apoio e extensão para todas as demais bibliotecas;
- NFe.Wsdl: Biblioteca com as classes de serviço wsdl.;
- NFe.Danfe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Danfe.Base: Biblioteca base para todas as bibliotecas que implementam a impressão do DANFE, idepentende do fornecedor de relatórios utilizado;
- NFe.Danfe.Fast: Biblioteca responsável por montar a impressão do DANFE em FastReports.

**DANFE**
- Foi implementado em 09/09/2015 a impressão do NFCe em Fast Reports (https://www.fast-report.com/pt/product/fast-report-net/);
- Os recursos implementados na biblioteca de impressão foram: Visualização e impressão direta, além dos recursos de exportação para pdf, xls, doc, etc. do próprio Fast Reports;
- A impressão segue rigorosamente o Manual de Especificacoes Tecnicas do DANFE NFC-e QRCode Versao 3.2);
- Obs: Visando abranger o maior número possível de impressoras térmicas, a impressão é feita via spooler do windows. A impressão térmica via spooler, dependendo da impressora, pode sair com má qualidade. Para sanar isso, no relatório são utilizadas duas fontes condensadas que possuem boa legibilidade em tamanho pequeno, a saber a OpenSans e UbuntuCondensed, ambas de uso livre podendo ser obtidas em https://www.google.com/fonts;
- As fontes estão anexadas ao projeto em NFe.Impressao\NFCe\Fontes;
- Instale as fontes informadas no PC que for imprimir o DANFE da NFCe;
- Impressão testada e funcionando 100% nas impressoras Bematech MP-4200, Daruma DR700 e Epson TM-81 e TM-20.

**Exemplo de impressão do DANFE da NFCe utilizando a biblioteca NFe.Danfe.Fast:**

```cs
var proc = new nfeProc().CarregarDeArquivoXml(Caminho_do_arquivo_XML);
var danfe = new DanfeFrNfce(proc, new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, null/*Logomarca em byte[]*/), "00001", "XXXXXXXXXXXXXXXXXXXXXXXXXX");
danfe.Visualizar();
//danfe.Imprimir();
//danfe.ExibirDesign();

```

**Atenção:**
Quaisquer dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github!

**Telas do aplicativo NFe.AppTeste (demonstração de Uso da biblioteca):**

![](http://www.zeusautomacao.com.br/imagens/git/01.png)
![](http://www.zeusautomacao.com.br/imagens/git/02.png)
![](http://www.zeusautomacao.com.br/imagens/git/03.png)
![](http://www.zeusautomacao.com.br/imagens/git/04.png)
![](http://www.zeusautomacao.com.br/imagens/git/05.png)


**Telas do aplicativo NFe.Danfe.AppTeste (demonstração de Uso da biblioteca NFe.Danfe.Fast):**

![](http://www.zeusautomacao.com.br/imagens/git/07.png)
![](http://www.zeusautomacao.com.br/imagens/git/08.png)
![](http://www.zeusautomacao.com.br/imagens/git/09.png)
![](http://www.zeusautomacao.com.br/imagens/git/10.png)
![](http://www.zeusautomacao.com.br/imagens/git/11.jpg)
