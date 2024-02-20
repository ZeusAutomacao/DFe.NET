namespace DFe.Wsdl.Common
{
    public static class ConfiguracaoServicoWSDL
    {
        public static bool ValidarCertificadoDoServidorNetCore { get; set; }
        public static IRequestSefaz RequestSefaz { get; set; }

        static ConfiguracaoServicoWSDL()
        {
            ValidarCertificadoDoServidorNetCore = true;
            RequestSefaz = new RequestSefazDefault();
        }
    }
}