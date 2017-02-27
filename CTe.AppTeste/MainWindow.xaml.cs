using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace CTe.AppTeste
{
    public partial class MainWindow
    {
        private readonly CTeTesteModel _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new CTeTesteModel();
            DataContext = _model;
            _model.SucessoSync += Sucesso;
        }

        private readonly string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private void Sucesso(object sender, RetornoEEnvio e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                XmlTemp(e.Envio, "envio-tmp.xml");
                XmlTemp(e.Retorno, "retorno-tmp.xml");
                WebXmlEnvio.Navigate(_path + @"\envio-tmp.xml");
                WebXmlRetorno.Navigate(_path + @"\retorno-tmp.xml");
            }));
        }

        private void XmlTemp(string xml, string nomeXml)
        {
            var stw = new StreamWriter(_path + @"\" + nomeXml);
            stw.WriteLine(xml);
            stw.Close();
        }

        private void Certificado_Click(object sender, RoutedEventArgs e)
        {
            _model.ObterSerialCertificado();
        }

        private void ArquivoCertificado_Click(object sender, RoutedEventArgs e)
        {
            _model.ObterCertificadoArquivo();
        }

        private void BuscarDiretorioSchema_Click(object sender, RoutedEventArgs e)
        {
            _model.BuscarDiretorioSchema();
        }

        private void BuscarDiretorioSalvarXml_Click(object sender, RoutedEventArgs e)
        {
            _model.BuscarDiretorioSalvarXml();
        }

        private void SalvarConfiguracoesXml_Click(object sender, RoutedEventArgs e)
        {
            _model.SalvarConfiguracoesXml();
        }

        private void ConsultarStatusServico(object sender, RoutedEventArgs e)
        {
            _model.ConsultarStatusServico2();
        }

        private void ConsultarStatusServico3Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ConsultaPorRecibo_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ConsultaPorProtocolo_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ConsultaStatus_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ConsultaNaoEncerrados1_0_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void EventoIncluirCondutor_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void EventoEncerrarMDFe1_0_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void EventoCancelarMDFe1_0_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _model.CarregarConfiguracoes();
        }
    }
}
