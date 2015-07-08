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

using System;
using System.Collections.Generic;
using System.Linq;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils;

namespace NFe.Servicos
{
    /// <summary>
    ///     Classe com estrutura para armazenamento dos dados dos webservices.
    /// </summary>
    internal class DefinicaoWsdlCampos
    {
        public DefinicaoWsdlCampos(ServicoNFe servicoNFe, VersaoServico versaoServico, TipoAmbiente tipoAmbiente, TipoEmissao tipoEmissao, Estado estado, ModeloDocumento modeloDocumento, string url)
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


    public static class DefinicaoWsdl
    {
        private static readonly List<DefinicaoWsdlCampos> Definicoes = new List<DefinicaoWsdlCampos>();

        /// <summary>
        /// Adiciona as urls dos webservices de todos os estados
        /// Obs: UFs que disponibilizaram urls diferentes para NFCe, até 04/05/2015: SVRS, AM, MT, PR, RS e SP
        /// </summary>
        static DefinicaoWsdl()
        {
            #region Listas

            var emissaoComum = new List<TipoEmissao>
            {
                TipoEmissao.teNormal,
                TipoEmissao.teDPEC,
                TipoEmissao.teFSIA,
                TipoEmissao.teFSDA,
                TipoEmissao.teOffLine
            };

            var versaoDoisETres = new List<VersaoServico> {VersaoServico.ve200, VersaoServico.ve310};

            var svanEstados = new List<Estado> {Estado.MA, Estado.PA, Estado.PI};

            var svrsEstadosConsultaCadastro = new List<Estado> {Estado.AC, Estado.RN, Estado.PB, Estado.SC};

            var svrsEstadosDemaisServicos = new List<Estado>
            {
                Estado.AC,
                Estado.AL,
                Estado.AP,
                Estado.DF,
                Estado.PB,
                Estado.RJ,
                Estado.RN,
                Estado.RO,
                Estado.RR,
                Estado.SC,
                Estado.SE,
                Estado.TO
            };

            var svcanEstados = new List<Estado>
            {
                Estado.AC,
                Estado.AL,
                Estado.AP,
                Estado.DF,
                Estado.ES,
                Estado.MG,
                Estado.PB,
                Estado.RJ,
                Estado.RN,
                Estado.RO,
                Estado.RR,
                Estado.RS,
                Estado.SC,
                Estado.SE,
                Estado.SP,
                Estado.TO
            };

            var svcRsEstados = new List<Estado>
            {
                Estado.AM,
                Estado.BA,
                Estado.CE,
                Estado.GO,
                Estado.MA,
                Estado.MS,
                Estado.MT,
                Estado.PA,
                Estado.PE,
                Estado.PI,
                Estado.PR
            };

            var todosOsEstados = Enum.GetValues(typeof (Estado)).Cast<Estado>().ToList();

            var todosOsModelos = Enum.GetValues(typeof(ModeloDocumento)).Cast<ModeloDocumento>().ToList();

            #endregion

            #region AM

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                        "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                    "https://homnfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                    "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                        TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                        "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                        TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                        "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                        TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                        "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                        TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                        "https://homnfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));//Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                    "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFe, 
                    "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeAutorizacao"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetAutorizacao"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeConsulta2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRecepcao2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/RecepcaoEvento"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeStatusServico2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetRecepcao2"));                
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeInutilizacao2"));

                #endregion

            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                        "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                    "https://nfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                    "https://nfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                        TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                        "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                        TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                        "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                        TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                        "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                        TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                        "https://nfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));//Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                    "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFe,
                    "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRecepcao2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetRecepcao2"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.AM, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao2"));

                #endregion

            }


            #endregion

            #endregion

            #region BA

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                            "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, versao,
                                TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                                "https://hnfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.BA, modelo,
                        "https://hnfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                            "https://nfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, versao,
                                TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                                "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.BA, modelo,
                        "https://nfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));                    
                }
            }

            #endregion

            #endregion

