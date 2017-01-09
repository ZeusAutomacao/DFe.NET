using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionEmitenteCTe
    {
        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "IE")]
        public string Ie { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string RazaoSocialOuNome { get; set; }

        [XmlElement(ElementName = "xFant")]
        public string NomeFantasia { get; set; }

        [XmlElement(ElementName = "enderEmit")]
        public FusionEnderecoCTe Endereco { get; set; }

        public FusionEmitenteCTe()
        {
            Endereco = new FusionEnderecoCTe();
        }
    }
}