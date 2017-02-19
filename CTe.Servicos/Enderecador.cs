using System;
using System.Collections.Generic;
using System.Linq;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using versao = CTeDLL.Classes.Servicos.Tipos.versao;

namespace CTeDLL.Servicos
{
    /// <summary>
    ///     Classe com estrutura para armazenamento dos dados dos webservices.
    /// </summary>
    internal class EnderecoServico
    {
        public EnderecoServico(ServicoCTe servicoNFe, versao versao, TipoAmbiente tipoAmbiente, tpEmis tpEmis, Estado estado, ModeloDocumento modeloDocumento, string url)
        {
            ServicoNFe = servicoNFe;
            Versao = versao;
            TipoAmbiente = tipoAmbiente;
            TpEmis = tpEmis;
            Estado = estado;
            Url = url;
            ModeloDocumento = modeloDocumento;
        }

        public ServicoCTe ServicoNFe { get; private set; }
        public versao Versao { get; private set; }
        public TipoAmbiente TipoAmbiente { get; private set; }
        public tpEmis TpEmis { get; private set; }
        public Estado Estado { get; private set; }
        public ModeloDocumento ModeloDocumento { get; private set; }
        public string Url { get; private set; }
    }


    public static class Enderecador
    {
        /*private static readonly List<EnderecoServico> ListaEnderecos;

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

            var emissaoComum = new List<TipoEmissao> {TipoEmissao.teNormal, TipoEmissao.teEPEC, TipoEmissao.teFSDA};

            var versaoDoisETres = new List<VersaoServico> {VersaoServico.ve200, VersaoServico.ve300};

            var svspEstados = new List<Estado> {Estado.AP, Estado.PE, Estado.RR};

            var svrsEstadosConsultaCadastro = new List<Estado> {Estado.AC, Estado.RN, Estado.PB, Estado.SC};

            var svrsEstadosDemaisServicos = new List<Estado>
            {   Estado.AC, Estado.AL, Estado.AM, Estado.BA, Estado.CE, Estado.DF, Estado.ES, Estado.GO, Estado.MA,
                Estado.PA, Estado.PB, Estado.PI, Estado.RJ, Estado.RN, Estado.RO, Estado.SC, Estado.SE, Estado.TO };

            var svcspEstados = new List<Estado> { Estado.AC, Estado.AL, Estado.AM, Estado.BA, Estado.CE, Estado.DF, Estado.ES,
                 Estado.GO, Estado.MA, Estado.MG, Estado.PA, Estado.PB, Estado.PI, Estado.PR, Estado.RJ, Estado.RN, Estado.RO,
                 Estado.RS, Estado.SC, Estado.SE, Estado.TO };

            var svcRsEstados = new List<Estado> { Estado.AP, Estado.MS, Estado.MT, Estado.PE, Estado.RR, Estado.SP };

            var todosOsEstados = Enum.GetValues(typeof (Estado)).Cast<Estado>().ToList();

            var todosOsModelos = Enum.GetValues(typeof (ModeloDocumento)).Cast<ModeloDocumento>().ToList();

            var todosOsAmbientes = Enum.GetValues(typeof (TipoAmbiente)).Cast<TipoAmbiente>().ToList();

            var eventoCceCanc = new List<ServicoCTe> { ServicoCTe.RecepcaoEventoCartaCorrecao, ServicoCTe.RecepcaoEventoCancelmento };

            #endregion

            #region AC

            //AC usa SRVS para CTe

            #endregion

            #region AL

            //AL usa SRVS para CTe

            #endregion

            #region AP

            //AL usa SVSP para CTe

            #endregion

            #region AM

            //AM usa SVRS para CTe

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaCadastro, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeConsulta2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeStatusServico2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeInutilizacao2"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaCadastro, versao, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.CTeOS, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao2"));

                #endregion
            }

            #endregion

            #endregion

            #region BA

            //BA usa SVRS para CTe

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaCadastro, versao, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                if (emissao != TipoEmissao.teEPEC)
                    foreach (var servicoNFe in eventoCceCanc)
                        endServico.AddRange(
                            versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, versao, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.CTe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaCadastro, versao, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                if (emissao != TipoEmissao.teEPEC)
                    foreach (var servicoNFe in eventoCceCanc)
                        endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, versao, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.CTe, "https://nfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));

                #endregion
            }

            #endregion

            #endregion

            #region CE

            //CE usa SVRS para CTe

            #endregion

            #region DF

            //DF usa SVRS para CTe

            #endregion

            #region ES

            //ES usa SVRS para CTe

            #endregion

            #region GO

            //GO usa SVRS para CTe

            #endregion

            #region MA

            //MA usa SVRS para CTe

            #endregion

            #region MG

            //MG servidor próprio

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaCadastro, versao, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MG, ModeloDocumento.CTe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"));
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/CteRecepcao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/CteRetRecepcao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/CteInutilizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/CteConsulta"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/CteStatusServico"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/CteRecepcao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.MG, ModeloDocumento.CTe, "https://cte.fazenda.mg.gov.br/cte/services/CteRetRecepcao"));
            }

            #endregion

            #endregion

            #region MS

            //MS ainda não implementou a NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/CadConsultaCadastro2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteInutilizacao, versao, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaProtocolo, versao, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteStatusServico, versao, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MS, ModeloDocumento.CTe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetAutorizacao"));
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteRecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteRecepcao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteRetRecepcao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CadConsultaCadastro"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteInutilizacao")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteConsulta")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteStatusServico")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteRecepcao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.MS, ModeloDocumento.CTe, "https://producao.cte.ms.gov.br/ws/CteRetRecepcao"));
            }

            #endregion

            #endregion

            #region MT

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaCadastro, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTeOS, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTeOS, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTeOS, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTeOS, "https://homologacao.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTeOS, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.CTeOS, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews/services/CteRecepcao"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews/services/CteRetRecepcao"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteInutilizacao, versao, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews/services/CteInutilizacao")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteConsultaProtocolo, versao, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews/services/CteConsulta")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteStatusServico, versao, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews/services/CteStatusServico")));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews2/services/CteRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews/services/CteRecepcao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.CTe, "https://cte.sefaz.mt.gov.br/ctews/services/CteRetRecepcao?wsdl"));
            }

            #endregion

            #endregion

            #region PA

            //PA usa SVRSpara CTe

            #endregion

            #region PB

            //PA usa SVRS para CTe

            #endregion

            #region PE

            //PE usa SVSP para CTe

            #endregion

            #region PI

            //PI usa SVRS para CTe

            #endregion

            #region PR

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaCadastro, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeConsulta3"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3"));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "https://cte.fazenda.pr.gov.br/cte/CteRecepcao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "https://cte.fazenda.pr.gov.br/cte/CteRetRecepcao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "https://cte.fazenda.pr.gov.br/cte/CteInutilizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "https://cte.fazenda.pr.gov.br/cte/CteConsulta?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "	https://cte.fazenda.pr.gov.br/cte/CteStatusServico?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "	https://cte.fazenda.pr.gov.br/cte/CteRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "https://cte.fazenda.pr.gov.br/cte/CteRecepcao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.CTe, "https://cte.fazenda.pr.gov.br/cte/CteRetRecepcao?wsdl"));
            }

            #endregion

            #endregion

            #region RJ

            //RJ usa SVRS para CTe

            #endregion

            #region RN

            //RN usa SVRS para CTe

            #endregion

            #region RO

            //RO usa SVRS para CTe

            #endregion

            #region RS

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteDownloadNF, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeDownloadNF/nfeDownloadNF.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaDest, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeConsultaDest/nfeConsultaDest.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTeOS, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTeOS, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTeOS, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTeOS, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTeOS, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.CTeOS, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "	https://cte.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cterecepcaoevento/cterecepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/ctestatusservico/CteStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "	https://cte.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx"));
            }

            #endregion

            #endregion

            #region RR

            //RR usa SVSP para CTe

            #endregion

            #region SC

            //SC usa SVRS para CTe

            #endregion

            #region SE

            //SE usa SVRS para CTe

            #endregion

            #region SP

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaCadastro, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teEPEC)
                    endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                else
                    endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoEpec, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTeOS, "https://homologacao.nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.CTeOS, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRetRecepcao.asmx"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteweb/services/cteRecepcaoEvento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteInutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx"));
                endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRetRecepcao.asmx"));
            }

            #endregion

            #endregion

            #region TO

            //TO usa SVRS para CTe

            #endregion

            #region SVSP

            //Estados que usam SVSP para emissão de CTe

            #region Homologação

            foreach (var estado in svspEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    if (emissao != TipoEmissao.teEPEC)
                        endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://hom.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://hom.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://hom.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://hom.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteDownloadNF, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://hom.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://hom.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://hom.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svspEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/CteRetRecepcao.asmx"));
                    if (emissao != TipoEmissao.teEPEC)
                        endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteweb/services/cteRecepcaoEvento.asmx")));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteInutilizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/CteConsulta.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/CteStatusServico.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://nfe.fazenda.sp.gov.br/cteWEB/services/CteRetRecepcao.asmx"));
                }
            }

            #endregion

            #endregion

            #region SVRS

            #region Homologação

            foreach (var estado in svrsEstadosDemaisServicos)
            {
                foreach (var emissao in emissaoComum)
                {
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx"));
                    if (emissao != TipoEmissao.teEPEC)
                            endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcaoevento/cterecepcaoevento.asmx")));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve200, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/ctestatusservico/CteStatusServico.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.CTe, "https://cte-homologacao.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx"));
                }
            }


            #endregion

            #region Produção

            foreach (var estado in svrsEstadosDemaisServicos)
            {
                foreach (var emissao in emissaoComum)
                {
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetRecepcao, VersaoServico.ve200, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx"));
                    if (emissao != TipoEmissao.teEPEC)
                            endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cterecepcaoevento/cterecepcaoevento.asmx")));
                        endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx"));
                        endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/ctestatusservico/CteStatusServico.asmx"));
                        endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.CTe, "https://cte.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx"));
                }
            }

            #endregion

            #endregion

            #region SVC-RS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svcRsEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, ModeloDocumento.CTe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, ModeloDocumento.CTeOS, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcRsEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, ModeloDocumento.CTe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, ModeloDocumento.CTeOS, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoCTe.CteInutilizacao, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                }
            }

            #endregion

            #endregion

            #region SVC-SP

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svcspEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCSP, estado, ModeloDocumento.CTe, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teSVCSP, estado, ModeloDocumento.CTeOS, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCSP, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCSP, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCSP, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taHomologacao, TipoEmissao.teSVCSP, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcspEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCSP, estado, ModeloDocumento.CTe, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoCancelmento, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teSVCSP, estado, ModeloDocumento.CTeOS, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoCTe.CteConsultaProtocolo, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCSP, estado, modelo, "https://www.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteStatusServico, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCSP, estado, modelo, "https://www.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCSP, estado, modelo, "https://www.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoCTe.CteRetAutorizacao, VersaoServico.ve300, TipoAmbiente.taProducao, TipoEmissao.teSVCSP, estado, modelo, "https://www.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                }
            }

            #endregion

            #endregion

            #region Ambiente Nacional - (AN)

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in todosOsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                    if (!(estado == Estado.SP & modelo == ModeloDocumento.CTeOS))
                        endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoEpec, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teEPEC, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    //RS possui endereço próprio para manifestação do destinatário. Demais UFs usam o ambiente nacional
                    if (estado != Estado.RS & modelo != ModeloDocumento.CTeOS)
                        endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoManifestacaoDestinatario, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                    
                    //CE, RS e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & estado != Estado.RS & !svspEstados.Contains(estado))
                        endServico.AddRange(
                            versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteDownloadNF, versao, TipoAmbiente.taHomologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx")));

                    if (modelo != ModeloDocumento.CTeOS)
                    {
                        endServico.Add(new EnderecoServico(ServicoCTe.CteDistribuicaoDFe, VersaoServico.ve100, TipoAmbiente.taHomologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"));
                    }
                }
            }

            #endregion

            #region Produção

            foreach (var estado in todosOsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                    if (!(estado == Estado.SP & modelo == ModeloDocumento.CTeOS))
                        endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoEpec, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teEPEC, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                    
                    //RS possui endereço próprio para manifestação do destinatário. Demais UFs usam o ambiente nacional
                    if (estado != Estado.RS & modelo != ModeloDocumento.CTeOS)
                        endServico.Add(new EnderecoServico(ServicoCTe.RecepcaoEventoManifestacaoDestinatario, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                    
                    //CE, RS e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & estado != Estado.RS & !svspEstados.Contains(estado))
                        endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoCTe.CteDownloadNF, versao, TipoAmbiente.taProducao, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx")));
                    
                    if (modelo != ModeloDocumento.CTeOS)
                    {
                        endServico.Add(new EnderecoServico(ServicoCTe.CteDistribuicaoDFe, VersaoServico.ve100, TipoAmbiente.taProducao, TipoEmissao.teNormal, estado, modelo, "https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"));
                    }
                }
            }

            #endregion

            #endregion

            return endServico;
        }

        /// <summary>
        ///     Obtém a versão configurada para um determinado serviço.
        ///     A versão configurada para o serviço é armazenada em ConfiguracaoServico
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="cfgServico"></param>
        /// <returns>Retorna um item do enum VersaoServico, com a versão do serviço</returns>
        private static VersaoServico? ObterVersaoServico(ServicoCTe servico, ConfiguracaoServico cfgServico)
        {
            switch (servico)
            {
                case ServicoCTe.RecepcaoEventoCartaCorrecao:
                    return cfgServico.VersaoRecepcaoEvento;
                case ServicoCTe.RecepcaoEventoCancelmento:
                    return cfgServico.VersaoRecepcaoEvento;
                case ServicoCTe.RecepcaoEventoEpec:
                    return cfgServico.VersaoRecepcaoEvento;
                case ServicoCTe.RecepcaoEventoManifestacaoDestinatario:
                    return cfgServico.VersaoRecepcaoEvento;
                case ServicoCTe.CteRecepcao:
                    return cfgServico.VersaoCteRecepcao;
                case ServicoCTe.CteRetRecepcao:
                    return cfgServico.VersaoCteRetRecepcao;
                case ServicoCTe.CteConsultaCadastro:
                    return cfgServico.VersaoCteConsultaCadastro;
                case ServicoCTe.CteInutilizacao:
                    return cfgServico.VersaoCteInutilizacao;
                case ServicoCTe.CteConsultaProtocolo:
                    return cfgServico.VersaoCteConsultaProtocolo;
                case ServicoCTe.CteStatusServico:
                    return cfgServico.VersaoCteStatusServico;
                case ServicoCTe.CteAutorizacao:
                    return cfgServico.VersaoCteAutorizacao;
                case ServicoCTe.CteRetAutorizacao:
                    return cfgServico.VersaoCteRetAutorizacao;
                case ServicoCTe.CteDistribuicaoDFe:
                    return cfgServico.VersaoCteDistribuicaoDFe;
                case ServicoCTe.CteConsultaDest:
                    return cfgServico.VersaoCteConsultaDest;
                case ServicoCTe.CteDownloadNF:
                    return cfgServico.VersaoCteDownloadNF;
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
        private static string Erro(ServicoCTe servico, ConfiguracaoServico cfgServico)
        {
            return "Serviço " + servico + ", versão " + Auxiliar.VersaoServicoParaString(servico, ObterVersaoServico(servico, cfgServico)) + ", não disponível para a UF " + cfgServico.cUF + ", no ambiente de " + Auxiliar.TpAmbParaString(cfgServico.tpAmb) +
                   " para emissão tipo " + Auxiliar.TipoEmissaoParaString(cfgServico.tpEmis) + ", documento: " + Auxiliar.ModeloDocumentoParaString(cfgServico.ModeloDocumento) + "!";
        }

        /// <summary>
        ///     Obtém uma url a partir de uma lista armazenada em enderecoServico e povoada dinamicamente no create desta classe
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="tipoRecepcaoEvento"></param>
        /// <param name="cfgServico"></param>
        /// <returns></returns>
        public static string ObterUrlServico(ServicoCTe servico, ConfiguracaoServico cfgServico)
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
        }*/
    }
}
