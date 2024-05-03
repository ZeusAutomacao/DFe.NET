using System;

namespace DFe.Wsdl.Common
{
    public static class ConfiguracaoServicoWSDL
    {
        public static bool ValidarCertificadoDoServidorNetCore { get; set; }
        private static Func<IRequestSefaz> _requestSefazFactory;

        public static void SetRequestSefazFactory(Func<IRequestSefaz> factory)
        {
            _requestSefazFactory = factory;
        }

        public static IRequestSefaz GetRequestSefaz()
        {
            return _requestSefazFactory();
        }

        static ConfiguracaoServicoWSDL()
        {
            ValidarCertificadoDoServidorNetCore = true;
            SetRequestSefazFactory(() => new RequestSefazDefault());
        }
    }
}