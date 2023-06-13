using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using DFe.Classes.Flags;
using NFe.Classes;
using NFe.Danfe.Html;
using NFe.Danfe.Html.CrossCutting;
using NFe.Danfe.Html.Dominio;
using NFe.Danfe.Html.Interfaces;
using NFe.Utils.NFe;

namespace NFe.Danfe.App.Teste.Html
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void DanfeA4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

                var danfe = new DanfeNFe(proc.NFe, Status.Autorizada, "55512121121211", "Emissor Fiscal DSBR Brasil - www.dsbrbrasil.com.br");
                IDanfeHtml2 d1 = new DanfeNfeHtml2(danfe);
                var doc = d1.ObterDocHtmlAsync().Result;
                
                var nomeArquivo = $"danfe-{proc.NFe.infNFe.Id}.html";
                var dir = AppDomain.CurrentDomain.BaseDirectory;

                var caminhoCompleto = Path.Combine(dir, nomeArquivo);

                File.WriteAllText(caminhoCompleto, doc.Html);
                Process.Start(caminhoCompleto);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }
    }
}
