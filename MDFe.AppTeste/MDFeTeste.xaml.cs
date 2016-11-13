using System.Windows;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.AppTeste
{
    public partial class MDFeTeste
    {
        public MDFeTeste()
        {
            InitializeComponent();
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
            
        }

        private void Certificado_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
