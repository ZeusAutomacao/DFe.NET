using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal
{
    public class veicOs
    {
        public string placa { get; set; }
        public string RENAVAM { get; set; }

        public prop prop { get; set; }

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