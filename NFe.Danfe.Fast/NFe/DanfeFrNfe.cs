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
using System.IO;
using System.Text.RegularExpressions;
using DFe.Classes.Flags;
using FastReport;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Danfe.Base.NFe;
using NFe.Danfe.Fast.Properties;

namespace NFe.Danfe.Fast.NFe
{
    /// <summary>
    /// Classe responsável pela impressão do DANFE da NFe em Fast Reports
    /// </summary>
    public class DanfeFrNfe : DanfeBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        public DanfeFrNfe(nfeProc proc, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
            #region Define as variáveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)

            Relatorio = new Report();
            Relatorio.RegisterData(new[] { proc }, "NFe", 20);
            Relatorio.GetDataSource("NFe").Enabled = true;

            if (string.IsNullOrEmpty(arquivoRelatorio))
            {
                Relatorio.Load(new MemoryStream(Resources.NFeRetrato));
            }
            else
            {
                Relatorio.Load(arquivoRelatorio);
            }

            var mensagem = string.Empty;
            var resumoCanhoto = string.Empty;
            var contingenciaDescricao = string.Empty;
            var contingenciaValor = string.Empty;
            var consultaAutenticidade = "Consulta de autenticidade no portal nacional da NF-e" + Environment.NewLine +
                                        "www.nfe.fazenda.gov.br/portal ou no site da Sefaz autorizadora";

            if (configuracaoDanfeNfe.ExibirResumoCanhoto)
            {
                resumoCanhoto = string.IsNullOrEmpty(configuracaoDanfeNfe.ResumoCanhoto) ?
                                string.Format("Emissão: {0: dd/MM/yyyy} Dest/Reme: {1} Valor Total: {2:C}", proc.NFe.infNFe.ide.dhEmi,
                                                proc.NFe.infNFe.dest.xNome, proc.NFe.infNFe.total.ICMSTot.vNF) :
                                configuracaoDanfeNfe.ResumoCanhoto;
            }

            if (proc.NFe.infNFe.ide.tpAmb == TipoAmbiente.Homologacao)
            {
                if (proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teSCAN ||
                    proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teEPEC ||
                    proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teFSDA ||
                    proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teFSIA)
                {
                    if (proc.protNFe != null && proc.protNFe.infProt != null &&
                        (proc.protNFe.infProt.cStat == 101 ||
                         proc.protNFe.infProt.cStat == 135 ||
                         proc.protNFe.infProt.cStat == 151 ||
                         proc.protNFe.infProt.cStat == 155))
                    {
                        mensagem = "NFe sem Valor Fiscal - HOMOLOGAÇÃO" + Environment.NewLine +
                                   "NFe em Contingência - CANCELADA";
                    }
                    else
                    {
                        mensagem = "NFe sem Valor Fiscal - HOMOLOGAÇÃO" + Environment.NewLine +
                                   "NFe em Contingência";
                    }
                }
                else
                {
                    mensagem = "NFe sem Valor Fiscal - HOMOLOGAÇÃO";
                }
            }
            else
            {
                if (configuracaoDanfeNfe.DocumentoCancelado ||
                    (proc.protNFe != null && proc.protNFe.infProt != null &&
                    !string.IsNullOrEmpty(proc.protNFe.infProt.nProt) &&
                    (proc.protNFe.infProt.cStat == 101 ||
                     proc.protNFe.infProt.cStat == 135 ||
                     proc.protNFe.infProt.cStat == 151 ||
                     proc.protNFe.infProt.cStat == 155)))
                {
                    mensagem = "NFe Cancelada";
                }
                else if (proc.protNFe != null && proc.protNFe.infProt != null &&
                    (proc.protNFe.infProt.cStat == 110 ||
                     proc.protNFe.infProt.cStat == 301 ||
                     proc.protNFe.infProt.cStat == 302 ||
                     proc.protNFe.infProt.cStat == 303))
                {
                    mensagem = "NFe denegada pelo Fisco";
                }
                else if (proc.protNFe != null && proc.protNFe.infProt != null &&
                         string.IsNullOrEmpty(proc.protNFe.infProt.nProt))
                {
                    mensagem = "NFe sem Autorização de Uso da SEFAZ";
                }
            }

            switch (proc.NFe.infNFe.ide.tpEmis)
            {
                case TipoEmissao.teNormal:
                case TipoEmissao.teSCAN:
                case TipoEmissao.teSVCAN:
                case TipoEmissao.teSVCRS:
                    contingenciaDescricao = "PROTOCOLO DE AUTORIZAÇÃO DE USO";
                    contingenciaValor = ((proc.protNFe == null || proc.protNFe.infProt == null || string.IsNullOrEmpty(proc.protNFe.infProt.nProt)) ? "NFe sem Autorização de Uso da SEFAZ" : string.Format("{0} - {1:dd/MM/yyyy HH:mm:ss}", proc.protNFe.infProt.nProt, proc.protNFe.infProt.dhRecbto));
                    if (configuracaoDanfeNfe.DocumentoCancelado || (proc.protNFe != null && proc.protNFe.infProt != null && (proc.protNFe.infProt.cStat == 101 || proc.protNFe.infProt.cStat == 151 || proc.protNFe.infProt.cStat == 155)))
                    {
                        contingenciaDescricao = "PROTOCOLO DE HOMOLOGAÇÃO DO CANCELAMENTO";
                    }
                    else if (proc.protNFe != null && proc.protNFe.infProt != null && (proc.protNFe.infProt.cStat == 110 || proc.protNFe.infProt.cStat == 301 || proc.protNFe.infProt.cStat == 302 || proc.protNFe.infProt.cStat == 303))
                    {
                        contingenciaDescricao = "PROTOCOLO DE DENEGAÇÃO DE USO";
                    }
                    break;

                case TipoEmissao.teFSIA:
                case TipoEmissao.teEPEC:
                case TipoEmissao.teFSDA:
                    contingenciaDescricao = "DADOS DA NF-E";
                    contingenciaValor = Regex.Replace(configuracaoDanfeNfe.ChaveContingencia, ".{4}", "$0 ");
                    consultaAutenticidade = string.Empty;
                    break;

                default:
                    contingenciaValor = string.Format("{0} - {1:dd/MM/yyyy HH:mm:ss}", proc.protNFe.infProt.nProt, proc.protNFe.infProt.dhRecbto);
                    break;
            }

            Relatorio.SetParameterValue("ResumoCanhoto", resumoCanhoto);
            Relatorio.SetParameterValue("Mensagem", mensagem);
            Relatorio.SetParameterValue("ConsultaAutenticidade", consultaAutenticidade);
            Relatorio.SetParameterValue("ContingenciaDescricao", contingenciaDescricao);
            Relatorio.SetParameterValue("ContingenciaValor", contingenciaValor);
            Relatorio.SetParameterValue("ContingenciaID", configuracaoDanfeNfe.ChaveContingencia);
            Relatorio.SetParameterValue("DuasLinhas", configuracaoDanfeNfe.DuasLinhas);
            Relatorio.SetParameterValue("Desenvolvedor", desenvolvedor);
            Relatorio.SetParameterValue("QuebrarLinhasObservacao", configuracaoDanfeNfe.QuebrarLinhasObservacao);
            Relatorio.SetParameterValue("ImprimirISSQN", configuracaoDanfeNfe.ImprimirISSQN);
            Relatorio.SetParameterValue("ImprimirDescPorc", configuracaoDanfeNfe.ImprimirDescPorc);
            Relatorio.SetParameterValue("ImprimirTotalLiquido", configuracaoDanfeNfe.ImprimirTotalLiquido);
            Relatorio.SetParameterValue("ImprimirUnidQtdeValor", configuracaoDanfeNfe.ImprimirUnidQtdeValor);
            Relatorio.SetParameterValue("ExibeCampoFatura", configuracaoDanfeNfe.ExibeCampoFatura);
            Relatorio.SetParameterValue("Logo", configuracaoDanfeNfe.Logomarca);
            Relatorio.SetParameterValue("ExibirTotalTributos", configuracaoDanfeNfe.ExibirTotalTributos);
            Relatorio.SetParameterValue("DecimaisValorUnitario", configuracaoDanfeNfe.DecimaisValorUnitario);
            Relatorio.SetParameterValue("DecimaisQuantidadeItem", configuracaoDanfeNfe.DecimaisQuantidadeItem);
            
            #endregion Define as variáveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)
        }

        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFe em Fast Reports.
        /// Use esse construtor apenas para impressão em contingência, já que neste modo ainda não é possível obter o grupo protNFe
        /// </summary>
        /// <param name="nfe">Objeto do tipo <see cref="Classes.NFe"/></param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        public DanfeFrNfe(Classes.NFe nfe, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor) : this(new nfeProc() { NFe = nfe }, configuracaoDanfeNfe, desenvolvedor)
        {
        }
    }
}