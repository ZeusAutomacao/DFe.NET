using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSCBSMono
    {
        private decimal _vTotIbsMonoItem;
        private decimal _vTotCbsMonoItem;

        // UB84a
        [XmlElement(Order = 1)]
        public gMonoPadrao gMonoPadrao { get; set; }

        // UB90
        [XmlElement(Order = 2)]
        public gMonoReten gMonoReten { get; set; }

        // UB94
        [XmlElement(Order = 3)]
        public gMonoRet gMonoRet { get; set; }

        // UB99
        [XmlElement(Order = 4)]
        public gMonoDif gMonoDif { get; set; }

        // UB104
        [XmlElement(Order = 5)]
        public decimal vTotIBSMonoItem
        {
            get => _vTotIbsMonoItem.Arredondar(2);
            set => _vTotIbsMonoItem = value.Arredondar(2);
        }

        // UB105
        [XmlElement(Order = 6)]
        public decimal vTotCBSMonoItem
        {
            get => _vTotCbsMonoItem.Arredondar(2);
            set => _vTotCbsMonoItem = value.Arredondar(2);
        }
    }
}