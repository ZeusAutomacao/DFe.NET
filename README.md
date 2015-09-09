Zeus.Net.NFe.NFCe
=================

Biblioteca para Geração de NFe 2.0 e 3.10 e NFCe 3.10 e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx

A biblioteca foi desenvolvida com o Visual Studio Community 2013 e compatível com o Visual Studio Community 2015.

**Instruções para compilar a solução**
- No visual studio, abra o arquivo "Zeus NFe.sln", defina o "NFe.AppTeste" como projeto de inicialização, compile e execute.

**Projetos na Solução**
- NFe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Classes: Biblioteca com todas as classes para montagem da NFe/NFCe, de acordo com os manuais vigentes até 14/04/2015;
- NFe.Impressao: Biblioteca que implementa a impressão da NFe/NFCe; 
- NFe.Servicos: Biblioteca que implementa o consumo e retorno dos serviços da NFe/NFCe;
- NFe.Utils: Biblioteca com classes de apoio e extensão para todas as demais bibliotecas;
- NFe.Wsdl: Biblioteca com as classes de serviço wsdl. 

**DANFE**
- Foi implementado em 09/09/2015 a impressão do NFCe em Fast Reports.
- Os recursos implementados na biblioteca foram: Visualização, personalização fácil por parte do usuário e impressão direta, além dos recursos de exportação para pdf, xls, doc, etc. do próprio Fast Reports;
- A impressão segue rigorosamente o Manual de Especificacoes Tecnicas do DANFE NFC-e QRCode Versao 3.2).
Exemplo de impressão do DANFE da NFCe utilizando a bilbioteca:

```cs
var proc = new nfeProc().CarregarDeArquivoXml(Caminho_do_arquivo_XML);
var danfe = new DanfeFrNfce(proc, new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, "00001", "XXXXXXXXXXXXXXXXXXXXXXXXXX", null/*Logomarca em byte[]*/));
danfe.Visualizar();
//danfe.Imprimir();
//danfe.ExibirDesign();

```

**TODO:**
- [x] Implementar consumo do serviço NfeDownloadNF;
- [ ] Implementar envio síncrono na versão 3.10;
- [ ] Implementar envio de nfe compactada para a versão 3.10;
- [x] Implementar impressão do DANFE de NFCe. Concluído em 09/09/2015;
- [ ] Implementar impressão do DANFE de NFe;
- [ ] Implementar envio de emails;
- [ ] Implementar consumo do serviço NFeDistribuicaoDFe;
- [ ] Implementar consumo do serviço NfeConsultaDest;
- [ ] Implementar Evento de Pedido de Prorrogação da Suspensão do ICMS na Remessa para Industrialização (NT2015/001);
- [ ] Implementar "diversas atualizações e melhorias no Sistema da NF-e" (NT2015/002) entre 01/10/15(quando vai ser liberado em homologação) e 03/11/15(entrada em produção);
- [ ] Implementar "Cobrança do ICMS na Operação Interestadual" (NT2015/003) entre 01/10/15(quando vai ser liberado em homologação) e 03/11/15(entrada em produção);
- [ ] Implementar Serviço Administração do CSC para NFCe.
 
**Atenção:**
Quaisquer dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github!

**Telas do aplicativo de demonstração de Uso da biblioteca:**



![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/01.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/02.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/03.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/04.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n5.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n6.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n7.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n8.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n9.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/10.png "Tela Aplicativo de Demonstração")
