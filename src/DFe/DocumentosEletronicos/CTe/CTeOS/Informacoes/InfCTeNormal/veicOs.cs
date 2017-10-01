using System.Xml.Serialization;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS;
using DFe.DocumentosEletronicos.Entidades;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.InfCTeNormal
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