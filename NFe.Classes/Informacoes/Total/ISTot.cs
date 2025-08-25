using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Total
{
    public class ISTot
    {
        private decimal _vIS;

        // W33
        [XmlElement(Order = 1)]
        public decimal vIS
        {
            get => _vIS;
            set => _vIS = value.Arredondar(2);
        }
    }
}