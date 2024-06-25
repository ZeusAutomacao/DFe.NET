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
using DFe.Utils;
using FastReport;
using FastReport.Export.Html;
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
            Relatorio.SetParameterValue("NewLine", Environment.NewLine);
            Relatorio.SetParameterValue("Tabulation", "\t");
            Relatorio.SetParameterValue("DocumentoCancelado", config.DocumentoCancelado);
            Relatorio.SetParameterValue("DocumentoEncerrado", config.DocumentoEncerrado);
            Relatorio.SetParameterValue("Desenvolvedor", config.Desenvolvedor);
            Relatorio.SetParameterValue("QuebrarLinhasObservacao", config.QuebrarLinhasObservacao);
            ((PictureObject)Relatorio.FindObject("poEmitLogo")).Image = config.ObterLogo();
            ((ReportPage)Relatorio.FindObject("Page1")).LeftMargin = config.MargemEsquerda;
            ((ReportPage)Relatorio.FindObject("Page1")).RightMargin = config.MargemDireita;
            ((ReportPage)Relatorio.FindObject("Page1")).TopMargin = config.MargemSuperior;
            ((ReportPage)Relatorio.FindObject("Page1")).BottomMargin = config.MargemInferior;
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

        /// <summary>
        /// Converte o DAMDFe para HTML e salva-o no caminho/arquivo indicado
        /// </summary>
        public MemoryStream ExportarPdf()
        {
            Relatorio.DoublePass = true;
            Relatorio.SmoothGraphics = false;
            FastReport.Utils.Config.WebMode = true;

            Relatorio.Prepare();

            var pdfExport = new PDFSimpleExport();

            var stream = new MemoryStream();
            Relatorio.Report.Export(pdfExport, stream);
            //pdfExport.Export(Relatorio, stream);
            Relatorio.Dispose();
            pdfExport.Dispose();

            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Converte o DAMDFe para HTML e salva-o no caminho/arquivo indicado
        /// </summary>
        public Stream ObterHTML()
        {
            Relatorio.DoublePass = true;
            Relatorio.SmoothGraphics = false;
            Relatorio.Prepare();

            using (var html = new HTMLExport())
            {
                html.EmbedPictures = true;
                html.SinglePage = false;
                html.SubFolder = false;
                html.Layers = true;
                html.Navigator = false;
                html.Pictures = true;
                html.EnableMargins = true;
                html.SaveStreams = true;
                html.Wysiwyg = true;

                var stream = new MemoryStream();
                Relatorio.Export(html, stream);

                return stream;
            }
        }
    }
}