using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionRemetenteCTe
    {
        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string Cpf { get; set; }

        [XmlElement(ElementName = "IE")]
        public string Ie { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "xFant")]
        public string NomeFantasia { get; set; }

        [XmlElement(ElementName = "fone")]
        public string Telefone { get; set; }

        [XmlElement(ElementName = "enderReme")]
        public FusionEnderecoTomadorCTe Endereco { get; set; }

        public FusionRemetenteCTe()
        {
            Endereco = new FusionEnderecoTomadorCTe();
        }
    }
}