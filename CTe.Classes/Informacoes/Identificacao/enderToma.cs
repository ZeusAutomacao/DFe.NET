using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace CTeDLL.Classes.Informacoes.Identificacao
{
    public class enderToma
    {
        public string xLgr { get; set; }

        public string nro { get; set; }

        public string xCpl { get; set; }

        public string xBairro { get; set; }

        public string cMun { get; set; }

        public string xMun { get; set; }

        public string CEP { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF { get { return UF.GetSiglaUfString(); } set { UF = UF.SiglaParaEstado(value); } }

        public int? cPais { get; set; }

        public bool cPaisSpecified { get { return cPais.HasValue; } }

        public string xPais { get; set; }
    }
}
