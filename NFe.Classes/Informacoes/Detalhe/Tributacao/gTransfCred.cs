using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gTransfCred
    {
        private decimal _vIbs;
        private decimal _vCbs;

        // UB107
        [XmlElement(Order = 1)]
        public decimal vIBS
        {
            get => _vIbs.Arredondar(2);
            set => _vIbs = value.Arredondar(2);
        }

        // UB108
        [XmlElement(Order = 2)]
        public decimal vCBS
        {
            get => _vCbs.Arredondar(2);
            set => _vCbs = value.Arredondar(2);
        }
    }
}