using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionEnderecoTomadorCTe
    {
        [XmlElement(ElementName = "xLgr")]
        public string Logradouro { get; set; }

        [XmlElement(ElementName = "nro")]
        public string Numero { get; set; }

        [XmlElement(ElementName = "xCpl")]
        public string Complemento { get; set; }

        [XmlElement(ElementName = "xBairro")]
        public string Bairro { get; set; }

        [XmlElement(ElementName = "cMun")]
        public long CodigoIbgeMunicipio { get; set; }

        [XmlElement(ElementName = "xMun")]
        public string NomeMunicipio { get; set; }

        [XmlElement(ElementName = "CEP")]
        public string Cep { get; set; }

        [XmlElement(ElementName = "UF")]
        public string SiglaUf { get; set; }

        [XmlElement(ElementName = "cPais")]
        public string CodigoPais { get; set; }

        [XmlElement(ElementName = "xPais")]
        public string NomePais { get; set; }
    }
}