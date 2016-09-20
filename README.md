[![Nuget count](http://img.shields.io/nuget/v/Zeus.Net.NFe.NFCe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
Zeus.Net.NFe.NFCe
=================

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


**TODO:**
- [x] Implementar consumo do serviço NfeDownloadNF;
- [ ] Implementar envio síncrono na versão 3.10;
- [x] Implementar envio de nfe compactada para a versão 3.10;
- [x] Implementar envio de emails (Concluído em 25/06/2016)
- [ ] Implementar consumo do serviço NFeDistribuicaoDFe (quase pronto por https://github.com/ernanisp);
- [ ] ~~Implementar consumo do serviço NfeConsultaDest;~~ Desativado em 02/02/15 pela NT 2014.002, Versão 1.01, de Agosto 2014.
- [ ] Implementar Evento de Pedido de Prorrogação da Suspensão do ICMS na Remessa para Industrialização (NT2015/001);
- [x] Implementar "diversas atualizações e melhorias no Sistema da NF-e" (NT2015/002) entre 01/10/15 e 03/11/15(entrada em produção);
- [x] Implementar "Cobrança do ICMS na Operação Interestadual" (NT2015/003) entre 01/10/15 e 03/11/15(entrada em produção);
- [x] Implementar Serviço Administração do CSC para NFCe (implementado em 05/04/2016 por https://github.com/rodrigomartins50);
- [ ] Aceitar certificado digital A1 em base64;
- [ ] Revisar urls para qr-code de acordo com link (http://nfce.encat.org/desenvolvedor/qrcode/) divulgado na NT 2015/002, versão 1.41, publicada em 26/08/2016;
- [ ] Implementar consumo do serviço RecepcaoEvento – Manifestação do Destinatário (quase pronto por https://github.com/ernanisp);
- ***TODO DANFE:***
- [x] Implementar impressão do DANFE de NFCe Mini. Concluído em 09/09/2015;
- [ ] Implementar impressão do DANFE de NFCe A4;
- [ ] Implementar impressão do DANFE de NFe (modelo 55);
- [ ] Implementar possíveis mudanças no Manual de Padrões Técnicos do DANFE-NFC-e e QR Code, versão 3.3 que será obrigatório a
 partir de  01/09/2016;
- [ ] Implementar possíveis mudanças no Manual de Padrões Técnicos do DANFE-NFC-e e QR Code, versão 4.0;
- [ ] Alterações no DANFE de NFCe (Adicionar opção para definir o tamanho da logomarca);
- [ ] Implementar impressão do DANFE sem dependências de bibliotecas de terceiros;
- [x] Adicionar opção para determinar se o desconto por item deve ser impresso.

 
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
