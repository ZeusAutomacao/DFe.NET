using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace CTe.Classes.Informacoes.infCTeNormal.docAnteriores
{
    public class emiDocAnt
    {
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public string IE { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string UFproxy
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        public string xNome { get; set; }

        [XmlElement(ElementName = "idDocAnt")]
        public List<idDocAnt> idDocAnt { get; set; }
    }
}