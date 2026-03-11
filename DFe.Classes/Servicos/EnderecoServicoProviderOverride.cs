using System;

namespace DFe.Classes.Servicos
{
    /// <summary>
    ///     Implementação que redireciona todas as URLs SEFAZ para um servidor alternativo (ex: WireMock).
    ///     Mantém o path original da URL, substituindo apenas scheme, host e porta.
    /// </summary>
    public class EnderecoServicoProviderOverride : IEnderecoServicoProvider
    {
        private readonly string _baseUrlOverride;

        /// <param name="baseUrlOverride">URL base do servidor mock (ex: "http://localhost:8080")</param>
        public EnderecoServicoProviderOverride(string baseUrlOverride)
        {
            if (string.IsNullOrWhiteSpace(baseUrlOverride))
                throw new ArgumentNullException(nameof(baseUrlOverride));

            _baseUrlOverride = baseUrlOverride.TrimEnd('/');
        }

        public string ResolverUrl(string urlOriginal)
        {
            if (string.IsNullOrWhiteSpace(urlOriginal))
                return urlOriginal;

            var uri = new Uri(urlOriginal);
            var overrideUri = new Uri(_baseUrlOverride);
            return new UriBuilder(overrideUri.Scheme, overrideUri.Host, overrideUri.Port, uri.AbsolutePath).Uri.ToString();
        }
    }
}
