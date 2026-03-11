namespace DFe.Classes.Servicos
{
    /// <summary>
    ///     Interface para resolução/override de URLs dos WebServices SEFAZ.
    ///     Permite redirecionar chamadas para WireMock ou outro mock server em testes.
    /// </summary>
    public interface IEnderecoServicoProvider
    {
        /// <summary>
        ///     Resolve a URL do serviço, podendo substituí-la por uma URL alternativa (ex: WireMock).
        /// </summary>
        /// <param name="urlOriginal">URL original do WebService SEFAZ</param>
        /// <returns>URL resolvida (original ou sobreposta)</returns>
        string ResolverUrl(string urlOriginal);
    }
}
