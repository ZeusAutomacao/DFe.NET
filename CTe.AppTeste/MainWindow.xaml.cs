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

        private void ConsultarStatusServico2Click(object sender, RoutedEventArgs e)
        {
            
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
