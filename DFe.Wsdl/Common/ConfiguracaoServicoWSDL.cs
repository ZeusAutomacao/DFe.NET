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

            //a partir de .net 9 utilizar o HttpClient
            //pois o WebRequest, HttpWebRequest, ServicePoint, and WebClient foi DESCONTINUADO
            //Ver https://github.com/Hercules-NET/ZeusFiscal/issues/59
#if NET9_0_OR_GREATER
            SetRequestSefazFactory(() => new RequestSefazHttpClientHandler());
#else
            SetRequestSefazFactory(() => new RequestSefazDefault());
#endif
        }
    }
}