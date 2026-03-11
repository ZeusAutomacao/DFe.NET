namespace DFe.Classes.Servicos
{
    /// <summary>
    ///     Implementação padrão que retorna a URL original sem alteração.
    /// </summary>
    public class EnderecoServicoProviderDefault : IEnderecoServicoProvider
    {
        public string ResolverUrl(string urlOriginal) => urlOriginal;
    }
}
