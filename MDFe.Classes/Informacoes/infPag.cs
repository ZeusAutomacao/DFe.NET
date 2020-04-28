using DFe.Classes;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infPag
    {
        public string xNome { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string idEstrangeiro { get; set; }

        [XmlElement(ElementName = "Comp")]
        public List<Comp> Comp { get; set; }

        [XmlIgnore]
        public decimal vContrato { get; set; }

        [XmlElement("vContrato")]
        public decimal vContratoProxy
        {
            get { return vContrato.Arredondar(2); }
            set { vContrato = value.Arredondar(2); }
        }

        public indPag indPag { get; set; }

        [XmlElement(ElementName = "infPrazo")]
        public List<infPrazo> infPrazo { get; set; }

        public infBanc infBanc { get; set; }
    }
}