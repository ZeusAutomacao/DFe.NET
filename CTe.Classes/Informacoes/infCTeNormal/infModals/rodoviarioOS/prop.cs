using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS
{
    public class prop
    {
        public string CPF { get; set; }

        public string CNPJ { get; set; }
        public string RNTRC { get; set; }

        public string TAF { get; set; }

        public string NroRegEstadual { get; set; }

        public string xNome { get; set; }

        public string IE { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }


        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        public tpPropProp tpProp { get; set; }
    }
}