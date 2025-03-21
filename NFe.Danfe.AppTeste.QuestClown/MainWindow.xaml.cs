using Microsoft.Win32;
using NFe.Classes;
using NFe.Danfe.QuestPdf.ImpressaoNfce;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Windows;
using System.Windows.Controls;

namespace NFe.Danfe.AppTeste.QuestPdf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private void Button_GerarDanfe_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Arquivos XML (*.xml)|*.xml",
                Title = "Selecione um arquivo XML da NFC-e"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var selectedItem = ComboBox_TamanhoImpressao.SelectedItem as ComboBoxItem;
                var tagValue = selectedItem?.Tag?.ToString();

                if (int.TryParse(tagValue, out int tamanhoEnumValue))
                {
                    var tamanho = (TamanhoImpressao)tamanhoEnumValue;
                    GerarDanfeNfce(openFileDialog.FileName, tamanho);
                }
                else
                {
                    MessageBox.Show("Selecione um tamanho de impressão válido.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private static void GerarDanfeNfce(string caminhoXml, TamanhoImpressao tamanho)
        {
            try
            {
                var proc = new nfeProc().CarregarDeArquivoXml(caminhoXml);
                var xml = proc.ObterXmlString();

                var documento = new DanfeNfceDocument(xml, null);
                documento.TamanhoImpressao(tamanho);
                documento.GeneratePdfAndShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar DANFE NFC-e: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
