using DFe.Classes.Flags;
using NFe.Classes;
using NFe.Danfe.Base.NFe;
using NFe.Danfe.Fast.Standard.NFCe;
using NFe.Danfe.Fast.Standard.NFe;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows;
using DFe.Utils;
using NFe.Utils.NFe;
using NFe.Classes.Servicos.Consulta;
using NFe.Danfe.Base;

namespace NFe.Danfe.AppTeste.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // MIME header with default value
            string mime = "application/json";

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    var report = BtnNfeDanfeA4_Click();
                    var bytes = report.ExportarPdf();

                    //para exemplos com aspnet core, de como retornar os bytes:
                    //https://github.com/FastReports/FastReport/blob/master/Demos/OpenSource/FastReport.OpenSource.Web.Vue/Controllers/ReportsController.cs

                    File.WriteAllBytes(@"D:\teste.pdf", bytes);
                }
                // Handle exceptions
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    stream.Dispose();
                }
            }
        }

        private static DanfeFrNfe BtnNfeDanfeA4_Click()
        {
            ConfiguracaoDanfeNfe configuracaoDanfeNfe = new ConfiguracaoDanfeNfe();
            try
            {
                #region Carrega um XML com nfeProc para a variável

                var arquivoXml = @"D:\35190227357612000192550010000001111381546746.xml";
                if (string.IsNullOrEmpty(arquivoXml))
                    throw new Exception("Arquivo não definido");

                nfeProc proc = null;

                try
                {
                    proc = new nfeProc().CarregarDeArquivoXml(arquivoXml);
                }
                catch //Carregar NFe ainda não transmitida à sefaz, como uma pré-visualização.
                {
                    proc = new nfeProc() { NFe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml), protNFe = new Classes.Protocolo.protNFe() };
                }

                if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe)
                    throw new Exception("O XML informado não é um NFe!");

                /*
                //Carregar atravez de um stream....                   
                var stream = new StreamReader(arquivoXml, Encoding.GetEncoding("ISO-8859-1"));
                var proc = new nfeProc().CarregardeStream(stream);               
                */
                #endregion

                #region Abre a visualização do relatório para impressão
                var danfe = new DanfeFrNfe(proc: proc,
                                    configuracaoDanfeNfe: new ConfiguracaoDanfeNfe()
                                    {
                                        Logomarca = configuracaoDanfeNfe.Logomarca,
                                        DuasLinhas = false,
                                        DocumentoCancelado = false,
                                        QuebrarLinhasObservacao = configuracaoDanfeNfe.QuebrarLinhasObservacao,
                                        ExibirResumoCanhoto = configuracaoDanfeNfe.ExibirResumoCanhoto,
                                        ResumoCanhoto = configuracaoDanfeNfe.ResumoCanhoto,
                                        ChaveContingencia = configuracaoDanfeNfe.ChaveContingencia,
                                        ExibeCampoFatura = configuracaoDanfeNfe.ExibeCampoFatura,
                                        ImprimirISSQN = configuracaoDanfeNfe.ImprimirISSQN,
                                        ImprimirDescPorc = configuracaoDanfeNfe.ImprimirDescPorc,
                                        ImprimirTotalLiquido = configuracaoDanfeNfe.ImprimirTotalLiquido,
                                        ImprimirUnidQtdeValor = configuracaoDanfeNfe.ImprimirUnidQtdeValor,
                                        ExibirTotalTributos = configuracaoDanfeNfe.ExibirTotalTributos
                                    },
                                    desenvolvedor: "NOME DA SOFTWARE HOUSE",
                                    arquivoRelatorio: string.Empty);

                return danfe;

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
