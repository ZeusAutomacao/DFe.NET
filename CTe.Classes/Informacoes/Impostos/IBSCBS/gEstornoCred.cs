using DFe.Classes;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gEstornoCred
    {
        private decimal _vIBSEstCred;
        private decimal _vCBSEstCred;

        [XmlElement(Order = 1)]
        public decimal vIBSEstCred
        {
            get => _vIBSEstCred.Arredondar(2);
            set => _vIBSEstCred = value.Arredondar(2);
        }

        [XmlElement(Order = 2)]
        public decimal vCBSEstCred
        {
            get => _vCBSEstCred.Arredondar(2);
            set => _vCBSEstCred = value.Arredondar(2);
        }
    }
}
