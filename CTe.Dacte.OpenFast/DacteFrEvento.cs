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
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using DFe.Utils;
using FastReport;
using FastReport.Export.PdfSimple;

namespace CTe.Dacte.OpenFast
{
    public class DacteFrEvento
    {
        public Report Relatorio;

        public DacteFrEvento()
        {
            Relatorio = new Report();
        }
        public DacteFrEvento(cteProc proc, procEventoCTe procEventoCTe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
            Relatorio = new Report();

            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"CTe\CTeEvento.frx";
                var frx = FrxFileHelper.TryGetFrxFile(caminho);
                Relatorio.Load(new MemoryStream(frx));
            }
            else
            {
                Relatorio.Load(arquivoRelatorio);
            }

            Configurar(desenvolvedor: desenvolvedor);
        }

        public void LoadReport(string arquivoRelatorio)
        {
            Relatorio.Load(arquivoRelatorio);
        }

        public void LoadReport(MemoryStream stream)
        {
            Relatorio.Load(stream);
        }

        public void RegisterData(cteProc proc, procEventoCTe procEventoCTe)
        {
            Relatorio.RegisterData(new[] { proc }, "cteProc", 20);
            Relatorio.GetDataSource("cteProc").Enabled = true;

            Relatorio.RegisterData(new[] { procEventoCTe }, "procEventoCTe", 20);
            Relatorio.GetDataSource("procEventoCTe").Enabled = true;
        }

        public void Configurar(string desenvolvedor = "")
        {
            Relatorio.SetParameterValue("Desenvolvedor", desenvolvedor);
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
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
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
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
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
