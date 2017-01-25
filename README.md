Zeus.NFe.NFCe  [![Nuget count](http://img.shields.io/nuget/v/Zeus.Net.NFe.NFCe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
Zeus.MDFe  [![Nuget count](https://img.shields.io/nuget/v/Zeus.Net.MDFe.svg)](http://www.nuget.org/packages/Zeus.Net.MDFe/)

Zeus.Net.NFe.NFCe
=================
Grupo [Skype](https://join.skype.com/CJbtNPlvbycL) para discussão

Biblioteca gratuita para Geração de NFe 2.0 e 3.10, NFCe 3.10 e MDF-e e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx e https://mdfe-portal.sefaz.rs.gov.br

A biblioteca foi desenvolvida com o Visual Studio Community 2013 e é compatível com o Visual Studio Community 2015 e 2015 Update 1.
Está licenciada sobre a LGPL.

**Instruções para compilar a solução**
- No visual studio, abra o arquivo "Zeus NFe.sln", defina o "NFe.AppTeste" como projeto de inicialização, compile e execute;

**Projetos na Solução**
- NFe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Classes: Biblioteca com todas as classes para montagem da NFe/NFCe, de acordo com os manuais vigentes até 14/04/2015;
- NFe.Integracao: Aplicação console que fornece acesso aos recursos do Zeus via linha de comando.
- NFe.Servicos: Biblioteca que implementa o consumo e retorno dos serviços da NFe/NFCe;
- NFe.Utils: Biblioteca com classes de apoio e extensão para todas as demais bibliotecas;
- NFe.Wsdl: Biblioteca com as classes de serviço wsdl.;
- NFe.Danfe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Danfe.Base: Biblioteca base para todas as bibliotecas que implementam a impressão do DANFE, independente do fornecedor de relatórios utilizado;
- NFe.Danfe.Fast: Biblioteca responsável por montar a impressão do DANFE em FastReports.

**DANFE**
- Foi implementado em 09/09/2015 a impressão do NFCe em FastReport.Net (https://www.fast-report.com/pt/product/fast-report-net/);
- Os recursos implementados na biblioteca de impressão foram: Visualização e impressão direta, além dos recursos de exportação para pdf, xls, doc, etc. do próprio FastReport.Net;
- A impressão segue rigorosamente o Manual de Especificacoes Tecnicas do DANFE NFC-e QRCode Versao 3.2);
- Obs: Visando abranger o maior número possível de impressoras térmicas, a impressão é feita via spooler do windows. A impressão térmica via spooler, dependendo da impressora, pode sair com má qualidade. Para sanar isso, no relatório são utilizadas duas fontes condensadas que possuem boa legibilidade em tamanho pequeno, a saber a OpenSans e UbuntuCondensed, ambas de uso livre podendo ser obtidas em https://www.google.com/fonts;
- As fontes estão anexadas ao projeto em NFe.Impressao\NFCe\Fontes;
- Instale as fontes informadas no PC que for imprimir o DANFE da NFCe;
- Impressão testada e funcionando 100% nas impressoras Bematech MP-4200, Daruma DR700 e Epson TM-81 e TM-20;
- As dlls do FastReport.Net disponibilizadas na biblioteca são da versão de demonstração do mesmo. A versão de demonstração coloca uma marca d'água "DEMO VERSION" na impressão do relatório. Se você possui licença FastReport.Net, substitua as dlls do FastReport.Net nos projetos NFe.Danfe.Fast\Dlls e MDFe.Damdfe.Fast\Dlls pelas dlls de sua versão licenciada, antes de compilar sua aplicação para distribuição.

**Exemplo de impressão do DANFE da NFCe utilizando a biblioteca NFe.Danfe.Fast:**

```cs
var proc = new nfeProc().CarregarDeArquivoXml(Caminho_do_arquivo_XML);
var danfe = new DanfeFrNfce(proc, new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, null/*Logomarca em byte[]*/), "00001", "XXXXXXXXXXXXXXXXXXXXXXXXXX");
danfe.Visualizar();
//danfe.Imprimir();
//danfe.ExibirDesign();

```

**Suporte:**

O uso dessa biblioteca não lhe dá quaisquer garantias de suporte. No entanto se tiver dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github ou pergunte no grupo skype.

**Diretrizes para contribuir com código**

A comunidade inteira agradece sua colaboração, mas afim de mantermos a qualidade da biblioteca, segue abaixo uma lista de diretrizes:

1 - A contribuição deve ser feita via pull request. Se não conhece o github dê uma olhada nesse tutorial: http://blog.da2k.com.br/2015/02/04/git-e-github-do-clone-ao-pull-request/ ou https://www.google.com.br/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=como+usar+o+github

2 - Os nomes das classes e atributos constantes no manual de orientação do contribuinte devem ser escritos conforme constam no manual, respeitando o case sensitive da documentação. Ex: Se o atributo/classe começar com caractere minúsculo, colocá-lo assim no c#;

3 - Os nomes de métodos/atributos e classes de apoio devem ser escritos em português;

4 - Todas as classes/atributos/métodos devem ser documentados no formato xml. A documentação dos atributos e classes do projeto CTe.Classes deve conter o código e descrição conforme consta no manual de orientação do contribuinte e conforme pode se ver no projeto NFe.Classes;

5 - Se a documentação XML dos atributos/classes/métodos fizer referência a um objeto, usar a tag <see cref=""/>. Ex:
```cs
/// <summary>
/// Obtém um objeto contendo o certificado digital
/// <para>Se for informado <see cref="ConfiguracaoCertificado.Arquivo"/>, 
/// o certificado digital será obtido pelo método <see cref="ObterDeArquivo(string,string)"/>,
/// senão será obtido pelo método <see cref="ObterDoRepositorio()"/> </para>
/// <para>Para liberar os recursos do certificado, após seu uso, invoque o método <see cref="X509Certificate2.Reset()"/></para>
/// </summary>
```

6 - Todas as classes deve conter um comentário no cabeçalho, informando sobre a licença e os direitos de uso;

7 - O código deve ser compatível com o .NET 4.0 e VS2013 em diante (pode ser revisado no futuro);

8 - Pulls requests não são anexados automaticamente ao projeto, eles estarão sujeitos à avaliação prévia antes de integrar a bilbiotca;

9 - Antes de iniciar uma implementação, informe-se na comunidade aqui no git ou no skype, pois outro pode já estar desenvolvendo-a.


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
