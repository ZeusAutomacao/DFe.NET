/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TipoAmbiente = NFe.Classes.Informacoes.Identificacao.Tipos.TipoAmbiente;

namespace NFe.Servicos
{
    /// <summary>
    ///     Classe com estrutura para armazenamento dos dados dos webservices.
    /// </summary>
    internal class EnderecoServico
    {
        public EnderecoServico(ServicoNFe servicoNFe, VersaoServico versaoServico, TipoAmbiente tipoAmbiente, TipoEmissao tipoEmissao, Estado estado, ModeloDocumento modeloDocumento, string url)
        {
            ServicoNFe = servicoNFe;
            VersaoServico = versaoServico;
            TipoAmbiente = tipoAmbiente;
            TipoEmissao = tipoEmissao;
            Estado = estado;
            Url = url;
            ModeloDocumento = modeloDocumento;
        }

        public ServicoNFe ServicoNFe { get; private set; }
        public VersaoServico VersaoServico { get; private set; }
        public TipoAmbiente TipoAmbiente { get; private set; }
        public TipoEmissao TipoEmissao { get; private set; }
        public Estado Estado { get; private set; }
        public ModeloDocumento ModeloDocumento { get; private set; }
        public string Url { get; private set; }
    }

    public static class Enderecador
    {
        private static readonly List<EnderecoServico> ListaEnderecos;

        /// <summary>
        ///     Adiciona as urls dos webservices de todos os estados
        ///     Obs: UFs que disponibilizaram urls diferentes para NFCe, até 04/05/2015: SVRS, AM, MT, PR, RS e SP
        /// </summary>
        static Enderecador()
        {
            ListaEnderecos = CarregarUrlsServicos();
        }

        /// <summary>
        ///     Adiciona na lista endServico as urls dos webservices para NFe e NFCe de todos os estados
        /// </summary>
        private static List<EnderecoServico> CarregarUrlsServicos()
        {
            var endServico = new List<EnderecoServico>();

            #region Listas

            var emissaoComum = new List<TipoEmissao> { TipoEmissao.teNormal, TipoEmissao.teEPEC, TipoEmissao.teFSIA, TipoEmissao.teFSDA, TipoEmissao.teOffLine };

            var versaoDoisETres = new List<VersaoServico> { VersaoServico.ve200, VersaoServico.ve310 };

            var svanEstados = new List<Estado> { Estado.MA, Estado.PA, Estado.PI };

            var svrsEstadosConsultaCadastro = new List<Estado> { Estado.AC, Estado.RN, Estado.PB, Estado.SC };

            var svrsEstadosDemaisServicos = new List<Estado>
            {
                Estado.AC,
                Estado.AL,
                Estado.AP,
                Estado.BA, //Somente NFCe. BA tem endereços próprios para NFe. Rev: 09/09/2015
                Estado.DF,
                Estado.ES,
                Estado.MA, //Somente NFCe. MA usa o SVAN para NFe. Rev: 09/09/2015
                Estado.PA, //Somente NFCe. PA usa o SVAN para NFe. Rev: 09/09/2015
                Estado.PB,
                Estado.PI, //Somente NFCe. PI usa o SVAN para NFe. Rev: 09/09/2015
                Estado.RJ,
                Estado.RN,
                Estado.RO,
                Estado.RR,
                Estado.SC,
                Estado.SE,
                Estado.TO
            };

            var svcanEstados = new List<Estado> { Estado.AC, Estado.AL, Estado.AP, Estado.DF, Estado.ES, Estado.MG, Estado.PB, Estado.RJ, Estado.RN, Estado.RO, Estado.RR, Estado.RS, Estado.SC, Estado.SE, Estado.SP, Estado.TO };

            var svcRsEstados = new List<Estado> { Estado.AM, Estado.BA, Estado.CE, Estado.GO, Estado.MA, Estado.MS, Estado.MT, Estado.PA, Estado.PE, Estado.PI, Estado.PR };

            var todosOsEstados = Enum.GetValues(typeof(Estado)).Cast<Estado>().ToList();

            var todosOsModelos = Enum.GetValues(typeof(ModeloDocumento)).Cast<ModeloDocumento>().ToList();

            var todosOsAmbientes = Enum.GetValues(typeof(TipoAmbiente)).Cast<TipoAmbiente>().ToList();

            var eventoCceCanc = new List<ServicoNFe> { ServicoNFe.RecepcaoEventoCartaCorrecao, ServicoNFe.RecepcaoEventoCancelmento };

            #endregion Listas

            #region AC

            //AC usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion AC

            #region AL

            //AL usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion AL

            #region AP

            //AL usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion AP

            #region AM

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico4"));

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento4")));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao4"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeConsulta2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeStatusServico2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeInutilizacao2"));

                //4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento4"));

                #endregion NFCe
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao2"));

                //4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento4"));

                #endregion NFCe
            }

