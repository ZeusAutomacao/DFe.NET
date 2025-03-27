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

using Microsoft.Win32;
using NFe.Danfe.PdfClown;
using NFe.Danfe.PdfClown.Modelo;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NFe.Danfe.AppTeste.PdfClown
{
    public partial class MainWindow : Window
    {
        private byte[]? _logoMarcaBytes;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCarregarLogo_Click(object sender, RoutedEventArgs e)
        {
            var caminhoImagem = SelecionarImagem();
            if (string.IsNullOrEmpty(caminhoImagem))
                return;

            try
            {
                var bitmap = CarregarImagem(caminhoImagem);
                LogoEmitente.Source = bitmap;
                _logoMarcaBytes = ConverterParaJpegBytes(bitmap);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar logomarca: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRemoverLogo_Click(object sender, RoutedEventArgs e)
        {
            _logoMarcaBytes = null;
            LogoEmitente.Source = null;
        }

        private void Button_GerarDanfe_Click(object sender, RoutedEventArgs e)
        {
            var caminhoXml = SelecionarXml();
            if (string.IsNullOrEmpty(caminhoXml))
                return;

            try
            {
                var xml = File.ReadAllText(caminhoXml);
                var pdfBytes = GerarDanfeZion(xml, _logoMarcaBytes);
                AbrirPdfTemporario(pdfBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar DANFE: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string? SelecionarImagem()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Imagens (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Selecione uma imagem para a logomarca"
            };

            var result = dialog.ShowDialog() == true ? dialog.FileName : null;
            return result;
        }

        private static string? SelecionarXml()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Arquivos XML (*.xml)|*.xml",
                Title = "Selecione o XML da NF-e"
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        private static BitmapImage CarregarImagem(string caminho)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(caminho);
            bitmap.EndInit();
            return bitmap;
        }

        private static byte[] ConverterParaJpegBytes(BitmapImage bitmap)
        {
            var imageControl = new Image
            {
                Source = bitmap,
                Width = bitmap.PixelWidth,
                Height = bitmap.PixelHeight
            };

            imageControl.Measure(new Size(bitmap.PixelWidth, bitmap.PixelHeight));
            imageControl.Arrange(new Rect(new Size(bitmap.PixelWidth, bitmap.PixelHeight)));

            var render = new RenderTargetBitmap(bitmap.PixelWidth, bitmap.PixelHeight, 96, 96, System.Windows.Media.PixelFormats.Pbgra32);
            render.Render(imageControl);

            var encoder = new JpegBitmapEncoder { QualityLevel = 100 };
            encoder.Frames.Add(BitmapFrame.Create(render));

            using var stream = new MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
        }

        private static byte[] GerarDanfeZion(string xmlNfeProc, byte[]? logoMarca)
        {
            xmlNfeProc = xmlNfeProc.Replace("\u00a0", " ");
            var model = DanfeViewModelCreator.CriarDeStringXml(xmlNfeProc);

            using var pdfStream = new MemoryStream();
            using var danfe = new DanfeDoc(model);

            if (logoMarca != null)
            {
                using var logo = new MemoryStream(logoMarca);
                danfe.AdicionarLogoImagem(logo);
            }

            danfe.Gerar();
            return danfe.ObterPdfBytes(pdfStream);
        }

        private static void AbrirPdfTemporario(byte[] pdfBytes)
        {
            var caminhoTemporario = Path.Combine(Path.GetTempPath(), "danfe-nfe.pdf");
            File.WriteAllBytes(caminhoTemporario, pdfBytes);
            Process.Start(new ProcessStartInfo(caminhoTemporario) { UseShellExecute = true });
        }
    }
}
