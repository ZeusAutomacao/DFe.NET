using System;
using System.IO;
using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Pdf;

namespace NFe.Danfe.Fast.Skia
{
    public class DanfeFastBase
    {
        public Report Relatorio { get; protected set; }
        
        /// <summary>
        /// Converte o DANFE para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DANFE</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFExport(), arquivo);
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
                Relatorio.Export(new PDFExport(), outputStream);
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
    }
}
