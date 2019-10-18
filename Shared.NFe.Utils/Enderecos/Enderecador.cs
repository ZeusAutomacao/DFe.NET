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
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Enderecos
{
    public static class Enderecador
    {
        private static readonly List<EnderecoServico> ListaEnderecos;

        /// <summary>
        ///     Adiciona as urls dos webservices de todos os estados
        ///     Obs: UFs que disponibilizaram urls diferentes para NFCe, até 04/05/2015: SVRS, AM, MT, PR, RS e SP
        /// </summary>
        static Enderecador()
        {
            ListaEnderecos = CarregarEnderecosServicos();
        }

        /// <summary>
        /// Lista de <see cref="Estado"/> que utilizam SVC-AN para NF-e
        /// </summary>
        /// <returns></returns>
        public static List<Estado> EstadosQueUsamSvcAnParaNfe()
        {
            return new List<Estado> { Estado.AC, Estado.AL, Estado.AP, Estado.DF, Estado.ES, Estado.MG, Estado.PB, Estado.RJ, Estado.RN, Estado.RO, Estado.RR, Estado.RS, Estado.SC, Estado.SE, Estado.SP, Estado.TO };
        }

        /// <summary>
        /// Lista de <see cref="Estado"/> que utilizam SVC-RS para NF-e
        /// </summary>
        /// <returns></returns>
        public static List<Estado> EstadosQueUsamSvcRsParaNfe()
        {
            return new List<Estado> { Estado.AM, Estado.BA, Estado.CE, Estado.GO, Estado.MA, Estado.MS, Estado.MT, Estado.PA, Estado.PE, Estado.PR };
        }

        /// <summary>
        /// Lista de <see cref="Estado"/> que usam SVAN para NF-e
        /// </summary>
        /// <returns></returns>
        public static List<Estado> EstadosQueUsamSvanParaNfe()
        {
            return new List<Estado> { Estado.MA };
        }

        /// <summary>
        ///     Adiciona na lista endServico as urls dos webservices para NFe e NFCe de todos os estados
        /// </summary>
        private static List<EnderecoServico> CarregarEnderecosServicos()
        {
            var endServico = new List<EnderecoServico>();

            var addServico = new Action<ServicoNFe[], VersaoServico[], TipoAmbiente, TipoEmissao, Estado, ModeloDocumento, string>(
                (servicosNFe, versoesServico, tipoAmbiente, tipoEmissao, estado, modeloDocumento, url) =>
                {
                    foreach (var servicoNFe in servicosNFe)
                    {
                        foreach (var versaoServico in versoesServico)
                        {
                            endServico.Add(new EnderecoServico(servicoNFe, versaoServico, tipoAmbiente, tipoEmissao, estado, modeloDocumento, url));
                        }
                    }
                });

            #region Listas

            var emissaoComum = new List<TipoEmissao> { TipoEmissao.teNormal, TipoEmissao.teEPEC, TipoEmissao.teFSIA, TipoEmissao.teFSDA, TipoEmissao.teOffLine };

            var versao1 = new[] { VersaoServico.Versao100 };

            var versao2 = new[] { VersaoServico.Versao200 };

            var versao3 = new[] { VersaoServico.Versao310 };

            var versao4 = new[] { VersaoServico.Versao400 };

            var versao2E3 = new[] { VersaoServico.Versao200, VersaoServico.Versao310 };

            var svanEstados = EstadosQueUsamSvanParaNfe();

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
                Estado.PA, //https://github.com/ZeusAutomacao/DFe.NET/issues/1025
                Estado.PB,
                Estado.PE, //Somente NFCe. PE tem endereços próprios para NFe. Rev: 01/12/2017
                Estado.PI,
                Estado.RJ,
                Estado.RN,
                Estado.RO,
                Estado.RR,
                Estado.SC,
                Estado.SE,
                Estado.TO
            };

            var svcanEstados = EstadosQueUsamSvcAnParaNfe();

            var svcRsEstados = EstadosQueUsamSvcRsParaNfe();

            var todosOsEstados = Enum.GetValues(typeof(Estado)).Cast<Estado>().ToList();

            var todosOsModelos = Enum.GetValues(typeof(ModeloDocumento)).Cast<ModeloDocumento>().ToList();

            var todosOsAmbientes = Enum.GetValues(typeof(TipoAmbiente)).Cast<TipoAmbiente>().ToList();

            var eventoCceCanc = new[] { ServicoNFe.RecepcaoEventoCartaCorrecao, ServicoNFe.RecepcaoEventoCancelmento };

            var hom = TipoAmbiente.Homologacao;

            var prod = TipoAmbiente.Producao;

            var nfe = ModeloDocumento.NFe;

            var nfce = ModeloDocumento.NFCe;

            #endregion

            #region AC

            //AC usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region AL

            //AL usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region AP

            //AL usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region AM

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2");
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico4");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.AM, nfe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao4");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetAutorizacao");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao3, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao3, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, Estado.AM, nfce, "https://homnfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento4");

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2");
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico4");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.AM, nfe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao4");
                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao3, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao3, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, Estado.AM, nfce, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento4");

                #endregion
            }

            #endregion

            #endregion

            #region BA

            //BA usa o NFCe da SVRS. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao2E3, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.BA, nfe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx");

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao2E3, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.BA, nfe, "https://nfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx");

                #endregion
            }

            #endregion

            #endregion

            #region CE

            //CE ainda não possui NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl");
                addServico(new[] { ServicoNFe.NfeDownloadNF }, versao2E3, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl");

                addServico(eventoCceCanc, versao4, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeInutilizacao4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeStatusServico4?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeAutorizacao4?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.CE, nfe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRetAutorizacao4?wsdl");
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl");
                addServico(new[] { ServicoNFe.NfeDownloadNF }, versao2E3, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeInutilizacao4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeStatusServico4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe4/services/CadConsultaCadastro4?wsdl");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeAutorizacao4?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.CE, nfe, "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRetAutorizacao4?wsdl");
            }

            #endregion

            #endregion

            #region DF

            //DF usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region ES

            //ES usa SRVS para NFe e para NFCe, exceto o serviço NfeConsultaCadastro. Rev: 09/09/2015

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    foreach (var ambiente in todosOsAmbientes)
                    {
                        addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, ambiente, emissao, Estado.ES, modelo, "https://app.sefaz.es.gov.br/ConsultaCadastroService/CadConsultaCadastro2.asmx");
                    }
                }
            }

            #endregion

            #region GO

            //GO usa os mesmos endereços para NFe e para NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.GO, nfe, "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, Estado.GO, nfce, "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl");

                foreach (var modelo in todosOsModelos)
                {
                    addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl");
                    addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl");
                    addServico(new[] { ServicoNFe.NfceAdministracaoCSC }, versao1, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/CscNFCe?wsdl");

                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeStatusServico4?wsdl");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeInutilizacao4?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/CadConsultaCadastro4?wsdl");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeAutorizacao4?wsdl");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeRetAutorizacao4?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl");
                    addServico(eventoCceCanc, versao4, hom, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl");
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.GO, nfe, "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, Estado.GO, nfce, "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl");

                foreach (var modelo in todosOsModelos)
                {
                    addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl");
                    addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl");
                    addServico(new[] { ServicoNFe.NfceAdministracaoCSC }, versao1, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/CscNFCe?wsdl​");

                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeInutilizacao4?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeStatusServico4?wsdl");
                    addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/CadConsultaCadastro4?wsdl");
                    addServico(eventoCceCanc, versao4, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeAutorizacao4?wsdl");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeRetAutorizacao4?wsdl");
                }
            }

            #endregion

            #endregion

            #region MA

            //MA usa SVAN para NFe e SRVS para NFCe, exceto o serviço NfeConsultaCadastro. Rev: 09/09/2015

            foreach (var emissao in emissaoComum)
            {
                foreach (var modelo in todosOsModelos)
                {
                    foreach (var ambiente in todosOsAmbientes)
                    {
                        addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, ambiente, emissao, Estado.MA, modelo, "https://sistemas.sefaz.ma.gov.br/wscadastro/CadConsultaCadastro2?wsdl");
                    }
                }
            }

            #endregion

            #region MG

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao");

                // NF-e
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.MG, nfe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4");

                // NFC-e
                //18/12/2018 - http://www.sped.fazenda.mg.gov.br/spedmg/nfce/web-services/
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.MG, nfce, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeStatusServico4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.MG, nfce, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.MG, nfce, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, Estado.MG, nfce, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeRecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.MG, nfce, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.MG, nfce, "https://hnfce.fazenda.mg.gov.br/nfce/services/NFeRetAutorizacao4");
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeStatus2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao");

                // NF-e
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.MG, nfe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4");

                // NFC-e
                 //18/12/2018 - http://www.sped.fazenda.mg.gov.br/spedmg/nfce/web-services/
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.MG, nfce, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.MG, nfce, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.MG, nfce, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeStatusServico4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, Estado.MG, nfce, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeRecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.MG, nfce, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.MG, nfce, "https://nfce.fazenda.mg.gov.br/nfce/services/NFeRetAutorizacao4");
            }

            #endregion

            #endregion

            #region MS

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/CadConsultaCadastro2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.MS, nfe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetAutorizacao");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.MS, nfe, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.MS, nfe, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.MS, nfe, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeStatusServico4");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.MS, nfe, "https://hom.nfe.sefaz.ms.gov.br/ws/CadConsultaCadastro4");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.MS, nfe, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.MS, nfe, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.MS, nfe, "https://hom.nfe.sefaz.ms.gov.br/ws/NFeRetAutorizacao4");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.MS, nfce, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.MS, nfce, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeRetAutorizacao");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, hom, emissao, Estado.MS, nfce, "https://homologacao.nfce.ms.gov.br/homologacao/services2/CadConsultaCadastro2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.MS, nfce, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.MS, nfce, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.MS, nfce, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeStatusServico2");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, Estado.MS, nfce, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/RecepcaoEvento");

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.MS, nfce, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.MS, nfce, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.MS, nfce, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.MS, nfce, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.MS, nfce, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeStatusServico4");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.MS, nfce, "https://hom.nfe.sefaz.ms.gov.br/ws/CadConsultaCadastro4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, Estado.MS, nfce, "https://hom.nfce.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4");

                #endregion

            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/CadConsultaCadastro2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.MS, nfe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetAutorizacao");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.MS, nfe, "https://nfe.sefaz.ms.gov.br/ws/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.MS, nfe, "https://nfe.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.MS, nfe, "https://nfe.sefaz.ms.gov.br/ws/NFeStatusServico4");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.MS, nfe, "https://nfe.sefaz.ms.gov.br/ws/CadConsultaCadastro4");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.MS, nfe, "https://nfe.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.MS, nfe, "https://nfe.sefaz.ms.gov.br/ws/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.MS, nfe, "https://nfe.sefaz.ms.gov.br/ws/NFeRetAutorizacao4");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.MS, nfce, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeAutorizacao");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.MS, nfce, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeRetAutorizacao");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.MS, nfce, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.MS, nfce, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.MS, nfce, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeStatusServico2");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, Estado.MS, nfce, "https://nfce.fazenda.ms.gov.br/producao/services2/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.MS, nfce, "https://nfce.sefaz.ms.gov.br/ws/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.MS, nfce, "https://nfce.sefaz.ms.gov.br/ws/NFeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.MS, nfce, "https://nfce.sefaz.ms.gov.br/ws/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.MS, nfce, "https://nfce.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.MS, nfce, "https://nfce.sefaz.ms.gov.br/ws/NFeStatusServico4");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.MS, nfce, "https://nfe.sefaz.ms.gov.br/ws/CadConsultaCadastro4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, Estado.MS, nfce, "https://nfce.sefaz.ms.gov.br/ws/NFeRecepcaoEvento4");

                #endregion
            }

            #endregion

            #endregion

            #region MT

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao2E3, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao3, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico4");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro4");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.MT, nfe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao4");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl");

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeConsulta4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeStatusServico4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, Estado.MT, nfce, "https://homologacao.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento4");

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro4?wsdl");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao4?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.MT, nfe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao4?wsdl");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl");

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeConsulta4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeStatusServico4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, Estado.MT, nfce, "https://nfce.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento4");

                #endregion
            }

            #endregion

            #endregion

            #region PA

            //PA usa SVRS para NFe e NFCe. Rev: 28/08/2019

            #endregion

            #region PB

            //PA usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region PE

            //PE usa o NFCe da SVRS. Rev: 01/12/2017

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeInutilizacao4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeConsultaProtocolo4?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeStatusServico4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/CadConsultaCadastro4?wsdl");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeRecepcaoEvento4?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeAutorizacao4?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.PE, nfe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NFeRetAutorizacao4?wsdl");

            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento");
                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2E3, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2E3, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2E3, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2E3, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/CadConsultaCadastro2");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeStatusServico4");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeRecepcaoEvento4");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.PE, nfe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeRetAutorizacao4");

            }

            #endregion

            #endregion

            #region PI

            //PI usa SRVS para NFe e para NFCe. Rev: 01/12/2017

            #endregion

            #region PR

            //Rev: 09/09/2015

            //Em http://www.nfe.fazenda.gov.br/portal/webServices.aspx?tipoConteudo=Wak0FwB7dKs=#PR há indicação que a SEFAZ do PR tem dois endereços para o serviço NfeConsultaCadastro (versão 2.0 e 3.10)
            //No entanto em 29/11/2016 foi constatado que a url indicada para versão 2.0 desse serviço não está funcionando e a url indicada para versão 3.10 na verdade roda um serviço com versão 2.0

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl");
                //addServico(new[] {ServicoNFe.NfeConsultaCadastro}, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeInutilizacao4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeStatusServico4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/CadConsultaCadastro4?wsdl");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeAutorizacao4?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.PR, nfe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeRetAutorizacao4?wsdl");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeConsulta3");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento");

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeStatusServico4");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.sefa.pr.gov.br/nfce/CadConsultaCadastro4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, Estado.PR, nfce, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeRecepcaoEvento4");

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                addServico(new[] { ServicoNFe.NfeRecepcao }, versao2, prod, emissao, Estado.PR, nfe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeRetRecepcao }, versao2, prod, emissao, Estado.PR, nfe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao2, prod, emissao, Estado.PR, nfe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao2, prod, emissao, Estado.PR, nfe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao2, prod, emissao, Estado.PR, nfe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl");
                //addServico(new[] {ServicoNFe.NfeConsultaCadastro}, versao2, prod, emissao, Estado.PR, nfe, "https://nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao2, prod, emissao, Estado.PR, nfe, "https://nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.PR, nfe, "https://nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.PR, nfe, "https://nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.PR, nfe, "https://nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, prod, emissao, Estado.PR, nfe, "https://nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.PR, nfe, "https://nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.PR, nfe, "https://nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.PR, nfe, "https://nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.PR, nfe, "https://nfe.sefa.pr.gov.br/nfe/NFeInutilizacao4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.PR, nfe, "https://nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.PR, nfe, "https://nfe.sefa.pr.gov.br/nfe/NFeStatusServico4?wsdl");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.PR, nfe, "https://nfe.sefa.pr.gov.br/nfe/CadConsultaCadastro4?wsdl");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.PR, nfe, "https://nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.PR, nfe, "https://nfe.sefa.pr.gov.br/nfe/NFeAutorizacao4?wsdl");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.PR, nfe, "https://nfe.sefa.pr.gov.br/nfe/NFeRetAutorizacao4?wsdl");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.PR, nfce, "https://nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.PR, nfce, "https://nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.PR, nfce, "https://nfce.fazenda.pr.gov.br/nfce/NFeConsulta3");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.PR, nfce, "https://nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.PR, nfce, "https://nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, Estado.PR, nfce, "https://nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.PR, nfce, "https://nfce.sefa.pr.gov.br/nfce/NFeAutorizacao4");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.PR, nfce, "https://nfce.sefa.pr.gov.br/nfce/NFeRetAutorizacao4");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.PR, nfce, "https://nfce.sefa.pr.gov.br/nfce/NFeInutilizacao4");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.PR, nfce, "https://nfce.sefa.pr.gov.br/nfce/NFeConsultaProtocolo4");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.PR, nfce, "https://nfce.sefa.pr.gov.br/nfce/NFeStatusServico4");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.PR, nfce, "https://nfce.sefa.pr.gov.br/nfce/CadConsultaCadastro4");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, Estado.PR, nfce, "https://nfce.sefa.pr.gov.br/nfce/NFeRecepcaoEvento4");

                #endregion
            }

            #endregion

            #endregion

            #region RJ

            //RJ usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region RN

            //RN usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region RO

            //RO usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region RS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, hom, emissao, Estado.RS, nfe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.RS, nfe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, Estado.RS, nfce, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, prod, emissao, Estado.RS, nfe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx");
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.RS, nfe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.RS, nfe, "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, Estado.RS, nfce, "https://nfce.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");

                #endregion
            }

            #endregion

            #endregion

            #region RR

            //RR usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region SC

            //SC usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region SE

            //SE usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region SP

            //Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe
                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx");

                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx");
                addServico(eventoCceCanc, versao4, hom, emissao, Estado.SP, nfe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx");
                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teEPEC)
                    addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx");
                else
                    addServico(new[] { ServicoNFe.RecepcaoEventoEpec }, versao1, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx");

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, Estado.SP, nfce, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx");

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    addServico(eventoCceCanc, versao1, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx");

                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx");
                addServico(eventoCceCanc, versao4, prod, emissao, Estado.SP, nfe, "https://nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx");

                #endregion

                #region NFCe

                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx");
                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teEPEC)
                    addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx");
                else
                    addServico(new[] { ServicoNFe.RecepcaoEventoEpec }, versao1, prod, emissao, Estado.SP, nfce, "https://nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx");
                addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx");
                addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx");
                addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, Estado.SP, nfce, "https://nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx");

                #endregion
            }

            #endregion

            #endregion

            #region TO

            //TO usa SRVS para NFe e para NFCe. Rev: 09/09/2015

            #endregion

            #region SVAN

            //Nenhuma UF utiliza NFCe no SVAN. Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svanEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    if (emissao != TipoEmissao.teEPEC)
                        addServico(eventoCceCanc, versao1, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.NfeDownloadNF }, versao3, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx");

                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx");
                    addServico(eventoCceCanc, versao4, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, estado, nfe, "https://hom.sefazvirtual.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx");
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svanEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    if (emissao != TipoEmissao.teEPEC)
                        addServico(eventoCceCanc, versao1, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.NfeDownloadNF }, versao3, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx");
                    addServico(eventoCceCanc, versao4, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, estado, nfe, "https://www.sefazvirtual.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx");
                }
            }

            #endregion

            #endregion

            #region SVRS

            //Rev: 02/09/2019

            #region Homologação

            foreach (var estado in svrsEstadosDemaisServicos)
            {
                foreach (var emissao in emissaoComum)
                {
                    #region NFe

                    if (estado != Estado.BA & estado != Estado.MA & estado != Estado.PE) //Esses estados usam SVRS somente para NFCe, possuindo endereços próprios para NFe.
                    {
                        if (emissao != TipoEmissao.teEPEC)
                            addServico(eventoCceCanc, versao1, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                        addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                        addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                        addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                        addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                        addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");

                        addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                        addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                        addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                        addServico(eventoCceCanc, versao4, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");
                        addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                        addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
                    }

                    #endregion

                    #region NFCe

                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");

                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                    addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, hom, emissao, estado, nfce, "https://nfce-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");

                    #endregion
                }
            }

            foreach (var estado in svrsEstadosConsultaCadastro)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, hom, emissao, estado, modelo, "https://cad-homologacao.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx");
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

                    //Rev: 02/09/2019
                    if (estado != Estado.BA & estado != Estado.MA & estado != Estado.PE) //Esses estados usam SVRS somente para NFCe, possuindo endereços próprios para NFe.
                    {
                        if (emissao != TipoEmissao.teEPEC)
                            addServico(eventoCceCanc, versao1, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                        addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                        addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                        addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                        addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                        addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");

                        addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                        addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                        addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                        addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, emissao, estado, nfe, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx");
                        addServico(eventoCceCanc, versao4, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");
                        addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                        addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
                    }

                    #endregion

                    #region NFCe

                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                    addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao4, prod, emissao, estado, nfce, "https://nfce.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");

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
                        addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao2, prod, emissao, estado, modelo, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx");
                    }
                }
            }

            #endregion

            #endregion

            #region SVC-AN

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svcanEstados)
            {
                addServico(eventoCceCanc, versao1, hom, TipoEmissao.teSVCAN, estado, nfe, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, TipoEmissao.teSVCAN, estado, nfce, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                foreach (var modelo in todosOsModelos)
                {
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx");

                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx");
                    addServico(eventoCceCanc, versao4, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx");
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcanEstados)
            {
                addServico(eventoCceCanc, versao1, prod, TipoEmissao.teSVCAN, estado, nfe, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, TipoEmissao.teSVCAN, estado, nfce, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                foreach (var modelo in todosOsModelos)
                {
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx");

                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx");
                    addServico(eventoCceCanc, versao4, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx");
                }
            }

            #endregion

            #endregion

            #region SVC-RS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svcRsEstados)
            {
                addServico(eventoCceCanc, versao1, hom, TipoEmissao.teSVCRS, estado, nfe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, hom, TipoEmissao.teSVCRS, estado, nfce, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                foreach (var modelo in todosOsModelos)
                {
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");

                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                    addServico(eventoCceCanc, versao4, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, hom, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcRsEstados)
            {
                addServico(eventoCceCanc, versao1, prod, TipoEmissao.teSVCRS, estado, nfe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                addServico(new[] { ServicoNFe.RecepcaoEventoCancelmento }, versao1, prod, TipoEmissao.teSVCRS, estado, nfce, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx");
                foreach (var modelo in todosOsModelos)
                {
                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao3, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao3, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao3, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao3, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao3, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx");

                    addServico(new[] { ServicoNFe.NfeInutilizacao }, versao4, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaProtocolo }, versao4, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx");
                    addServico(new[] { ServicoNFe.NfeStatusServico }, versao4, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx");
                    addServico(new[] { ServicoNFe.NfeConsultaCadastro }, versao4, prod, TipoEmissao.teSVCRS, estado, modelo, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx");
                    addServico(eventoCceCanc, versao4, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx");
                    addServico(new[] { ServicoNFe.NFeAutorizacao }, versao4, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx");
                    addServico(new[] { ServicoNFe.NFeRetAutorizacao }, versao4, prod, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx");
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
                    if (!(estado == Estado.SP & modelo == ModeloDocumento.NFCe))
                    {
                        addServico(new[] { ServicoNFe.RecepcaoEventoEpec }, versao1, hom, TipoEmissao.teEPEC, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                        addServico(new[] { ServicoNFe.RecepcaoEventoEpec }, versao4, hom, TipoEmissao.teEPEC, estado, modelo, "https://hom.nfe.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    }

                    if (modelo != ModeloDocumento.NFCe)
                    {
                        addServico(new[] { ServicoNFe.RecepcaoEventoManifestacaoDestinatario }, versao1, hom, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                        addServico(new[] { ServicoNFe.RecepcaoEventoManifestacaoDestinatario }, versao4, hom, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    }

                    //CE e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & !svanEstados.Contains(estado))
                        addServico(new[] { ServicoNFe.NfeDownloadNF }, versao2E3, hom, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx");
                    if (modelo != ModeloDocumento.NFCe)
                        addServico(new[] { ServicoNFe.NFeDistribuicaoDFe }, versao1, hom, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx");
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
                    {
                        addServico(new[] { ServicoNFe.RecepcaoEventoEpec }, versao1, prod, TipoEmissao.teEPEC, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                        addServico(new[] { ServicoNFe.RecepcaoEventoEpec }, versao4, prod, TipoEmissao.teEPEC, estado, modelo, "https://www.nfe.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    }

                    if (modelo != ModeloDocumento.NFCe)
                    {
                        addServico(new[] { ServicoNFe.RecepcaoEventoManifestacaoDestinatario }, versao1, prod, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx");
                        addServico(new[] { ServicoNFe.RecepcaoEventoManifestacaoDestinatario }, versao4, prod, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx");
                    }

                    //CE e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & !svanEstados.Contains(estado))
                        addServico(new[] { ServicoNFe.NfeDownloadNF }, versao2E3, prod, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx");
                    if (modelo != ModeloDocumento.NFCe)
                        addServico(new[] { ServicoNFe.NFeDistribuicaoDFe }, versao1, prod, TipoEmissao.teNormal, estado, modelo, "https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx");
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
        /// <returns></returns>
        private static string Erro(ServicoNFe servico, VersaoServico versaoServico, Estado estado, TipoAmbiente tipoAmbiente, TipoEmissao tipoEmissao, ModeloDocumento modeloDocumento)
        {
            return "Serviço " + servico + ", versão " + servico.VersaoServicoParaString(versaoServico) + ", não disponível para a UF " + estado + ", no ambiente de " + tipoAmbiente.TpAmbParaString() +
                   " para emissão tipo " + tipoEmissao.TipoEmissaoParaString() + ", documento: " + modeloDocumento.ModeloDocumentoParaString() + "!";
        }

        /// <summary>
        ///     Obtém uma url a partir de uma lista armazenada em enderecoServico e povoada dinamicamente no create desta classe
        /// </summary>
        /// <returns></returns>
        public static string ObterUrlServico(ServicoNFe servico, ConfiguracaoServico cfgServico)
        {
            var versaoServico = ObterVersaoServico(servico, cfgServico);
            if (versaoServico == null)
                throw new Exception(string.Format("Não foi possível obter a versão do serviço {0}!", servico));
            return ObterUrlServico(servico, versaoServico.GetValueOrDefault(), cfgServico.cUF, cfgServico.tpAmb,
                cfgServico.ModeloDocumento, cfgServico.tpEmis);
        }

        /// <summary>
        ///     Obtém uma url a partir de uma lista armazenada em enderecoServico e povoada dinamicamente no create desta classe
        /// </summary>
        /// <returns></returns>
        public static string ObterUrlServico(ServicoNFe servico, VersaoServico versaoServico, Estado estado, TipoAmbiente tipoAmbiente, ModeloDocumento modeloDocumento, TipoEmissao tipoEmissao)
        {
            var urls = ObterEnderecoServicosMaisRecentes(versaoServico, estado, tipoAmbiente, modeloDocumento, tipoEmissao).Where(n => n.ServicoNFe == servico)
                .Select(n => n.Url).ToList();

            if (!urls.Any())
                throw new Exception(Erro(servico, versaoServico, estado, tipoAmbiente, tipoEmissao, modeloDocumento));
            if (urls.Count > 1)
                throw new Exception("A função ObterUrlServico obteve mais de um resultado!");
            return urls.FirstOrDefault();
        }

        /// <summary>
        /// Obtém os serviços mais recentes
        /// </summary>
        /// <param name="versaoLimite">Versão limite para o serviço. Ex: informe <see cref="DFe.Classes.Flags.VersaoServico.Versao310"/> para obter todos os serviços mais recentes até a versão 3.10</param>
        /// <param name="uf">UF do serviço da NFe</param>
        /// <param name="tipoAmbiente">Tipo de ambiente (produção ou homologação)</param>
        /// <param name="modeloDocumento">Modelo do documento. Ex: NFe, NFCe.</param>
        /// <param name="tipoEmissao">Tipo de emissão da nota</param>
        public static List<EnderecoServico> ObterEnderecoServicosMaisRecentes(VersaoServico versaoLimite, Estado uf, TipoAmbiente tipoAmbiente,
            ModeloDocumento modeloDocumento, TipoEmissao tipoEmissao)
        {

            var enderecoServicos = from end in ListaEnderecos
                                   where end.Estado == uf && end.TipoAmbiente == tipoAmbiente && end.ModeloDocumento == modeloDocumento &&
                                         end.TipoEmissao == tipoEmissao && end.VersaoServico <= versaoLimite
                                   group end by end.ServicoNFe
                into g
                                   select g.OrderByDescending(t => t.VersaoServico).FirstOrDefault();

            return enderecoServicos.ToList();
        }

    }
}
