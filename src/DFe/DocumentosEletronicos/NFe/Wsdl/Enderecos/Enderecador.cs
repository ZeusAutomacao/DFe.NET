﻿/********************************************************************************/
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
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.NFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.NFe.Classes.Informacoes.Identificacao.Tipos;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Flags;

namespace DFe.DocumentosEletronicos.NFe.Wsdl.Enderecos
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

            var versaoDoisETres = new List<VersaoServico> { VersaoServico.Versao200, VersaoServico.Versao300 };

            var svanEstados = new List<Estado> { Estado.MA, Estado.PA };

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

            var svcanEstados = new List<Estado> { Estado.AC, Estado.AL, Estado.AP, Estado.DF, Estado.ES, Estado.MG, Estado.PB, Estado.RJ, Estado.RN, Estado.RO, Estado.RR, Estado.RS, Estado.SC, Estado.SE, Estado.SP, Estado.TO };

            var svcRsEstados = new List<Estado> { Estado.AM, Estado.BA, Estado.CE, Estado.GO, Estado.MA, Estado.MS, Estado.MT, Estado.PA, Estado.PE, Estado.PI, Estado.PR };

            var todosOsEstados = Enum.GetValues(typeof(Estado)).Cast<Estado>().ToList();

            var todosOsModelos = Enum.GetValues(typeof(ModeloDocumento)).Cast<ModeloDocumento>().ToList();

            var todosOsAmbientes = Enum.GetValues(typeof(TipoAmbiente)).Cast<TipoAmbiente>().ToList();

            var eventoCceCanc = new List<ServicoNFe> { ServicoNFe.RecepcaoEventoCartaCorrecao, ServicoNFe.RecepcaoEventoCancelmento };

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
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFe, "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao4"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeConsulta2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeStatusServico2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao300, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services-nac/services/NfeInutilizacao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://homnfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento4"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/cadconsultacadastro2")));
                //Este endereço não possui suporte a REST, de modo que não é possível obter o endpoint reference (EPR)  do webservice. Entrar em contato com a SEFAZ AM
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFe, "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao"));


                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao300, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.AM, ModeloDocumento.NFCe, "https://nfce.sefaz.am.gov.br/nfce-services/services/RecepcaoEvento4"));

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

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                if (emissao != TipoEmissao.teEPEC)
                    foreach (var servicoNFe in eventoCceCanc)
                        endServico.AddRange(
                            versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, versao, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));

                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.BA, ModeloDocumento.NFe, "https://hnfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeRetRecepcao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/NfeStatusServico2.asmx"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/nfenw/CadConsultaCadastro2.asmx")));
                if (emissao != TipoEmissao.teEPEC)
                    foreach (var servicoNFe in eventoCceCanc)
                        endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, versao, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeInutilizacao/NfeInutilizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeConsulta/NfeConsulta.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeInutilizacao4/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeStatusServico4/NFeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/CadConsultaCadastro4/CadConsultaCadastro4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.BA, ModeloDocumento.NFe, "https://nfe.sefaz.ba.gov.br/webservices/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));

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
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeInutilizacao4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeStatusServico4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeAutorizacao4?WSDL"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRetAutorizacao4?WSDL"));
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.CE, ModeloDocumento.NFe, "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl"));
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
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, ambiente, emissao, Estado.ES, modelo, "https://app.sefaz.es.gov.br/ConsultaCadastroService/CadConsultaCadastro2.asmx"));
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
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.GO, ModeloDocumento.NFe, "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.GO, ModeloDocumento.NFCe, "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl"));

                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl"));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl")));
                    endServico.AddRange(
                        versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfceAdministracaoCSC, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/v2/CscNFCe?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeStatusServico4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeInutilizacao4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/CadConsultaCadastro4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeAutorizacao4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeRetAutorizacao4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.GO, modelo, "https://homolog.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl"));
                }
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.GO, ModeloDocumento.NFe, "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.GO, ModeloDocumento.NFCe, "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl"));

                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRecepcao2?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetRecepcao2?wsdl"));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeInutilizacao2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl")));
                    endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/CadConsultaCadastro2?wsdl")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfceAdministracaoCSC, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/v2/CscNFCe?wsdl​"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeInutilizacao4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeStatusServico4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/CadConsultaCadastro4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeRecepcaoEvento4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeAutorizacao4?wsdl"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.GO, modelo, "https://nfe.sefaz.go.gov.br/nfe/services/NFeRetAutorizacao4?wsdl"));
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
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, ambiente, emissao, Estado.MA, modelo, "https://sistemas.sefaz.ma.gov.br/wscadastro/CadConsultaCadastro2?wsdl"));
                    }
                }
            }

            #endregion

            #region MG

            //MG ainda não implementou a NFCe. Rev: 09/09/2015

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeStatusServico2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MG, ModeloDocumento.NFe, "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4"));
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/cadconsultacadastro2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeStatus2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeConsultaProtocolo4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MG, ModeloDocumento.NFe, "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4"));
            }

            #endregion

            #endregion

            #region MS

            #region Homologação

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/CadConsultaCadastro2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetAutorizacao"));

                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/NFeConsultaProtocolo4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/CadConsultaCadastro4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFe, "https://homologacao.nfe.ms.gov.br/ws/NFeRetAutorizacao4"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.ms.gov.br/homologacao/services2/CadConsultaCadastro2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/homologacao/services2/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/ws/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/ws/NFeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/ws/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/ws/NFeConsultaProtocolo4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/ws/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/ws/CadConsultaCadastro4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.ms.gov.br/ws/NFeRecepcaoEvento4"));

                #endregion

            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/CadConsultaCadastro2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/NFeConsultaProtocolo4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/CadConsultaCadastro4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/NFeRecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFe, "https://nfe.fazenda.ms.gov.br/ws/NFeRetAutorizacao4"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeAutorizacao"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeRetAutorizacao"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/producao/services2/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/producao/services2/RecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/ws/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/ws/NFeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/ws/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/ws/NFeConsultaProtocolo4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/ws/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/ws/CadConsultaCadastro4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MS, ModeloDocumento.NFCe, "https://nfce.fazenda.ms.gov.br/ws/NFeRecepcaoEvento4"));

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

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl")));
                if (emissao != TipoEmissao.teEPEC)
                    foreach (var servicoNFe in eventoCceCanc)
                        endServico.AddRange(
                            versaoDoisETres.Select(versao => new EnderecoServico(servicoNFe, versao, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFe, "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao4"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl"));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/v2/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/v2/services/NfeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/v2/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/v2/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/v2/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://homologacao.sefaz.mt.gov.br/nfcews/v2/services/RecepcaoEvento4"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetRecepcao2?wsdl"));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro2?wsdl")));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(
                        eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl"));

                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeInutilizacao4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/CadConsultaCadastro4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFe, "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao4?wsdl"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeRetAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/RecepcaoEvento?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeStatusServico2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/services/NfeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/v2/services/NfeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/v2/services/NfeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/v2/services/NfeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/v2/services/NfeConsulta4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/v2/services/NfeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.MT, ModeloDocumento.NFCe, "https://nfce.sefaz.mt.gov.br/nfcews/v2/services/RecepcaoEvento4"));

                #endregion
            }

            #endregion

            #endregion

            #region PA

            //PA usa SVAN para NFe e SRVS para NFCe. Rev: 09/09/2015

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
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2")));
                endServico.AddRange(
                    versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl"));
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRecepcao2"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetRecepcao2"));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeInutilizacao, versao, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeInutilizacao2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, versao, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeStatusServico, versao, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2")));
                endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeConsultaCadastro, versao, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/CadConsultaCadastro2")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PE, ModeloDocumento.NFe, "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao?wsdl"));
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

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl"));
                //endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl"));

                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeInutilizacao4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeStatusServico4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/CadConsultaCadastro4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeAutorizacao4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFe, "https://homologacao.nfe.sefa.pr.gov.br/nfe/NFeRetAutorizacao4?wsdl"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeConsulta3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento"));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeConsultaProtocolo4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.sefa.pr.gov.br/nfce/CadConsultaCadastro4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://homologacao.nfce.sefa.pr.gov.br/nfce/NFeRecepcaoEvento4"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeRetRecepcao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeRetRecepcao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeInutilizacao2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeConsulta2?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/NFeStatusServico2?wsdl"));
                //endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeInutilizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/CadConsultaCadastro2?wsdl"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeRecepcaoEvento?wsdl")));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl"));

                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/NFeInutilizacao4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/NFeStatusServico4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/CadConsultaCadastro4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/NFeRecepcaoEvento4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/NFeAutorizacao4?wsdl"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFe, "https://nfe.sefa.pr.gov.br/nfe/NFeRetAutorizacao4?wsdl"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeRetAutorizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeConsulta3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeInutilizacao3"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeStatusServico3"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.fazenda.pr.gov.br/nfce/NFeRecepcaoEvento"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.sefa.pr.gov.br/nfce/NFeAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.sefa.pr.gov.br/nfce/NFeRetAutorizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.sefa.pr.gov.br/nfce/NFeInutilizacao4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.sefa.pr.gov.br/nfce/NFeConsultaProtocolo4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.sefa.pr.gov.br/nfce/NFeStatusServico4"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.sefa.pr.gov.br/nfce/CadConsultaCadastro4"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.PR, ModeloDocumento.NFCe, "https://nfce.sefa.pr.gov.br/nfce/NFeRecepcaoEvento4"));

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
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce-homologacao.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://cad.sefazrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFe, "https://nfe.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.RS, ModeloDocumento.NFCe, "https://nfce.sefazrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));

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
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFe, "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teEPEC)
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                else
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://homologacao.nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx"));

                #endregion
            }

            #endregion

            #region Produção

            foreach (var emissao in emissaoComum)
            {
                #region NFe

                if (emissao != TipoEmissao.teEPEC)
                    endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/cadconsultacadastro4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeinutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFe, "https://nfe.fazenda.sp.gov.br/ws/nferecepcaoevento4.asmx"));

                #endregion

                #region NFCe

                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfeautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nferetautorizacao.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfeinutilizacao2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfeconsulta2.asmx"));
                //SP possui um endereço diferente para EPEC de NFCe, serviço "RecepcaoEvento", conforme http://www.nfce.fazenda.sp.gov.br/NFCePortal/Paginas/URLWebServices.aspx
                if (emissao != TipoEmissao.teEPEC)
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/recepcaoevento.asmx"));
                else
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.epec.fazenda.sp.gov.br/EPECws/RecepcaoEPEC.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/nfestatusservico2.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeRetAutorizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeInutilizacao4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeConsultaProtocolo4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeStatusServico4.asmx"));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, Estado.SP, ModeloDocumento.NFCe, "https://nfce.fazenda.sp.gov.br/ws/NFeRecepcaoEvento4.asmx"));

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
                        endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeDownloadNF, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://hom.sefazvirtual.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svanEstados)
            {
                foreach (var emissao in emissaoComum)
                {
                    if (emissao != TipoEmissao.teEPEC)
                        endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeInutilizacao2/NfeInutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeDownloadNF, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NFeInutilizacao4/NFeInutilizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://www.sefazvirtual.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));
                }
            }

            #endregion

            #endregion

            #region SVRS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svrsEstadosDemaisServicos)
            {
                foreach (var emissao in emissaoComum)
                {
                    #region NFe

                    if (estado != Estado.BA & estado != Estado.MA & estado != Estado.PA & estado != Estado.PE) //Esses estados usam SVRS somente para NFCe, possuindo endereços próprios para NFe.
                    {
                        if (emissao != TipoEmissao.teEPEC)
                            endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
                    }

                    #endregion

                    #region NFCe

                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, emissao, estado, ModeloDocumento.NFCe, "https://nfce-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));


                    #endregion
                }
            }

            foreach (var estado in svrsEstadosConsultaCadastro)
            {
                foreach (var emissao in emissaoComum)
                {
                    foreach (var modelo in todosOsModelos)
                    {
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Homologacao, emissao, estado, modelo, "https://cad-homologacao.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
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

                    //Rev: 09/09/2015
                    if (estado != Estado.BA & estado != Estado.MA & estado != Estado.PA & estado != Estado.PE) //Esses estados usam SVRS somente para NFCe, possuindo endereços próprios para NFe.
                    {
                        if (emissao != TipoEmissao.teEPEC)
                            endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
                    }

                    #endregion

                    #region NFCe

                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, emissao, estado, ModeloDocumento.NFCe, "https://nfce.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));

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
                        endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao200, TipoAmbiente.Producao, emissao, estado, modelo, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro2.asmx"));
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
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFe, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFCe, "https://hom.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCAN, estado, modelo, "https://hom.svc.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcanEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFe, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, ModeloDocumento.NFCe, "https://www.svc.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx"));

                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeStatusServico4/NFeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeRecepcaoEvento4/NFeRecepcaoEvento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeAutorizacao4/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCAN, estado, modelo, "https://www.svc.fazenda.gov.br/NFeRetAutorizacao4/NFeRetAutorizacao4.asmx"));
                }
            }

            #endregion

            #endregion

            #region SVC-RS

            //Rev: 09/09/2015

            #region Homologação

            foreach (var estado in svcRsEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFCe, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Homologacao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
                }
            }

            #endregion

            #region Produção

            foreach (var estado in svcRsEstados)
            {
                endServico.AddRange(eventoCceCanc.Select(servicoNFe => new EnderecoServico(servicoNFe, VersaoServico.Versao100, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx")));
                endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao100, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, ModeloDocumento.NFCe, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx"));
                foreach (var modelo in todosOsModelos)
                {
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao310, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeInutilizacao, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/nfeinutilizacao/nfeinutilizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaProtocolo, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeStatusServico, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeStatusServico/NfeStatusServico4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NfeConsultaCadastro, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://cad.svrs.rs.gov.br/ws/cadconsultacadastro/cadconsultacadastro4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCancelmento, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoCartaCorrecao, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/recepcaoevento/recepcaoevento4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx"));
                    endServico.Add(new EnderecoServico(ServicoNFe.NFeRetAutorizacao, VersaoServico.Versao400, TipoAmbiente.Producao, TipoEmissao.teSVCRS, estado, modelo, "https://nfe.svrs.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao4.asmx"));
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
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.Versao100, TipoAmbiente.Homologacao, TipoEmissao.teEPEC, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoManifestacaoDestinatario, VersaoServico.Versao100, TipoAmbiente.Homologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    //CE e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & !svanEstados.Contains(estado))
                        endServico.AddRange(
                            versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.Homologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx")));
                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeDistribuicaoDFe, VersaoServico.Versao100, TipoAmbiente.Homologacao, TipoEmissao.teNormal, estado, modelo, "https://hom.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"));
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
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoEpec, VersaoServico.Versao100, TipoAmbiente.Producao, TipoEmissao.teEPEC, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.RecepcaoEventoManifestacaoDestinatario, VersaoServico.Versao100, TipoAmbiente.Producao, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx"));

                    //CE e SVAN possuem endereços próprios para o serviço NfeDownloadNF
                    if (estado != Estado.CE & !svanEstados.Contains(estado))
                        endServico.AddRange(versaoDoisETres.Select(versao => new EnderecoServico(ServicoNFe.NfeDownloadNF, versao, TipoAmbiente.Producao, TipoEmissao.teNormal, estado, modelo, "https://www.nfe.fazenda.gov.br/NfeDownloadNF/NfeDownloadNF.asmx")));
                    if (modelo != ModeloDocumento.NFCe)
                        endServico.Add(new EnderecoServico(ServicoNFe.NFeDistribuicaoDFe, VersaoServico.Versao100, TipoAmbiente.Producao, TipoEmissao.teNormal, estado, modelo, "https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx"));
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
        /// <param name="nfeBaseConfig"></param>
        /// <returns>Retorna um item do enum VersaoServico, com a versão do serviço</returns>
        private static VersaoServico? ObterVersaoServico(this ServicoNFe servico, NFeBaseConfig nfeBaseConfig)
        {
            switch (servico)
            {
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                case ServicoNFe.RecepcaoEventoCancelmento:
                    return nfeBaseConfig.VersaoRecepcaoEventoCceCancelamento;
                case ServicoNFe.RecepcaoEventoEpec:
                    return nfeBaseConfig.VersaoRecepcaoEventoEpec;
                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    return nfeBaseConfig.VersaoRecepcaoEventoManifestacaoDestinatario;
                case ServicoNFe.NfeRecepcao:
                    return nfeBaseConfig.VersaoNfeRecepcao;
                case ServicoNFe.NfeRetRecepcao:
                    return nfeBaseConfig.VersaoNfeRetRecepcao;
                case ServicoNFe.NfeConsultaCadastro:
                    return nfeBaseConfig.VersaoNfeConsultaCadastro;
                case ServicoNFe.NfeInutilizacao:
                    return nfeBaseConfig.VersaoNfeInutilizacao;
                case ServicoNFe.NfeConsultaProtocolo:
                    return nfeBaseConfig.VersaoNfeConsultaProtocolo;
                case ServicoNFe.NfeStatusServico:
                    return nfeBaseConfig.VersaoNfeStatusServico;
                case ServicoNFe.NFeAutorizacao:
                    return nfeBaseConfig.VersaoNFeAutorizacao;
                case ServicoNFe.NFeRetAutorizacao:
                    return nfeBaseConfig.VersaoNFeRetAutorizacao;
                case ServicoNFe.NFeDistribuicaoDFe:
                    return nfeBaseConfig.VersaoNFeDistribuicaoDFe;
                case ServicoNFe.NfeConsultaDest:
                    return nfeBaseConfig.VersaoNfeConsultaDest;
                case ServicoNFe.NfeDownloadNF:
                    return nfeBaseConfig.VersaoNfeDownloadNF;
                case ServicoNFe.NfceAdministracaoCSC:
                    return nfeBaseConfig.VersaoNfceAministracaoCSC;
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
        private static string Erro(ServicoNFe servico, NFeBaseConfig baseConfig)
        {
            return "Serviço " + servico + ", versão " + servico.VersaoServicoParaString(servico.ObterVersaoServico(baseConfig)) + ", não disponível para a UF " + baseConfig.EstadoUf + ", no ambiente de " + Conversao.TpAmbParaString(baseConfig.TipoAmbiente) +
                   " para emissão tipo " + Conversao.TipoEmissaoParaString(baseConfig.TipoEmissao) + ", documento: " + baseConfig.ModeloDocumento.ModeloDocumentoParaString() + "!";
        }

        /// <summary>
        ///     Obtém uma url a partir de uma lista armazenada em enderecoServico e povoada dinamicamente no create desta classe
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="cfgServico"></param>
        /// <returns></returns>
        public static string ObterUrlServico(NFeBaseConfig nfeBaseConfig)
        {
            var definicao = from d in ListaEnderecos
                where d.Estado == nfeBaseConfig.EstadoUf && d.ServicoNFe == nfeBaseConfig.ServicoNFe && d.TipoAmbiente == nfeBaseConfig.TipoAmbiente && d.TipoEmissao == nfeBaseConfig.TipoEmissao && d.VersaoServico == ObterVersaoServico(nfeBaseConfig.ServicoNFe, nfeBaseConfig) && d.ModeloDocumento == nfeBaseConfig.ModeloDocumento
                select d.Url;
            var listaRetorno = definicao as IList<string> ?? definicao.ToList();
            var qtdeRetorno = listaRetorno.Count();

            if (qtdeRetorno == 0)
                throw new Exception(Erro(nfeBaseConfig.ServicoNFe, nfeBaseConfig));
            if (qtdeRetorno > 1)
                throw new Exception("A função ObterUrlServico obteve mais de um resultado!");
            return listaRetorno.FirstOrDefault();
        }
    }
}