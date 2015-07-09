Zeus.Net.NFe.NFCe
=================

Biblioteca para Geração de NFe 2.0 e 3.10 e NFCe 3.10 e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx

A biblioteca foi desenvolvida com o Visual Studio Community 2013.

**Instruções para compilar a solução**
- No visual studio, abra o arquivo "Zeus NFe.sln", defina o "NFe.AppTeste" como projeto de inicialização, compile e execute.

**Projetos na Solução**
- NFe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Classes: Biblioteca com todas as classes para montagem da NFe/NFCe, de acordo com os manuais vigentes até 14/04/2015;
- NFe.Servicos: Biblioteca que implementa o consumo e retorno dos serviços da NFe/NFCe;
- NFe.Utils: Biblioteca com classes de apoio e extensão para todas as demais bibliotecas;
- NFe.Wsdl: Biblioteca com as classes de serviço wsdl. 
 
**TODO:**
- [x] Implementar consumo do serviço NfeDownloadNF;
- [ ] Implementar envio síncrono na versão 3.10;
- [ ] Implementar envio de nfe compactada para a versão 3.10;
- [ ] Implementar impressão de Danfes;
- [ ] Implementar envio de emails;
- [ ] Implementar consumo do serviço NFeDistribuicaoDFe;
- [ ] Implementar consumo do serviço NfeConsultaDest;
- [ ] Implementar Evento de Pedido de Prorrogação da Suspensão do ICMS na Remessa para Industrialização (NT2015/001).

**Atenção:**
Quaisquer dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github!

**Telas do aplicativo de demonstração de Uso da biblioteca:**

![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n1.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n2.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n3.png "Tela Aplicativo de Demonstração")
![Alt text](http://www.zeusautomacao.com.br/zeus/images/git/n4.png "Tela Aplicativo de Demonstração")
