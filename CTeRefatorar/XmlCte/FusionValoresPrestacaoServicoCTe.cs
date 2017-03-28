using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionValoresPrestacaoServicoCTe
    {
        [XmlElement(ElementName = "vTPrest")]
        public decimal ValorTotal { get; set; }

        [XmlElement(ElementName = "vRec")]
        public decimal ValorAReceber { get; set; }
    }
}