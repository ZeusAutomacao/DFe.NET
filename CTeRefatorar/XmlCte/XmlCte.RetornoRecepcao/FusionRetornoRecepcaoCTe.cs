using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.RetornoRecepcao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte", ElementName = "consReciCTe")]
    public class FusionRetornoRecepcaoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; } = "2.00";

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "nRec")]
        public string NumeroRecibo { get; set; }
    }
}