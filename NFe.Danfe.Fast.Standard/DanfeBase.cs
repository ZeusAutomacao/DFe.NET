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
using FastReport.Export.Html;
using FastReport.Export.Image;
using FastReport.Export.PdfSimple;
using Shared.NFe.Danfe.Base;

namespace NFe.Danfe.Fast.Standard
{
    public class DanfeBase: IDanfeBasico
    {
        public Report Relatorio { get; protected set; }

        public byte[] ExportarPdf()
        {
            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    Relatorio.Prepare();
                    Relatorio.Export(new PDFSimpleExport(), stream);
                    return stream.ToArray();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    stream.Dispose();
                }
            }
        }

        public byte[] ExportarHtml()
        {
            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    Relatorio.Prepare();
                    
                    HTMLExport html = new HTMLExport();
                    html.SinglePage = true; // Single page report
                    html.Navigator = false; // Top navigation bar
                    html.EmbedPictures = true; // Embeds images into a document
                    Relatorio.Export(html, stream);

                    return stream.ToArray();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    stream.Dispose();
                }
            }
        }

        public byte[] ExportarPng()
        {
            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    Relatorio.Prepare();
                    // Export report to PDF
                    ImageExport png = new ImageExport();
                    png.ImageFormat = ImageExportFormat.Png;
                    png.SeparateFiles = false;

                    Relatorio.Export(png, stream);
                    return stream.ToArray();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    stream.Dispose();
                }
            }
        }
    }
}