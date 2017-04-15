using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionTomadorOutrosCTe
    {
        [XmlElement(ElementName = "toma")]
        public FusionTipoTomadorCTe TipoTomador { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string Cpf { get; set; }

        [XmlElement(ElementName = "IE")]
        public string IE { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "xFant")]
        public string Fantasia { get; set; }

        [XmlElement(ElementName = "fone")]
        public string Telefone { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "enderToma")]
        public FusionEnderecoTomadorCTe EnderecoTomador { get; set; }

        public FusionTomadorOutrosCTe()
        {
            EnderecoTomador = new FusionEnderecoTomadorCTe();
            TipoTomador = FusionTipoTomadorCTe.Outro;
        }
    }
}