            #region CE

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                            "https://nfeh.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                        "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                        "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                            "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                            "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                            "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                            "https://nfeh.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeDownloadNF, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                            "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                        "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.CE, modelo,
                        "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                            "https://nfe.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                        "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                        "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                            "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                            "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                            "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                            "https://nfe.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeDownloadNF, versao,
                            TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                            "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                        "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.CE, modelo,
                        "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl"));                    
                }
            }

            #endregion

            #endregion

            #region GO

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                            "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                        "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                        "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                            "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                            "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                            "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                            "https://homolog.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                        "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.GO, modelo,
                        "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                            "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                        "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                        "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                            "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                            "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                            "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                            "https://nfe.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                        "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.GO, modelo,
                        "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl"));                    
                }
            }

            #endregion

            #endregion

            #region MG

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                            "https://hnfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                        "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                        "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                            "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                            "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                            "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeStatusServico2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                            "https://hnfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                        "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.MG, modelo,
                        "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeStatus2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2"));
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve200,
                            TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                            "https://nfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.MG, modelo,
                        "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"));                    
                }
            }

            #endregion

            #endregion

            #region MA

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.MA, modelo,
                        "https://sistemas.sefaz.ma.gov.br/wscadastro/CadConsultaCadastro2?wsdl"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MA, modelo,
                        "https://sistemas.sefaz.ma.gov.br/wscadastro/CadConsultaCadastro2?wsdl"));                    
                }
            }

            #endregion

            #endregion

            #region MS

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                            "https://homologacao.nfe.ms.gov.br/homologacao/services2/RecepcaoEvento"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                        "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                        "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                        "https://homologacao.nfe.ms.gov.br/homologacao/services2/CadConsultaCadastro2"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                            "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeInutilizacao2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                            "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeConsulta2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                            "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeStatusServico2")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                        "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeAutorizacao"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.MS, modelo,
                        "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetAutorizacao"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                            "https://nfe.fazenda.ms.gov.br/producao/services2/RecepcaoEvento"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                        "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                        "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                        "https://nfe.fazenda.ms.gov.br/producao/services2/CadConsultaCadastro2"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                            "https://nfe.fazenda.ms.gov.br/producao/services2/NfeInutilizacao2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                            "https://nfe.fazenda.ms.gov.br/producao/services2/NfeConsulta2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                            "https://nfe.fazenda.ms.gov.br/producao/services2/NfeStatusServico2")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                        "https://nfe.fazenda.ms.gov.br/producao/services2/NfeAutorizacao"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.MS, modelo,
                        "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetAutorizacao"));                    
                }
            }

            #endregion

            #endregion

            #region MT

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                        "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve310, 
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFe,
                    "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://homologacao.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl"));

                #endregion

            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                    "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                    "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl"));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                        TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                        "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                        TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                        "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                        TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                        "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl")));
                Definicoes.AddRange(
                    versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                        TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                        "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl")));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, versao,
                            TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                            "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl")));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                    "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFe, 
                    "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl"));

                #endregion
                
                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.MT, ModeloDocumento.NFCe,
                    "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl"));

                #endregion
            }

            #endregion

            #endregion

            #region PE

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                            "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                        "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                        "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                            "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                            "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                            "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                        "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.PE, modelo,
                        "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                            "https://nfe.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                        "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                        "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                            TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                            "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                            "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                            "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, versao,
                            TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                            "https://nfe.sefaz.pe.gov.br/nfe-service/services/CadConsultaCadastro2")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                        "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.PE, modelo,
                        "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl"));                    
                }
            }

            #endregion

            #endregion

            #region PR

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                        "https://homologacao.nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                        "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeConsulta3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento"));

                #endregion


            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                        "https://nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                        "https://nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFe,
                    "https://nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.pr.gov.br/nfce/NFeConsulta3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.PR, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento"));

                #endregion
            }

            #endregion

            #endregion

            #region RS

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                        "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeDownloadNF, VersaoServico.ve100,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeDownloadNF/nfeDownloadNF.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaDest, VersaoServico.ve100,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeConsultaDest/nfeConsultaDest.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                #endregion

            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                        "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeDownloadNF, VersaoServico.ve100,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe.sefazrs.rs.gov.br/ws/nfeDownloadNF/nfeDownloadNF.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaDest, VersaoServico.ve100,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe.sefazrs.rs.gov.br/ws/nfeConsultaDest/nfeConsultaDest.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFe,
                    "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.RS, ModeloDocumento.NFCe,
                    "https://nfce.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                #endregion
            }

            #endregion

            #endregion

            #region SP

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/cadconsultacadastro2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeRecepcao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/NfeRetRecepcao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico2.asmx"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve200,
                        TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                        "https://homologacao.nfe.fazenda.sp.gov.br/eventosWEB/services/RecepcaoEvento.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                        "https://homologacao.nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe,
                        "https://homologacao.nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                else
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe,
                        "https://homologacao.nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));                
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taHomologacao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/nfeweb/services/cadconsultacadastro2.asmx"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve200,
                        TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                        "https://nfe.fazenda.sp.gov.br/eventosWEB/services/RecepcaoEvento.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx"));
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                        "https://nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFe,
                    "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));

                #endregion

                #region NFCe

                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teDPEC)
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe,
                        "https://nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                else
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe,
                        "https://nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));
                Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                    TipoAmbiente.taProducao, emissao, Estado.SP, ModeloDocumento.NFCe,
                    "https://nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));

                #endregion

            }

            #endregion

            #endregion

            #region SVAN

            #region Homologação

            foreach (var estado in svanEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        if (emissao != TipoEmissao.teDPEC)
                            Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                                TipoAmbiente.taHomologacao, emissao, estado, modelo,
                                "https://hom.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                                TipoAmbiente.taHomologacao, emissao, estado, modelo,
                                "https://hom.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx")));
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                                TipoAmbiente.taHomologacao, emissao, estado, modelo,
                                "https://hom.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx")));
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                                TipoAmbiente.taHomologacao, emissao, estado, modelo,
                                "https://hom.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx")));
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeDownloadNF, VersaoServico.ve310,
                            TipoAmbiente.taHomologacao, emissao, estado, modelo,
                            "https://hom.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx"));
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                            TipoAmbiente.taHomologacao, emissao, estado, modelo,
                            "https://hom.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                            TipoAmbiente.taHomologacao, emissao, estado, modelo,
                            "https://hom.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));                        
                    }
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svanEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        if (emissao != TipoEmissao.teDPEC)
                            Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                                TipoAmbiente.taProducao, emissao, estado, modelo,
                                "https://www.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
                                TipoAmbiente.taProducao, emissao, estado, modelo,
                                "https://www.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx")));
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                                TipoAmbiente.taProducao, emissao, estado, modelo,
                                "https://www.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx")));
                        Definicoes.AddRange(
                            versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                                TipoAmbiente.taProducao, emissao, estado, modelo,
                                "https://www.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx")));
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeDownloadNF, VersaoServico.ve310,
                            TipoAmbiente.taProducao, emissao, estado, modelo,
                            "https://www.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx"));
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                            TipoAmbiente.taProducao, emissao, estado, modelo,
                            "https://www.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                            TipoAmbiente.taProducao, emissao, estado, modelo,
                            "https://www.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));                        
                    }
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
                    #region NFe

                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe,
                            "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));

                    #endregion

                    #region NFCe

                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taHomologacao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                    #endregion
                      
                }
            }

            foreach (var estado in svrsEstadosConsultaCadastro)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200,
                            TipoAmbiente.taHomologacao, emissao, estado, modelo,
                            "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));                        
                    }
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svrsEstadosDemaisServicos)
            {
                foreach (var emissao in emissaoComum)
                {
                    #region NFe

                    if (emissao != TipoEmissao.teDPEC)
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe,
                            "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFe,
                        "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));

                    #endregion

                    #region NFCe

                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100, 
                        TipoAmbiente.taProducao, emissao, estado, ModeloDocumento.NFCe,
                        "https://nfce.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));

                    #endregion
                    
                }
            }

            foreach (var estado in svrsEstadosConsultaCadastro)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        //Na relação de serviços web em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#SVRS marca 1.0, mas o serviço na verdade é 2.0
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaCadastro, VersaoServico.ve200, 
                            TipoAmbiente.taProducao, emissao, estado, modelo,
                            "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));                        
                    }
                }
            }

            #endregion

            #endregion

            #region SCAN (Desativado)

            //#region Homologação

            //foreach (var estado in todosOsEstados)
            //{
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
            //        TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //        "https://hom.nfe.fazenda.gov.br/SCAN/RecepcaoEvento/RecepcaoEvento.asmx"));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
            //        TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //        "https://hom.nfe.fazenda.gov.br/SCAN/NfeRecepcao2/NfeRecepcao2.asmx"));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
            //        TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //        "https://hom.nfe.fazenda.gov.br/SCAN/NfeRetRecepcao2/NfeRetRecepcao2.asmx"));
            //    Definicoes.AddRange(
            //        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
            //            TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //            "https://hom.nfe.fazenda.gov.br/SCAN/NfeInutilizacao2/NfeInutilizacao2.asmx")));
            //    Definicoes.AddRange(
            //        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
            //            TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //            "https://hom.nfe.fazenda.gov.br/SCAN/NfeConsulta2/NfeConsulta2.asmx")));
            //    Definicoes.AddRange(
            //        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
            //            TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //            "https://hom.nfe.fazenda.gov.br/SCAN/NfeStatusServico2/NfeStatusServico2.asmx")));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
            //        TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //        "https://hom.nfe.fazenda.gov.br/SCAN/NfeAutorizacao/NfeAutorizacao.asmx"));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
            //        TipoAmbiente.taHomologacao, TipoEmissao.teSCAN, estado,
            //        "https://hom.nfe.fazenda.gov.br/SCAN/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
            //}

            //#endregion

            //#region Produção

            //foreach (var estado in Enum.GetValues(typeof(Estado)).Cast<Estado>())
            //{
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
            //        TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //        "https://www.scan.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRecepcao, VersaoServico.ve200,
            //        TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //        "https://www.scan.fazenda.gov.br/NfeRecepcao2/NfeRecepcao2.asmx"));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeRetRecepcao, VersaoServico.ve200,
            //        TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //        "https://www.scan.fazenda.gov.br/NfeRetRecepcao2/NfeRetRecepcao2.asmx"));
            //    Definicoes.AddRange(
            //        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, versao,
            //            TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //            "https://www.scan.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx")));
            //    Definicoes.AddRange(
            //        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
            //            TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //            "https://www.scan.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx")));
            //    Definicoes.AddRange(
            //        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
            //            TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //            "https://www.scan.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx")));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
            //        TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //        "https://www.scan.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
            //    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
            //        TipoAmbiente.taProducao, TipoEmissao.teSCAN, estado,
            //        "https://www.scan.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
            //}


            //#endregion

            #endregion

            #region SVC-AN

            #region Homologação

            foreach (var estado in svcanEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo,
                        "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo,
                            "https://hom.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo,
                            "https://hom.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo,
                        "https://hom.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCAN, estado, modelo,
                        "https://hom.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcanEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo,
                        "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, versao,
                            TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo,
                            "https://www.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx")));
                    Definicoes.AddRange(
                        versaoDoisETres.Select(versao => new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, versao,
                            TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo,
                            "https://www.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx")));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo,
                        "https://www.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCAN, estado, modelo,
                        "https://www.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));                    
                }
            }

            #endregion

            #endregion

            #region SVC-RS

            #region Homologação

            foreach (var estado in svcRsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taHomologacao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcRsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeInutilizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeConsultaProtocolo, VersaoServico.ve310,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NfeStatusServico, VersaoServico.ve310,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.NFeRetAutorizacao, VersaoServico.ve310,
                        TipoAmbiente.taProducao, TipoEmissao.teSVCRS, estado, modelo,
                        "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));                   
                }
            }

            #endregion

            #endregion

            #region Ambiente Nacional - (AN)

            #region Homologação

            foreach (var estado in todosOsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                    if (!(estado == Estado.SP & modelo == ModeloDocumento.NFCe))                    
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taHomologacao, TipoEmissao.teDPEC, estado, modelo,
                            "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));                    
                }
            }

            #endregion

            #region Produção

            foreach (var estado in todosOsEstados)
            {
                foreach (var modelo in todosOsModelos)
                {
                    //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                    if (!(estado == Estado.SP & modelo == ModeloDocumento.NFCe))                    
                        Definicoes.Add(new DefinicaoWsdlCampos(ServicoNFe.RecepcaoEvento, VersaoServico.ve100,
                            TipoAmbiente.taProducao, TipoEmissao.teDPEC, estado, modelo,
                            "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));                    
                }
            }

            #endregion

            #endregion
        }

        /// <summary>
        ///     Obtém a versão configurada para um determinado serviço.
        ///     A versão configurada para o serviço é armazenada em ConfiguracaoServico
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="cfgServico"></param>
        /// <returns>Retorna um item do enum VersaoServico, com a versão do serviço</returns>
        private static VersaoServico? ObterVersaoServico(ServicoNFe servico, ConfiguracaoServico cfgServico)
        {
            switch (servico)
            {
                case ServicoNFe.RecepcaoEvento:
                    return cfgServico.VersaoRecepcaoEvento;
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
            }
            return null;
        }

        /// <summary>
        ///     Obtém uma string com a mensagem de erro.
        ///     Essa função é acionada quando não é encontrada uma url para os parâmetros informados  na função ObterUrl.
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="cfgServico"></param>
        /// <returns></returns>
        private static string Erro(ServicoNFe servico, ConfiguracaoServico cfgServico)
        {
            return 
                "Serviço " + servico + 
                ", versão " + Auxiliar.VersaoServicoParaString(servico, ObterVersaoServico(servico, cfgServico)) + 
                ", não disponível para a UF " + cfgServico.cUF + 
                ", no ambiente de " + Auxiliar.TpAmbParaString(cfgServico.tpAmb) +
                " para emissão tipo " + Auxiliar.TipoEmissaoParaString(cfgServico.tpEmis) + 
                ", documento: " + Auxiliar.ModeloDocumentoParaString(cfgServico.ModeloDocumento) + "!";
        }

        /// <summary>
        ///     Obtém uma url a partir de uma lista armazenada em Definicoes e povoada dinamicamente no create desta classe
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="tipoRecepcaoEvento"></param>
        /// <param name="cfgServico"></param>
        /// <returns></returns>
        public static string ObterUrl(ServicoNFe servico, TipoRecepcaoEvento tipoRecepcaoEvento, ConfiguracaoServico cfgServico)
        {
            //Se o serviço for RecepcaoEvento e o tipo de emissão for dpec, a url deve ser a do ambiente nacional para EPEC e da propria sefaz para os demais casos
            var tipoEmissao = servico == ServicoNFe.RecepcaoEvento & cfgServico.tpEmis == TipoEmissao.teDPEC & tipoRecepcaoEvento != TipoRecepcaoEvento.Epec
                ? TipoEmissao.teNormal
                : cfgServico.tpEmis;

            var definicao = from d in Definicoes
                where d.Estado == cfgServico.cUF && d.ServicoNFe == servico && d.TipoAmbiente == cfgServico.tpAmb
                      && d.TipoEmissao == tipoEmissao 
                      && d.VersaoServico == ObterVersaoServico(servico, cfgServico)
                      && d.ModeloDocumento == cfgServico.ModeloDocumento
                select d.Url;
            var listaRetorno = definicao as IList<string> ?? definicao.ToList();
            var qtdeRetorno = listaRetorno.Count();

            if (qtdeRetorno == 0)
                throw new Exception(Erro(servico, cfgServico));
            if (qtdeRetorno > 1)
                throw new Exception("A função ObterUrl obteve mais de um resultado!");
            return listaRetorno.FirstOrDefault();
        }
    }
}