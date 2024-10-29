using System;
using System.IO;
using DFe.Utils;
using FastReport;
using FastReport.Export.PdfSimple;
using MDFe.Classes.Retorno;
using MDFe.Damdfe.Base;

namespace MDFe.Damdfe.OpenFast
{
    public class DamdfeFrMDFe
    {
        protected Report Relatorio;

        public DamdfeFrMDFe(MDFeProcMDFe proc, ConfiguracaoDamdfe config, string arquivoRelatorio = null)
        {
            Relatorio = new Report();
            RegisterData(proc);

            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"MDFe\MDFeRetrato.frx";
                var frx = FrxFileHelper.TryGetFrxFile(caminho);
                Relatorio.Load(new MemoryStream(frx));
            }
            else
            {
                Relatorio.Load(arquivoRelatorio);
            }
                
            Configurar(config);            
        }

        public DamdfeFrMDFe()
        {
            Relatorio = new Report();
        }

        public void RegisterData(MDFeProcMDFe proc)
        {
            Relatorio.RegisterData(new[] { proc }, "MDFeProcMDFe", 20);
            Relatorio.GetDataSource("MDFeProcMDFe").Enabled = true;            
        }

        public void Configurar(ConfiguracaoDamdfe config)
        {
            Relatorio.SetParameterValue("DoocumentoCancelado", config.DocumentoCancelado);
            Relatorio.SetParameterValue("DocumentoEncerrado", config.DocumentoEncerrado);
            Relatorio.SetParameterValue("Desenvolvedor", config.Desenvolvedor);
            Relatorio.SetParameterValue("QuebrarLinhasObservacao", config.QuebrarLinhasObservacao);
            ((PictureObject)Relatorio.FindObject("poEmitLogo")).SetImageData(config.Logomarca);
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFSimpleExport(), arquivo);
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e copia para o stream
        /// </summary>
        /// <param name="outputStream">Variável do tipo Stream para output</param>
        public void ExportarPdf(Stream outputStream)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFSimpleExport(), outputStream);
            outputStream.Position = 0;
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public void ExportarPdf(string arquivo, FastReport.Export.ExportBase exportBase)
        {
            if (exportBase == null)
                throw new NullReferenceException("exportBase deve ter um objeto instanciado, tente 'new PDFExport()'");

            Relatorio.Prepare();
            Relatorio.Export(exportBase, arquivo);
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e copia para o stream
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
    }
}