            #endregion Produção

            #endregion AM

            #region BA

            //BA usa o NFCe da SVRS. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                if (emissao != TipoEmissao.teEPEC)
                    foreach (var servicoNFe in eventoCceCanc)
                        endServico.AddRange(
                            versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, versao, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx"));

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx")));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));

                #endregion NFe
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                if (emissao != TipoEmissao.teEPEC)
                    foreach (var servicoNFe in eventoCceCanc)
                        endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, versao, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "	https://nfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx"));

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx")));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));

                #endregion NFe
            }

            #endregion Produção

            #endregion BA

            #region CE

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeInutilizacao4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeStatusServico4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeAutorizacao4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRetAutorizacao4?WSDL"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfceh.sefaz.ce.gov.br/nfce/services/NfeAutorizacao?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfceh.sefaz.ce.gov.br/nfce/services/NfeRetAutorizacao?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfceh.sefaz.ce.gov.br/nfce/services/NfeInutilizacao2?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfceh.sefaz.ce.gov.br/nfce/services/NfeConsulta2?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfceh.sefaz.ce.gov.br/nfce/services/NfeStatusServico2?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfceh.sefaz.ce.gov.br/nfce/services/CadConsultaCadastro2?WSDL"));
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfceh.sefaz.ce.gov.br/nfce/services/RecepcaoEvento?WSDL")));

                #endregion NFCe
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfce.sefaz.ce.gov.br/nfce/services/NfeAutorizacao?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfce.sefaz.ce.gov.br/nfce/services/NfeRetAutorizacao?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfce.sefaz.ce.gov.br/nfce/services/NfeInutilizacao2?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfce.sefaz.ce.gov.br/nfce/services/NfeConsulta2?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfce.sefaz.ce.gov.br/nfce/services/NfeStatusServico2?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfce.sefaz.ce.gov.br/nfce/services/CadConsultaCadastro2?WSDL"));
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.CE, ModeloDocumento.NFCe, "https://nfce.sefaz.ce.gov.br/nfce/services/RecepcaoEvento?WSDL")));

                #endregion NFCe
            }

            #endregion Produção

            #endregion CE

            #region DF

            //DF usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion DF

            #region ES

            //ES usa SRVS para NFe e para NFCe, exceto o serviço NfeConsultaCadastro. Rev: 09/09/2015

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    foreach (var ambiente in todosOsAmbientes)
                    {
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, ambiente, emissao, Estado.ES, modelo, "https://app.sefaz.es.gov.br/ConsultaCadastroService/CadConsultaCadastro2.asmx"));
                    }
                }
            }

            #endregion ES

            #region GO

            //GO usa os mesmos endereços para NFe e para NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.GO, ModeloDocumento.NFe, "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.GO, ModeloDocumento.NFCe, "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl"));

                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl"));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl")));
                    endServico.AddRange(
                        versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfceAdministracaoCSC, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/CscNFCe?wsdl"));
                }
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.GO, ModeloDocumento.NFe, "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.GO, ModeloDocumento.NFCe, "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl"));

                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl"));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfceAdministracaoCSC, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/CscNFCe?wsdl​"));
                }
            }

            #endregion Produção

            #endregion GO

            #region MA

            //MA usa SVAN para NFe e SRVS para NFCe, exceto o serviço NfeConsultaCadastro. Rev: 09/09/2015

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    foreach (var ambiente in todosOsAmbientes)
                    {
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, ambiente, emissao, Estado.MA, modelo, "https://sistemas.sefaz.ma.gov.br/wscadastro/CadConsultaCadastro2?wsdl"));
                    }
                }
            }

            #endregion MA

            #region MG

            //MG ainda não implementou a NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"));
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeStatus2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"));
            }

            #endregion Produção

            #endregion MG

            #region MS

            //MS ainda não implementou a NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/CadConsultaCadastro2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetAutorizacao"));
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/CadConsultaCadastro2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetAutorizacao"));
            }

            #endregion Produção

            #endregion MS

            #region MT

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl"));

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl")));

                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl"));

                #endregion NFCe
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl")));

                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl"));

                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.ve100,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl"));

                //if (emissao != TipoEmissao.teEPEC)
                //    foreach (var servicoNFe in eventoCceCanc)
                //        endServico.AddRange(
                //            versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl")));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl"));

                #endregion NFCe
            }

            #endregion Produção

            #endregion MT

            #region PA

            //PA usa SVAN para NFe e SRVS para NFCe. Rev: 09/09/2015

            #endregion PA

            #region PB

            //PA usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion PB

            #region PE

            //PE ainda não implementou a NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl"));
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/CadConsultaCadastro2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl"));
            }

            #endregion Produção

            #endregion PE

            #region PI

            //PI usa SVAN para NFe e SRVS para NFCe. Rev: 09/09/2015

            #endregion PI

            #region PR

            //Rev: 09/09/2015

            //Em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#PR há indicação que a SEFAZ do PR tem dois endereços para o serviço NfeConsultaCadastro (versão 2.0 e 3.10)
            //No entanto em 29/11/2016 foi constatado que a url indicada para versão 2.0 desse serviço não está funcionando e a url indicada para versão 3.10 na verdade roda um serviço com versão 2.0

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl"));
                //endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeConsulta3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento"));

                #endregion NFCe
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl"));
                //endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeConsulta3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento"));

                #endregion NFCe
            }

            #endregion Produção

            #endregion PR

            #region RJ

            //RJ usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion RJ

            #region RN

            //RN usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion RN

            #region RO

            //RO usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion RO

            #region RS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                #endregion NFCe
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                #endregion NFCe
            }

            #endregion Produção

            #endregion RS

            #region RR

            //RR usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion RR

            #region SC

            //SC usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion SC

            #region SE

            //SE usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion SE

            #region SP

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx"));

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx")));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));

                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teEPEC)
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                else
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx"));

                if (emissao != TipoEmissao.teEPEC)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx"));
                }
                else
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.ve400, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));
                }

                #endregion NFCe
            }

            #endregion Homologação

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx"));

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx")));

                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx"));

                #endregion NFe

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));

                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teEPEC)
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                else
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));

                // 4.0
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx"));

                if (emissao != TipoEmissao.teEPEC)
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx"));
                else
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.ve400, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));

                #endregion NFCe
            }

            #endregion Produção

            #endregion SP

            #region TO

            //TO usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion TO

            #region SVAN

            //Nenhuma UF utiliza NFCe no SVAN. Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svanEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    if (emissao != TipoEmissao.teEPEC)
                        endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeDownloadNF, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                }
            }

            #endregion Homologação

            #region Produção

            foreach (var estado in svanEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    if (emissao != TipoEmissao.teEPEC)
                        endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeDownloadNF, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                }
            }

            #endregion Produção

            #endregion SVAN

            #region SVRS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svrsEstadosDemaisServicos)
            {
                foreach (var emissao in emissaoComum)
                {
                    #region NFe

                    if (estado != Estado.BA & estado != Estado.MA & estado != Estado.PA & estado != Estado.PI)
                    {
                        if (emissao != TipoEmissao.teEPEC)
                            endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    }

                    #endregion NFe

                    #region NFCe

                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                    #endregion NFCe
                }
            }

            foreach (var estado in svrsEstadosConsultaCadastro)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, estado, modelo, "https://cad-homologacao.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                    }
                }
            }

            #endregion Homologação

            #region Produção

            foreach (var estado in svrsEstadosDemaisServicos)
            {
                foreach (var emissao in emissaoComum)
                {
                    #region NFe

                    //Rev: 09/09/2015
                    if (estado != Estado.BA & estado != Estado.MA & estado != Estado.PA & estado != Estado.PI)
                    {
                        if (emissao != TipoEmissao.teEPEC)
                            endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    }

                    #endregion NFe

                    #region NFCe

                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                    #endregion NFCe
                }
            }

            foreach (var estado in svrsEstadosConsultaCadastro)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        //Na relação de serviços web em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#SVRS marca 1.0, mas o serviço na verdade é 2.0
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, estado, modelo, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                    }
                }
            }

            #endregion Produção

            #endregion SVRS

            #region SVC-AN

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svcanEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFe, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFCe, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                }
            }

            #endregion Homologação

            #region Produção

            foreach (var estado in svcanEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFe, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFCe, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                }
            }

            #endregion Produção

            #endregion SVC-AN

            #region SVC-RS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svcRsEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFCe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                }
            }

            #endregion Homologação

            #region Produção

            foreach (var estado in svcRsEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFCe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                }
            }

            #endregion Produção

            #endregion SVC-RS

            #region Ambiente Nacional - (AN)

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in todosOsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                    if (!(estado == Estado.SP & modelo == ModeloDocumento.NFCe))
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teEPEC, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoManifestacaoDestinatario, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    //CE e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & !svanEstados.Contains(estado))
                        endServico.AddRange(
                            versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.taHomologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx")));
                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeDistribuicaoDFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"));
                }
            }

            #endregion Homologação

            #region Produção

            foreach (var estado in todosOsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                    if (!(estado == Estado.SP & modelo == ModeloDocumento.NFCe))
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teEPEC, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoManifestacaoDestinatario, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    //CE e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & !svanEstados.Contains(estado))
                        endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.taProducao, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx")));
                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeDistribuicaoDFe, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teNormal, estado, modelo, "https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"));
                }
            }

            #endregion Produção

            #endregion Ambiente Nacional - (AN)

            return endServico;
        }

        /// <summary>
        ///     Obtém a versão configurada para um determinado serviço.
        ///     A versão configurada para o serviço é armazenada em ConfiguracaoServico
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="cfgServico"></param>
        /// <returns>Retorna um item do enum VersaoServico, com a versão do serviço</returns>
        private static VersaoServico? ObterVersaoServico(this ServicoNFe servico, ConfiguracaoServico cfgServico)
        {
            switch (servico)
            {
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                case ServicoNFe.RecepcaoEventoCancelmento:
                    return cfgServico.VersaoRecepcaoEventoCceCancelamento;

                case ServicoNFe.RecepcaoEventoEpec:
                    return cfgServico.VersaoRecepcaoEventoEpec;

                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    return cfgServico.VersaoRecepcaoEventoManifestacaoDestinatario;

                case ServicoNFe.NfeRecepcao:
                    return cfgServico.VersaoNfeRecepcao;

                case ServicoNFe.NfeRetRecepcao:
                    return cfgServico.VersaoNfeRetRecepcao;

                case ServicoNFe.NfeConsultaCadastro:
                    return cfgServico.VersaoNfeConsultaCadastro;

                case ServicoNFe.NfeInutilizacao:
                    return cfgServico.VersaoNfeInutilizacao;

                case ServicoNFe.NfeConsultaProtocolo:
                    return cfgServico.VersaoNfeConsultaProtocolo;

                case ServicoNFe.NfeStatusServico:
                    return cfgServico.VersaoNfeStatusServico;

                case ServicoNFe.NFeAutorizacao:
                    return cfgServico.VersaoNFeAutorizacao;

                case ServicoNFe.NFeRetAutorizacao:
                    return cfgServico.VersaoNFeRetAutorizacao;

                case ServicoNFe.NFeDistribuicaoDFe:
                    return cfgServico.VersaoNFeDistribuicaoDFe;

                case ServicoNFe.NfeConsultaDest:
                    return cfgServico.VersaoNfeConsultaDest;

                case ServicoNFe.NfeDownloadNF:
                    return cfgServico.VersaoNfeDownloadNF;

                case ServicoNFe.NfceAdministracaoCSC:
                    return cfgServico.VersaoNfceAministracaoCSC;
            }
            return null;
        }

        /// <summary>
        ///     Obtém uma string com a mensagem de erro.
        ///     Essa função é acionada quando não é encontrada uma url para os parâmetros informados  na função ObterUrlServico.
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="cfgServico"></param>
        /// <returns></returns>
        private static string Erro(ServicoNFe servico, ConfiguracaoServico cfgServico)
        {
            return "Serviço " + servico + ", versão " + servico.VersaoServicoParaString(servico.ObterVersaoServico(cfgServico)) + ", não disponível para a UF " + cfgServico.cUF + ", no ambiente de " + cfgServico.tpAmb.TpAmbParaString() +
                   " para emissão tipo " + cfgServico.tpEmis.TipoEmissaoParaString() + ", documento: " + cfgServico.ModeloDocumento.ModeloDocumentoParaString() + "!";
        }

        /// <summary>
        ///     Obtém uma url a partir de uma lista armazenada em enderecoServico e povoada dinamicamente no create desta classe
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="cfgServico"></param>
        /// <returns></returns>
        public static string ObterUrlServico(ServicoNFe servico, ConfiguracaoServico cfgServico)
        {
            var definicao = from d in ListaEnderecos
                            where d.Estado == cfgServico.cUF && d.ServicoNFe == servico && d.TipoAmbiente == cfgServico.tpAmb && d.TipoEmissao == cfgServico.tpEmis && d.VersaoServico == ObterVersaoServico(servico, cfgServico) && d.ModeloDocumento == cfgServico.ModeloDocumento
                            select d.Url;
            var listaRetorno = definicao as IList<string> ?? definicao.ToList();
            var qtdeRetorno = listaRetorno.Count();

            if (qtdeRetorno == 0)
                throw new Exception(Erro(servico, cfgServico));
            if (qtdeRetorno > 1)
                throw new Exception("A função ObterUrlServico obteve mais de um resultado!");
            return listaRetorno.FirstOrDefault();
        }
    }
}