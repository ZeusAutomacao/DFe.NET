using System.Windows;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.AppTeste
{
    public partial class MDFeTeste
    {
        private MDFeTesteModel _model;

        public MDFeTeste()
        {
            _model = new MDFeTesteModel();
            InitializeComponent();
            DataContext = _model;
        }

        private void Enviar1_0_Click(object sender, RoutedEventArgs e)
        {
            var mdfe = new MDFeEletronico();
            mdfe.InfMDFe.Ide.CUF = EstadoUF.GO;
            mdfe.InfMDFe.Ide.TpAmb = TipoAmbiente.Homologacao;
            mdfe.InfMDFe.Ide.TpEmit = MDFeTipoEmitente.PrestadorServicoDeTransporte;
            mdfe.InfMDFe.Ide.Mod = MDFeModelo.MDFe;
            mdfe.InfMDFe.Ide.Serie = 750;

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
            _model.CarregarConfiguracoes();
        }
    }
}
