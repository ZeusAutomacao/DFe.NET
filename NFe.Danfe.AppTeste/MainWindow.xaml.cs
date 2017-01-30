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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Danfe.Fast.NFCe;
using NFe.Classes;
using NFe.Danfe.Fast.NFe;
using NFe.Utils.NFe;
using NFe.Classes.Servicos.Consulta;
using NFe.Danfe.Base.NFe;

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
            try
            {
                #region Carrega um XML com nfeProc para a variável

                var arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;
                var proc = new nfeProc().CarregarDeArquivoXml(arquivoXml);
                if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFCe)
                    throw new Exception("O XML informado não é um NFCe!");

                #endregion

                #region Abre a visualização do relatório para impressão

                var danfe = new DanfeFrNfce(proc, _configuracoes.ConfiguracaoDanfeNfce, _configuracoes.CIdToken, _configuracoes.Csc);
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

        private void BtnNfceDanfeOff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Carrega um XML com nfeProc para a variável

                var arquivoXml = Funcoes.BuscarArquivoXml();
                if (string.IsNullOrEmpty(arquivoXml))
                    return;
                var nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
                if (nfe.infNFe.ide.mod != ModeloDocumento.NFCe)
                    throw new Exception("O XML informado não é um NFCe!");

                #endregion

                #region Abre a visualização do relatório para impressão

                var danfe = new DanfeFrNfce(nfe, _configuracoes.ConfiguracaoDanfeNfce, _configuracoes.CIdToken, _configuracoes.Csc);
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

        private void BtnNfeDanfeA4_Click(object sender, RoutedEventArgs e)
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

                #endregion

                #region Abre a visualização do relatório para impressão
                var danfe = new DanfeFrNfe(proc, new ConfiguracaoDanfeNfe(_configuracoes.ConfiguracaoDanfeNfce.Logomarca, rdbDuasLinhas.IsChecked ?? false, chbCancelado.IsChecked ?? false), "NOME DA SOFTWARE HOUSE");
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
                var danfe = new DanfeFrEvento(proc, procEvento, new ConfiguracaoDanfeNfe(_configuracoes.ConfiguracaoDanfeNfce.Logomarca, rdbDuasLinhas.IsChecked ?? false, chbCancelado.IsChecked ?? false), "NOME DA SOFTWARE HOUSE");
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
    }

}
