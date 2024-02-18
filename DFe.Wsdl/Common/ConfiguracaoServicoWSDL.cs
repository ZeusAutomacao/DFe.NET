namespace DFe.Wsdl.Common
{
    public static class ConfiguracaoServicoWSDL
    {
        public static bool ValidarCertificadoDoServidorNetCore { get; set; }

        static ConfiguracaoServicoWSDL()
        {
            ValidarCertificadoDoServidorNetCore = true;
        }
    }
}