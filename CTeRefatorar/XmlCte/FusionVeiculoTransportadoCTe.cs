using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionVeiculoTransportadoCTe
    {
        [XmlElement(ElementName = "chassi")]
        public string Chassi { get; set; }

        [XmlElement(ElementName = "cCor")]
        public string CodigoCor { get; set; }

        [XmlElement(ElementName = "xCor")]
        public string DescricaoCor { get; set; }

        [XmlElement(ElementName = "cMod")]
        public string CodigoMarcaModelo { get; set; }

        [XmlElement(ElementName = "vUnit")]
        public decimal ValorUnitario { get; set; }

        [XmlElement(ElementName = "vFrete")]
        public decimal FreteUnitario { get; set; }
    }
}