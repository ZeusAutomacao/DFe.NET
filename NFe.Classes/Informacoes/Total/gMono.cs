using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Total
{
    public class gMono
    {
        private decimal _vIBSMono;
        private decimal _vCBSMono;
        private decimal _vIBSMonoReten;
        private decimal _vCBSMonoReten;
        private decimal _vIBSMonoRet;
        private decimal _vCBSMonoRet;

        // W58
        [XmlElement(Order = 1)]
        public decimal vIBSMono
        {
            get => _vIBSMono;
            set => _vIBSMono = value.Arredondar(2);
        }

        // W59
        [XmlElement(Order = 2)]
        public decimal vCBSMono
        {
            get => _vCBSMono;
            set => _vCBSMono = value.Arredondar(2);
        }

        // W59a
        [XmlElement(Order = 3)]
        public decimal vIBSMonoReten
        {
            get => _vIBSMonoReten;
            set => _vIBSMonoReten = value.Arredondar(2);
        }

        // W59b
        [XmlElement(Order = 4)]
        public decimal vCBSMonoReten
        {
            get => _vCBSMonoReten;
            set => _vCBSMonoReten = value.Arredondar(2);
        }

        // W59c
        [XmlElement(Order = 5)]
        public decimal vIBSMonoRet
        {
            get => _vIBSMonoRet;
            set => _vIBSMonoRet = value.Arredondar(2);
        }

        // W59d
        [XmlElement(Order = 6)]
        public decimal vCBSMonoRet
        {
            get => _vCBSMonoRet;
            set => _vCBSMonoRet = value.Arredondar(2);
        }
    }
}