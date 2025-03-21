using Microsoft.Win32;
using NFe.Danfe.PdfClown;
using NFe.Danfe.PdfClown.Modelo;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace NFe.Danfe.AppTeste.PdfClown
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_GerarDanfe_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivos XML (*.xml)|*.xml",
                Title = "Selecione o XML da NF-e"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var caminhoXml = openFileDialog.FileName;

                try
                {
                    string xml = File.ReadAllText(caminhoXml);

                    byte[]? logo = null;

                    byte[] pdfBytes = GerarDanfeZion(xml, logo);

                    string tempFile = Path.Combine(Path.GetTempPath(), "danfe-nfe.pdf");
                    File.WriteAllBytes(tempFile, pdfBytes);
                    Process.Start(new ProcessStartInfo(tempFile) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao gerar DANFE: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
                {
                    danfe.AdicionarLogoImagem(logo);
                }
            }

            danfe.Gerar();
            return danfe.ObterPdfBytes(pdfStream);
        }
    }
}
