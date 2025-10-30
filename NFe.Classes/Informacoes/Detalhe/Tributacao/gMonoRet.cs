using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gMonoRet
    {
        private decimal _qBcMonoRet;
        private decimal _adRemIbsRet;
        private decimal _vIbsMonoRet;
        private decimal _adRemCbsRet;
        private decimal _vCbsMonoRet;

        // UB95
        [XmlElement(Order = 1)]
        public decimal qBCMonoRet
        {
            get => _qBcMonoRet.Arredondar(4);
            set => _qBcMonoRet = value.Arredondar(4);
        }

        // UB96
        [XmlElement(Order = 2)]
        public decimal adRemIBSRet
        {
            get => _adRemIbsRet.Arredondar(4);
            set => _adRemIbsRet = value.Arredondar(4);
        }

        // UB97
        [XmlElement(Order = 3)]
        public decimal vIBSMonoRet
        {
            get => _vIbsMonoRet.Arredondar(2);
            set => _vIbsMonoRet = value.Arredondar(2);
        }

        // UB98
        [XmlElement(Order = 4)]
        public decimal adRemCBSRet
        {
            get => _adRemCbsRet.Arredondar(4);
            set => _adRemCbsRet = value.Arredondar(4);
        }

        // UB98a
        [XmlElement(Order = 5)]
        public decimal vCBSMonoRet
        {
            get => _vCbsMonoRet.Arredondar(2);
            set => _vCbsMonoRet = value.Arredondar(2);
        }
    }
}