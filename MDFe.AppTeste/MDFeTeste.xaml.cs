using System.Windows;

namespace MDFe.AppTeste
{
    public partial class MDFeTeste
    {
        private readonly MDFeTesteModel _model;

        public MDFeTeste()
        {
            _model = new MDFeTesteModel();
            InitializeComponent();
            DataContext = _model;
        }

        private void Enviar1_0_Click(object sender, RoutedEventArgs e)
        {
            _model.CriarEnviar100();
        }

        private void ArquivoCertificado_Click(object sender, RoutedEventArgs e)
        {
            _model.ObterCertificadoArquivo();
        }

        private void Certificado_Click(object sender, RoutedEventArgs e)
        {
            _model.ObterSerialCertificado();
        }

        private void SalvarConfiguracoesXml_Click(object sender, RoutedEventArgs e)
        {
            _model.SalvarConfiguracoesXml();
        }

        private void MDFeTeste_OnLoaded(object sender, RoutedEventArgs e)
        {
            _model.CarregarConfiguracoesMDFe();
        }

        private void BuscarDiretorioSchema_Click(object sender, RoutedEventArgs e)
        {
            _model.BuscarDiretorioSchema();
        }

        private void GerarESalvar1_0_Click(object sender, RoutedEventArgs e)
        {
            _model.GerarESalvar1_0();
        }

        private void BuscarDiretorioSalvarXml_Click(object sender, RoutedEventArgs e)
        {
            _model.BuscarDiretorioSalvarXml();
        }

        private void ConsultaPorRecibo_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaPorRecibo1_0();
        }

        private void ConsultaPorProtocolo_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaPorProtocolo1_0();
        }

        private void ConsultaStatus_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaStatusServico1_0();
        }

        private void ConsultaNaoEncerrados1_0_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaNaoEncerrados1_0();
        }

        private void EventoIncluirCondutor_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoIncluirCondutor1_0();
        }

        private void EventoEncerrarMDFe1_0_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoEncerramento1_0();
        }
    }
}
