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
using FastReport;
#if (NETSTANDARD || NETCOREAPP)
using FastReport.Export.Html;
using FastReport.Export.Image;
using FastReport.Export.PdfSimple;
#else
using FastReport.Export.Pdf;
#endif
using NFe.Danfe.Base;

namespace NFe.Danfe.Fast
{
    public class DanfeBase: IDanfe
    {
        public Report Relatorio { get; protected set; }

#if NETFRAMEWORK

        /// <summary>
        /// Abre a janela de visualização do DANFE da NFCe
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        public void Visualizar(bool modal = true)
        {
            Relatorio.Show(modal);
        }

        /// <summary>
        ///  Abre a janela de visualização do design do DANFE da NFCe.
        /// Chame esse método se desja fazer alterações no design do DANFE em modo run-time
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        public void ExibirDesign(bool modal = false)
        {
            Relatorio.Design(modal);
        }

        /// <summary>
        /// Envia a impressão do DANFE da NFCe diretamente para a impressora
        /// </summary>
        /// <param name="exibirDialogo">Se true exibe o diálogo Imprimindo...</param>
        /// <param name="impressora">Passe a string com o nome da impressora para imprimir diretamente em determinada impressora. Caso contrário, a impressão será feita na impressora que estiver como padrão</param>
        public void Imprimir(bool exibirDialogo = true, string impressora = "")
        {
            Relatorio.PrintSettings.ShowDialog = exibirDialogo;
            Relatorio.PrintSettings.Printer = impressora;
            Relatorio.Print();
        }

#endif

        /// <summary>
        /// Converte o DANFE para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DANFE</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
#if NETFRAMEWORK
            Relatorio.Export(new PDFExport(), arquivo);
#else
            Relatorio.Export(new PDFSimpleExport(), arquivo);
#endif
        }

        /// <summary>
        /// Converte o DANFE para PDF e copia para o stream
        /// </summary>
        /// <param name="outputStream">Variável do tipo Stream para output</param>
        public void ExportarPdf(Stream outputStream)
        {
            try
            {
                Relatorio.Prepare();
#if NETFRAMEWORK
            Relatorio.Export(new PDFExport(), outputStream);
#elif (NETSTANDARD || NETCOREAPP)
            Relatorio.Export(new PDFSimpleExport(), outputStream);
#endif
            outputStream.Position = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Converte o DANFE para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DANFE</param>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public void ExportarPdf(string arquivo, FastReport.Export.ExportBase exportBase)
        {
            if (exportBase == null)
                throw new NullReferenceException("exportBase deve ter um objeto instanciado, tente 'new PDFExport()'");

            Relatorio.Prepare();
            Relatorio.Export(exportBase, arquivo);
        }

        /// <summary>
        /// Converte o DANFE para PDF e copia para o stream
        /// </summary>
        /// <param name="outputStream">Variável do tipo Stream para output</param>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public void ExportarPdf(Stream outputStream, FastReport.Export.ExportBase exportBase)
        {
            if (exportBase == null)
                throw new NullReferenceException("exportBase deve ter um objeto instanciado, tente 'new PDFExport()'");

            Relatorio.Prepare();
            Relatorio.Export(exportBase, outputStream);
            outputStream.Position = 0;
        }

#if (NETSTANDARD || NETCOREAPP)
        
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
            }
        }

        public byte[] ExportarHtml()
        {
            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    Relatorio.Prepare();
                    HTMLExport html = new HTMLExport
                    {
                        SinglePage = true, // Single page report
                        Navigator = false, // Top navigation bar
                        EmbedPictures = true // Embeds images into a document
                    };
                    Relatorio.Export(html, stream);
                    return stream.ToArray();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ExportarHtml(Stream outputStream)
        {
            try
            {
                Relatorio.Prepare();
                HTMLExport html = new HTMLExport
                {
                    SinglePage = true, // Single page report
                    Navigator = false, // Top navigation bar
                    EmbedPictures = true // Embeds images into a document
                };
                Relatorio.Export(html, outputStream);
                outputStream.Position = 0;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ExportarPng()
        {
            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    Relatorio.Prepare();
                    ImageExport png = new ImageExport
                    {
                        ImageFormat = ImageExportFormat.Png,
                        SeparateFiles = false
                    };
                    Relatorio.Export(png, stream);
                    return stream.ToArray();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ExportarPng(Stream outputStream)
        {
            try
            {
                Relatorio.Prepare();
                ImageExport png = new ImageExport
                {
                    ImageFormat = ImageExportFormat.Png,
                    SeparateFiles = false
                };
                Relatorio.Export(png, outputStream);
                outputStream.Position = 0;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

#endif
    }
}