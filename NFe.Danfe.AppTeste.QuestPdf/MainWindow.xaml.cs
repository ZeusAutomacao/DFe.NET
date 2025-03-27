using Microsoft.Win32;
using NFe.Classes;
using NFe.Danfe.QuestPdf.ImpressaoNfce;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NFe.Danfe.AppTeste.QuestPdf
{
    public partial class MainWindow : Window
    {
        private byte[]? _logoMarcaBytes;

        public MainWindow()
        {
            InitializeComponent();
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private void Button_GerarDanfe_Click(object sender, RoutedEventArgs e)
        {
            var caminhoXml = SelecionarArquivoXml();
            if (string.IsNullOrEmpty(caminhoXml))
                return;

            var tamanho = ObterTamanhoImpressaoSelecionado();
            if (tamanho == null)
            {
                MessageBox.Show("Selecione um tamanho de impressão válido.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                GerarDanfeNfce(caminhoXml, tamanho.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar DANFE NFC-e: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                _logoMarcaBytes = ConverterParaPngBytes(bitmap);
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

        private static string? SelecionarArquivoXml()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Arquivos XML (*.xml)|*.xml",
                Title = "Selecione um arquivo XML da NFC-e"
            };

            var result = dialog.ShowDialog() == true ? dialog.FileName : null;
            return result;
        }

        private static string? SelecionarImagem()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Imagens (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Selecione uma imagem para a logomarca"
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

        private static byte[] ConverterParaPngBytes(BitmapImage bitmap)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using var stream = new MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
        }

        private TamanhoImpressao? ObterTamanhoImpressaoSelecionado()
        {
            var selectedItem = ComboBox_TamanhoImpressao.SelectedItem as ComboBoxItem;
            var tagValue = selectedItem?.Tag?.ToString();

            if (int.TryParse(tagValue, out int enumValue))
                return (TamanhoImpressao)enumValue;

            return null;
        }

        private void GerarDanfeNfce(string caminhoXml, TamanhoImpressao tamanho)
        {
            var proc = new nfeProc().CarregarDeArquivoXml(caminhoXml);
            var xml = proc.ObterXmlString();

            var documento = new DanfeNfceDocument(xml, _logoMarcaBytes);
            documento.TamanhoImpressao(tamanho);
            documento.GeneratePdfAndShow();
        }
    }
}
