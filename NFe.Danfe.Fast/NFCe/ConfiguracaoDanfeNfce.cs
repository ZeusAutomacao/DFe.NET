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

using System.IO;
using FastReport;
using FastReport.Barcode;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Utils;
using NFe.Utils.InformacoesSuplementares;

namespace NFe.Danfe.Fast.NFCe
{
    /// <summary>
    /// Classe reponsável pela impressão do DANFE da NFCe em Fast Reports
    /// </summary>
    public class DanfeFrNfce: DanfeBase
    {
        /// <summary>
        /// Construtor da classe reponsável pela impressão do DANFE da NFCe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        /// <param name="cIdToken">Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ</param>
        /// <param name="csc">Código de Segurança do Contribuinte(antigo Token)</param>
        public DanfeFrNfce(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc)
        {
            #region Define as varíaveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)

            Relatorio = new Report();
            Relatorio.RegisterData(new[] { proc }, "NFCe", 20);
            Relatorio.GetDataSource("NFCe").Enabled = true;
            Relatorio.Load(new MemoryStream(Properties.Resources.NFCe));
            Relatorio.SetParameterValue("NfceDetalheVendaNormal", configuracaoDanfeNfce.DetalheVendaNormal);
            Relatorio.SetParameterValue("NfceDetalheVendaContigencia", configuracaoDanfeNfce.DetalheVendaContigencia);
            Relatorio.SetParameterValue("NfceImprimeDescontoItem", configuracaoDanfeNfce.ImprimeDescontoItem);
            Relatorio.SetParameterValue("NfceModoImpressao", configuracaoDanfeNfce.ModoImpressao);
            Relatorio.SetParameterValue("NfceCancelado", configuracaoDanfeNfce.DocumentoCancelado);
            ((ReportPage) Relatorio.FindObject("PgNfce")).LeftMargin = configuracaoDanfeNfce.MargemEsquerda;
            ((ReportPage)Relatorio.FindObject("PgNfce")).RightMargin = configuracaoDanfeNfce.MargemDireita;
            ((PictureObject) Relatorio.FindObject("poEmitLogo")).Image = configuracaoDanfeNfce.ObterLogo();
            ((TextObject)Relatorio.FindObject("txtUrl")).Text = proc.NFe.infNFeSupl.ObterUrl(proc.NFe.infNFe.ide.tpAmb, proc.NFe.infNFe.ide.cUF, TipoUrlConsultaPublica.UrlConsulta);
            ((BarcodeObject)Relatorio.FindObject("bcoQrCode")).Text = proc.NFe.infNFeSupl  == null ? proc.NFe.infNFeSupl.ObterUrlQrCode(proc.NFe, cIdToken, csc) : proc.NFe.infNFeSupl.qrCode;

            //Segundo o Manual de Padrões Padrões Técnicos do DANFE - NFC - e e QR Code, versão 3.2, página 9, nos casos de emissão em contigência deve ser impresso uma segunda cópia como via do estabelecimento
            Relatorio.PrintSettings.Copies = (proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teNormal | (proc.protNFe != null && proc.protNFe.infProt != null && NfeSituacao.Autorizada(proc.protNFe.infProt.cStat))
                /*Se a NFe for autorizada, mesmo que seja em contingência, imprime somente uma via*/ ) ? 1 : 2;

            #endregion
        }

        /// <summary>
        /// Construtor da classe reponsável pela impressão do DANFE da NFCe em Fast Reports.
        /// Use esse construtor apenas para impressão em contigência, já que neste modo ainda não é possível obter o grupo protNFe 
        /// </summary>
        /// <param name="nfe">Objeto do tipo NFe</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        /// <param name="cIdToken">Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ</param>
        /// <param name="csc">Código de Segurança do Contribuinte(antigo Token)</param>
        public DanfeFrNfce(Classes.NFe nfe, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc) : this(new nfeProc() { NFe = nfe }, configuracaoDanfeNfce, cIdToken, csc)
        {
        }
    }
}
