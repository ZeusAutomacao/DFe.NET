using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infContratante
    {
        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }
        
        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }
        
        [XmlElement(ElementName = "idEstrangeiro")]
        public string IdEstrangeiro { get; set; }
    }
}