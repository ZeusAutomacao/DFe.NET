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
using FastReport.Export.Pdf;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Impressao.NFCe.FastReports
{
    /// <summary>
    /// Classe reponsável pela impressão do DANFE da NFCe em Fast Reports
    /// </summary>
    public class DanfeFrNfce
    {
        private readonly Report _relatorio;

        /// <summary>
        /// Construtor da classe reponsável pela impressão do DANFE da NFCe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        public DanfeFrNfce(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce)
        {
            #region Define as varíaveis que serão usadas no relatório (dúvidas a respeito do fast reports consulte a documentação em https://www.fast-report.com/pt/product/fast-report-net/documentation/)

            _relatorio = new Report();
            _relatorio.RegisterData(new[] { proc }, "NFCe", 20);
            _relatorio.GetDataSource("NFCe").Enabled = true;
            _relatorio.Load(new MemoryStream(Properties.Resources.NFCe));
            _relatorio.SetParameterValue("NfceDetalheVendaNormal", configuracaoDanfeNfce.DetalheVendaNormal);
            _relatorio.SetParameterValue("NfceDetalheVendaContigencia", configuracaoDanfeNfce.DetalheVendaContigencia);
            _relatorio.SetParameterValue("NfceImprimeDescontoItem", configuracaoDanfeNfce.ImprimeDescontoItem);
            ((PictureObject) _relatorio.FindObject("poEmitLogo")).Image = configuracaoDanfeNfce.ObterLogo();
            ((TextObject)_relatorio.FindObject("txtUrl")).Text = EnderecadorDanfeNfce.ObterUrl(proc.NFe.infNFe.ide.tpAmb, proc.NFe.infNFe.ide.cUF, TipoUrlDanfeNfce.UrlConsulta);
            ((BarcodeObject)_relatorio.FindObject("bcoQrCode")).Text = proc.NFe.infNFeSupl  == null ? EnderecadorDanfeNfce.ObterUrlQrCode(proc.NFe, configuracaoDanfeNfce) : proc.NFe.infNFeSupl.qrCode;

            //Segundo o Manual de Padrões Padrões Técnicos do DANFE - NFC - e e QR Code, versão 3.2, página 9, nos casos de emissão em contigência deve ser impresso uma segunda cópia como via do estabelecimento
            _relatorio.PrintSettings.Copies = proc.NFe.infNFe.ide.tpEmis == TipoEmissao.teNormal ? 1 : 2;

            #endregion
        }

        /// <summary>
        /// Construtor da classe reponsável pela impressão do DANFE da NFCe em Fast Reports.
        /// Use esse construtor apenas para impressão em contigência, já que neste modo ainda não é possível obter o grupo protNFe 
        /// </summary>
        /// <param name="nfe">Objeto do tipo NFe</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        public DanfeFrNfce(Classes.NFe nfe, ConfiguracaoDanfeNfce configuracaoDanfeNfce) : this(new nfeProc() { NFe = nfe }, configuracaoDanfeNfce)
        {
        }

        /// <summary>
        /// Abre a janela de visualização do DANFE da NFCe
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        public void Visualizar(bool modal = true)
        {
            _relatorio.Show(modal);
        }

        /// <summary>
        ///  Abre a janela de visualização do design do DANFE da NFCe.
        /// Chame esse método se desja fazer alterações no design do DANFE em modo run-time
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        public void ExibirDesign(bool modal = false)
        {
            _relatorio.Design(modal);
        }

        /// <summary>
        /// Envia a impressão do DANFE da NFCe diretamente para a impressora
        /// </summary>
        /// <param name="exibirDialogo">Se true exibe o diálogo Imprimindo...</param>
        /// <param name="impressora">Passe a string com o nome da impressora para imprimir diretamente em determinada impressora. Caso contrário, a impressão será feita na impressora que estiver como padrão</param>
        public void Imprimir(bool exibirDialogo = true, string impressora = "")
        {
            _relatorio.PrintSettings.ShowDialog = exibirDialogo;
            _relatorio.PrintSettings.Printer = impressora;
            _relatorio.Print();
        }

        public void ExportarPdf(string arquivo)
        {
            _relatorio.Prepare();
            _relatorio.Export(new PDFExport(), arquivo);
        }

    }
}
