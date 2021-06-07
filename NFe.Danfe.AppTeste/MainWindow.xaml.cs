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
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes;
using NFe.Classes.Servicos.Consulta;
using NFe.Danfe.Base;
using NFe.Danfe.Base.NFe;
using NFe.Danfe.Fast.NFCe;
using NFe.Danfe.Fast.NFe;
using NFe.Utils.NFe;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NFe.Danfe.AppTeste
{
    /// <summary>
    ///     Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow
    {
        private const string ArquivoConfiguracao = @"\configuracao.xml";
        private ConfiguracaoApp _configuracoes;
        private readonly string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainWindow()
        {
            InitializeComponent();
            CarregarConfiguracao();
            DataContext = _configuracoes;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarConfiguracao();
        }

        private void SalvarConfiguracao()
        {
            try
            {
                _configuracoes.SalvarParaAqruivo(_path + ArquivoConfiguracao);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(string.Format("{0} \n\nDetalhes: {1}", ex.Message, ex.InnerException), "Erro",
                        MessageBoxButton.OK);
            }
        }

        private void CarregarConfiguracao()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                _configuracoes = !File.Exists(path + ArquivoConfiguracao)
                    ? new ConfiguracaoApp()
                    : FuncoesXml.ArquivoXmlParaClasse<ConfiguracaoApp>(path + ArquivoConfiguracao);

                #region Carrega a logo no controle logoEmitente

                if (_configuracoes.ConfiguracaoDanfeNfce.Logomarca != null && _configuracoes.ConfiguracaoDanfeNfce.Logomarca.Length > 0)
                    using (var stream = new MemoryStream(_configuracoes.ConfiguracaoDanfeNfce.Logomarca))
                    {
                        LogoEmitente.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }

                #endregion
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnNfceDanfe_Click(object sender, RoutedEventArgs e)
        {
            ImprimirDanfeNfce(_configuracoes.ConfiguracaoDanfeNfce.NfceLayoutQrCode);
        }

        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            var arquivo = Funcoes.BuscarImagem();
            if (string.IsNullOrEmpty(arquivo)) return;
            var imagem = Image.FromFile(arquivo);
            LogoEmitente.Source = new BitmapImage(new Uri(arquivo));

            _configuracoes.ConfiguracaoDanfeNfce.Logomarca = new byte[0];
            using (var stream = new MemoryStream())
            {
                imagem.Save(stream, ImageFormat.Png);
                stream.Close();
                _configuracoes.ConfiguracaoDanfeNfce.Logomarca = stream.ToArray();
            }
        }

        private void btnRemoveLogo_Click(object sender, RoutedEventArgs e)
        {
            LogoEmitente.Source = null;
            _configuracoes.ConfiguracaoDanfeNfce.Logomarca = null;
        }

        private void BtnNfeDanfeA4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Carrega um XML com nfeProc para a variável

                var arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;

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
                                        Logomarca = _configuracoes.ConfiguracaoDanfeNfce.Logomarca,
                                        DuasLinhas = RdbDuasLinhas.IsChecked ?? false,
                                        DocumentoCancelado = ChbCancelado.IsChecked ?? false,
                                        QuebrarLinhasObservacao = _configuracoes.ConfiguracaoDanfeNfe.QuebrarLinhasObservacao,
                                        ExibirResumoCanhoto = _configuracoes.ConfiguracaoDanfeNfe.ExibirResumoCanhoto,
                                        ResumoCanhoto = _configuracoes.ConfiguracaoDanfeNfe.ResumoCanhoto,
                                        ChaveContingencia = _configuracoes.ConfiguracaoDanfeNfe.ChaveContingencia,
                                        ExibeCampoFatura = _configuracoes.ConfiguracaoDanfeNfe.ExibeCampoFatura,
                                        ImprimirISSQN = _configuracoes.ConfiguracaoDanfeNfe.ImprimirISSQN,
                                        ImprimirDescPorc = _configuracoes.ConfiguracaoDanfeNfe.ImprimirDescPorc,
                                        ImprimirTotalLiquido = _configuracoes.ConfiguracaoDanfeNfe.ImprimirTotalLiquido,
                                        ImprimirUnidQtdeValor = _configuracoes.ConfiguracaoDanfeNfe.ImprimirUnidQtdeValor,
                                        ExibirTotalTributos = _configuracoes.ConfiguracaoDanfeNfe.ExibirTotalTributos
                                    },
                                    desenvolvedor: "NOME DA SOFTWARE HOUSE",
                                    arquivoRelatorio: string.Empty);

                danfe.Visualizar();
                //danfe.Imprimir();
                //danfe.ExibirDesign();
                //danfe.ExportarPdf(@"d:\teste.pdf");

                #endregion

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void btnEventoNFe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Carrega um XML com nfeProc para a variável

                var arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;
                var proc = new nfeProc().CarregarDeArquivoXml(arquivoXml);
                if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe)
                    throw new Exception("O XML informado não é um NFe!");


                arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;
                var procEvento = FuncoesXml.ArquivoXmlParaClasse<procEventoNFe>(arquivoXml);

                #endregion

                #region Abre a visualização do relatório para impressão
                var danfe = new DanfeFrEvento(proc, procEvento, new ConfiguracaoDanfeNfe(_configuracoes.ConfiguracaoDanfeNfce.Logomarca, RdbDuasLinhas.IsChecked ?? false, ChbCancelado.IsChecked ?? false), "NOME DA SOFTWARE HOUSE");
                danfe.Visualizar();
                //danfe.Imprimir();
                //danfe.ExibirDesign();
                //danfe.ExportarPdf(@"d:\teste.pdf");

                #endregion

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void ImprimirDanfeNfce(NfceLayoutQrCode layout)
        {
            try
            {
                #region Carrega um XML para a variável

                var arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;

                nfeProc nfeProc;

                try // Tenta carregar NFeProc
                {
                    nfeProc = FuncoesXml.ArquivoXmlParaClasse<nfeProc>(arquivoXml);
                }
                catch (Exception)  // Carrega NFCe sem protocolo
                {
                    NFe.Classes.NFe nfeContingenciaSemProc = FuncoesXml.ArquivoXmlParaClasse<NFe.Classes.NFe>(arquivoXml);
                    nfeProc = new nfeProc() { NFe = nfeContingenciaSemProc };
                }

                if (nfeProc.NFe.infNFe.ide.mod != ModeloDocumento.NFCe)
                    throw new Exception("O XML informado não é um NFCe!");

                #endregion

                #region Abre a visualização do relatório para impressão

                var danfe = new DanfeFrNfce(proc: nfeProc, configuracaoDanfeNfce: _configuracoes.ConfiguracaoDanfeNfce, cIdToken: _configuracoes.CIdToken, csc: _configuracoes.Csc, arquivoRelatorio: string.Empty);
                danfe.Visualizar();
                //danfe.Imprimir();
                //danfe.ExibirDesign();
                //danfe.ExportarPdf(@"d:\teste.pdf");

                #endregion

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnNFeSimplificado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Carrega um XML com nfeProc para a variável

                var arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;

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
                var danfe = new DanfeFrSimplificado(proc: proc,
                                    configuracaoDanfeNfe: new ConfiguracaoDanfeNfe()
                                    {
                                        Logomarca = _configuracoes.ConfiguracaoDanfeNfce.Logomarca,
                                        DuasLinhas = RdbDuasLinhas.IsChecked ?? false,
                                        DocumentoCancelado = ChbCancelado.IsChecked ?? false,
                                        QuebrarLinhasObservacao = _configuracoes.ConfiguracaoDanfeNfe.QuebrarLinhasObservacao,
                                        ExibirResumoCanhoto = _configuracoes.ConfiguracaoDanfeNfe.ExibirResumoCanhoto,
                                        ResumoCanhoto = _configuracoes.ConfiguracaoDanfeNfe.ResumoCanhoto,
                                        ChaveContingencia = _configuracoes.ConfiguracaoDanfeNfe.ChaveContingencia,
                                        ExibeCampoFatura = _configuracoes.ConfiguracaoDanfeNfe.ExibeCampoFatura,
                                        ImprimirISSQN = _configuracoes.ConfiguracaoDanfeNfe.ImprimirISSQN,
                                        ImprimirDescPorc = _configuracoes.ConfiguracaoDanfeNfe.ImprimirDescPorc,
                                        ImprimirTotalLiquido = _configuracoes.ConfiguracaoDanfeNfe.ImprimirTotalLiquido,
                                        ImprimirUnidQtdeValor = _configuracoes.ConfiguracaoDanfeNfe.ImprimirUnidQtdeValor,
                                        ExibirTotalTributos = _configuracoes.ConfiguracaoDanfeNfe.ExibirTotalTributos
                                    },
                                    desenvolvedor: "NOME DA SOFTWARE HOUSE",
                                    arquivoRelatorio: string.Empty);

                //danfe.Visualizar();
                //danfe.Imprimir();
                danfe.ExibirDesign();
                //danfe.ExportarPdf(@"d:\teste.pdf");

                #endregion

            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }
    }

}
