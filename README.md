[![Nuget count](http://img.shields.io/nuget/v/Zeus.Net.NFe.NFCe.svg)](http://www.nuget.org/packages/Zeus.Net.NFe.NFCe/)
Zeus.Net.NFe.NFCe
=================

Biblioteca gratuita para Geração de NFe 2.0 e 3.10 e NFCe 3.10 e consumo dos serviços necessários à sua manutenção, conforme descritos em http://www.nfe.fazenda.gov.br/portal/principal.aspx

A biblioteca foi desenvolvida com o Visual Studio Community 2013 e é compatível com o Visual Studio Community 2015 e 2015 Update 1.
Está licenciada sobre a LGPL.

**Instruções para compilar a solução**
- No visual studio, abra o arquivo "Zeus NFe.sln", defina o "NFe.AppTeste" como projeto de inicialização, compile e execute.

**Projetos na Solução**
- NFe.AppTeste: Aplicação em wpf com demonstração de uso da biblioteca;
- NFe.Classes: Biblioteca com todas as classes para montagem da NFe/NFCe, de acordo com os manuais vigentes até 14/04/2015;
- NFe.Integracao: Aplicação console que fornece acesso aos recursos do Zeus via linha de comando.
- NFe.Servicos: Biblioteca que implementa o consumo e retorno dos serviços da NFe/NFCe;
- NFe.Utils: Biblioteca com classes de apoio e extensão para todas as demais bibliotecas;
- NFe.Wsdl: Biblioteca com as classes de serviço wsdl. 

**Impressão do DANFE da NFCe em Fast-Reports**
- Ver a biblioteca Zeus.Net.NFe.NFCe.DANFE, disponível em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe.DANFE


**TODO:**
- [x] Implementar consumo do serviço NfeDownloadNF;
- [ ] Implementar envio síncrono na versão 3.10;
- [x] Implementar envio de nfe compactada para a versão 3.10;
- [x] Implementar envio de emails (Concluído em 25/06/2016)
- [ ] Implementar consumo do serviço NFeDistribuicaoDFe;
- [ ] ~~Implementar consumo do serviço NfeConsultaDest;~~ Desativado em 02/02/15 pela NT 2014.002, Versão 1.01, de Agosto 2014.
- [ ] Implementar Evento de Pedido de Prorrogação da Suspensão do ICMS na Remessa para Industrialização (NT2015/001);
- [x] Implementar "diversas atualizações e melhorias no Sistema da NF-e" (NT2015/002) entre 01/10/15 e 03/11/15(entrada em produção);
- [x] Implementar "Cobrança do ICMS na Operação Interestadual" (NT2015/003) entre 01/10/15 e 03/11/15(entrada em produção);
- [x] Implementar Serviço Administração do CSC para NFCe (implementado em 05/04/2016 por https://github.com/rodrigomartins50);
- [ ] Aceitar certificado digital A1 em base64;
- [ ] Revisar urls para qr-code de acordo com link (http://nfce.encat.org/desenvolvedor/qrcode/) divulgado na NT 2015/002, versão 1.41, publicada em 26/08/2016;
- [ ] Implementar consumo do serviço RecepcaoEvento – Manifestação do Destinatário.
 
**Atenção:**
Quaisquer dúvidas a respeito do uso desta biblioteca, abra um novo Issue aqui mesmo no github!

**Telas do aplicativo de demonstração de Uso da biblioteca:**

![](http://www.zeusautomacao.com.br/imagens/git/01.png)
![](http://www.zeusautomacao.com.br/imagens/git/02.png)
![](http://www.zeusautomacao.com.br/imagens/git/03.png)
![](http://www.zeusautomacao.com.br/imagens/git/04.png)
![](http://www.zeusautomacao.com.br/imagens/git/05.png)
