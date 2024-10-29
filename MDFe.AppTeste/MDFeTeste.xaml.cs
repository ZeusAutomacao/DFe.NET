using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace MDFe.AppTeste
{
    public partial class MDFeTeste
    {
        private readonly MDFeTesteModel _model;

        public MDFeTeste()
        {
            _model = new MDFeTesteModel();
            _model.SucessoSync += Sucesso;
            InitializeComponent();
            DataContext = _model;
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

        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            _model.CriarEnviar();
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

        private void GerarESalvar_Click(object sender, RoutedEventArgs e)
        {
            _model.GerarESalvar();
        }

        private void BuscarDiretorioSalvarXml_Click(object sender, RoutedEventArgs e)
        {
            _model.BuscarDiretorioSalvarXml();
        }

        private void ConsultaPorRecibo_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaPorRecibo();
        }

        private void ConsultaPorProtocolo_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaPorProtocolo();
        }

        private void ConsultaStatus_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaStatusServico();
        }

        private void ConsultaNaoEncerrados_Click(object sender, RoutedEventArgs e)
        {
            _model.ConsultaNaoEncerrados();
        }

        private void EventoIncluirCondutor_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoIncluirCondutor();
        }

        private void EventoEncerrarMDFe_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoEncerramento();
        }

        private void EventoCancelarMDFe_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoCancelar();
        }

        private void EventoIncluirDFe_Click(object sender, RoutedEventArgs e)
        {
            _model.EventoIncluirDFe();
        }
    }
}