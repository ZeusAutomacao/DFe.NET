using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionRecebedorCTe
    {
        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string Cpf { get; set; }

        [XmlElement(ElementName = "IE")]
        public string Ie { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "fone")]
        public string Telefone { get; set; }

        [XmlElement(ElementName = "enderReceb")]
        public FusionEnderecoTomadorCTe Endereco { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        public FusionRecebedorCTe()
        {
            Endereco = new FusionEnderecoTomadorCTe();
        }
    }
}