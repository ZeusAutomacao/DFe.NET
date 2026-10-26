using System;
using System.IO;
using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Pdf;

namespace CTe.Dacte.Fast.Skia
{
    public class DacteFastBase
    {
        public Report Relatorio { get; protected set; }

        public void LoadReport(string arquivoRelatorio)
        {
            Relatorio.Load(arquivoRelatorio);
        }

        public void LoadReport(MemoryStream stream)
        {
            Relatorio.Load(stream);
        }

        /// <summary>
        /// Converte o DACTE para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DACTE</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFExport(), arquivo);
        }

        /// <summary>
        /// Converte o DACTE para PDF e copia para o stream
        /// </summary>
        /// <param name="outputStream">Variável do tipo Stream para output</param>
        public void ExportarPdf(Stream outputStream)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFExport(), outputStream);
            outputStream.Position = 0;
        }

        /// <summary>
        /// Converte o DACTE para PDF retorna como byte[]
        /// </summary>
        public byte[] ExportarPdf()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Relatorio.Prepare();
                Relatorio.Export(new PDFExport(), stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Converte o DACTE para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DACTE</param>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public void ExportarPdf(string arquivo, FastReport.Export.ExportBase exportBase)
        {
            if (exportBase == null)
                throw new NullReferenceException("exportBase deve ter um objeto instanciado, tente 'new PDFExport()'");

            Relatorio.Prepare();
            Relatorio.Export(exportBase, arquivo);
        }

        /// <summary>
        /// Converte o DACTE para PDF e copia para o stream
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

        /// <summary>
        /// Converte o DACTE para PDF retorna como byte[]
        /// </summary>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public byte[] ExportarPdf(FastReport.Export.ExportBase exportBase)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Relatorio.Prepare();
                Relatorio.Export(exportBase, stream);
                return stream.ToArray();
            }
        }

        public byte[] ExportarHtml()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Relatorio.Prepare();
                HTMLExport html = new HTMLExport
                {
                    SinglePage = true,
                    Navigator = false,
                    EmbedPictures = true
                };
                Relatorio.Export(html, stream);
                return stream.ToArray();
            }
        }

        public void ExportarHtml(Stream outputStream)
        {
            Relatorio.Prepare();
            HTMLExport html = new HTMLExport
            {
                SinglePage = true,
                Navigator = false,
                EmbedPictures = true
            };
            Relatorio.Export(html, outputStream);
            outputStream.Position = 0;
        }
    }
}
