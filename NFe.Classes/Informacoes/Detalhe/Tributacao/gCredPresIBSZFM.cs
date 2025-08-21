using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gCredPresIBSZFM
    {
        private decimal? _vCredPresIbsZfm;

        // UB110
        [XmlElement(Order = 1)]
        public tpCredPresIBSZFM tpCredPresIBSZFM { get; set; }

        // UB111
        [XmlElement(Order = 2)]
        public decimal? vCredPresIBSZFM
        {
            get => _vCredPresIbsZfm.Arredondar(2);
            set => _vCredPresIbsZfm = value.Arredondar(2);
        }

        public bool ShouldSerializevCredPresIBSZFM()
        {
            return vCredPresIBSZFM.HasValue;
        }
    }
}