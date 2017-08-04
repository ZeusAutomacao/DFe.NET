namespace DFe.DocumentosEletronicos.CTe.Classes.Flags
{
    /// <summary>
    ///     Usado para discriminar o tipo de evento, pois o serviço de cancelamento e carta de correção devem usar a url
    ///     designada para UF da empresa, já o serviço EPEC usa a url do ambiente nacional
    /// </summary>
    public enum TipoRecepcaoEvento
    {
        Nenhum,
        Cancelamento,
        CartaCorrecao,
        Epec,
        ManifestacaoDestinatario
    }
}