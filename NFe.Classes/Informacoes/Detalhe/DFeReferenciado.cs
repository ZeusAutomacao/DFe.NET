namespace NFe.Classes.Informacoes.Detalhe
{
    public class DFeReferenciado
    {
        /// <summary>
        ///     VC02 - Chave de acesso do DF-e referenciado
        /// </summary>
        public string chaveAcesso { get; set; }

        /// <summary>
        ///     VC03 - Número do item do documento referenciado.
        /// </summary>
        public int? nItem { get; set; }
    }
}