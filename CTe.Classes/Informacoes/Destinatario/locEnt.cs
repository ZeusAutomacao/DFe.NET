using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace CTeDLL.Classes.Informacoes.Destinatario
{
    public class locEnt
    {
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public string xNome { get; set; }

        public string xLgr { get; set; }

        public string nro { get; set; }

        public string xCpl { get; set; }

        public string xBairro { get; set; }

        public long cMun { get; set; }

        public string xMun { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }
    }
}
