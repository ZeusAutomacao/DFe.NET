using System;
using DFe.Classes.Servicos;

namespace DFe.Wsdl.Common
{
    public static class ConfiguracaoServicoWSDL
    {
        public static bool ValidarCertificadoDoServidorNetCore { get; set; }
        private static Func<IRequestSefaz> _requestSefazFactory;
        private static IEnderecoServicoProvider _enderecoProvider = new EnderecoServicoProviderDefault();

        public static void SetRequestSefazFactory(Func<IRequestSefaz> factory)
        {
            _requestSefazFactory = factory;
        }

        public static IRequestSefaz GetRequestSefaz()
        {
            return _requestSefazFactory();
        }

        /// <summary>
        ///     Define o provider de resolução de URLs dos WebServices SEFAZ.
        ///     Use EnderecoServicoProviderOverride para redirecionar para WireMock em testes.
        /// </summary>
        public static void SetEnderecoServicoProvider(IEnderecoServicoProvider provider)
        {
            _enderecoProvider = provider ?? new EnderecoServicoProviderDefault();
        }

        /// <summary>
        ///     Resolve a URL do serviço através do provider configurado.
        /// </summary>
        public static string ResolverUrl(string urlOriginal)
        {
            return _enderecoProvider.ResolverUrl(urlOriginal);
        }

        static ConfiguracaoServicoWSDL()
        {
            ValidarCertificadoDoServidorNetCore = true;
            SetRequestSefazFactory(() => new RequestSefazDefault());
        }
    }
}