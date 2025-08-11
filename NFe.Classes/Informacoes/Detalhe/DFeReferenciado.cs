namespace NFe.Classes.Informacoes.Detalhe
{
    public class DFeReferenciado
    {
        // VC02
        public string chaveAcesso { get; set; }

        // VC03
        public int? nItem { get; set; }

        public bool ShouldSerializenItem()
        {
            return nItem.HasValue;
        }
    }
}