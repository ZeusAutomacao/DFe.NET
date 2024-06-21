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
using System;
using DFe.Utils;
using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Pdf;
using MDFe.Classes.Retorno;
using MDFe.Damdfe.Base;

namespace MDFe.Damdfe.Fast
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
            ((PictureObject)Relatorio.FindObject("poEmitLogo")).Image = config.ObterLogo();
        }
        
        /// <summary>
        /// Abre a janela de visualização do DAMDFe
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        public void Visualizar(bool modal = true)
        {
            Relatorio.Show(modal);
        }

        /// <summary>
        ///  Abre a janela de visualização do design do DAMDFe
        /// Chame esse método se desja fazer alterações no design do DAMDFe em modo run-time
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        public void ExibirDesign(bool modal = false)
        {
            Relatorio.Design(modal);
        }

        /// <summary>
        /// Envia a impressão do DAMDFe diretamente para a impressora
        /// </summary>
        /// <param name="exibirDialogo">Se true exibe o diálogo Imprimindo...</param>
        /// <param name="impressora">Passe a string com o nome da impressora para imprimir diretamente em determinada impressora. Caso contrário, a impressão será feita na impressora que estiver como padrão</param>
        public void Imprimir(bool exibirDialogo = true, string impressora = "")
        {
            Relatorio.PrintSettings.ShowDialog = exibirDialogo;
            Relatorio.PrintSettings.Printer = impressora;
            Relatorio.Print();
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFExport(), arquivo);
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e copia para o stream
        /// </summary>
        /// <param name="outputStream">Variável do tipo Stream para output</param>
        public void ExportarPdf(Stream outputStream)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFExport(), outputStream);
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

            var pdfExport = new PDFExport();

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

        /// <summary>
        /// Converte o DAMDFe para HTML e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o HTML do DAMDFe</param>
        public void ExportarHTML(string arquivo)
        {
            Relatorio.Prepare();

            var html = new HTMLExport
            {
                EmbedPictures = true,
                SinglePage = true,
                EnableVectorObjects = true,
                Wysiwyg = true,
                SubFolder = false,
                Layers = true,
                Navigator = false,
                Pictures = true,
                EnableMargins = true
            };

            Relatorio.Export(html, arquivo);
        }
    }
}