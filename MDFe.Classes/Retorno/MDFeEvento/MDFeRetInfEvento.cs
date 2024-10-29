using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;

namespace MDFe.Classes.Retorno.MDFeEvento
{
    [Serializable]
    public class MDFeRetInfEvento
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VerAplic { get; set; }

        [XmlIgnore]
        public Estado COrgao { get; set; }

        [XmlElement(ElementName = "cOrgao")]
        public string COrgaoProxy
        {
            get
            {
                return COrgao.GetCodigoIbgeEmString();
            }
            set { COrgao = COrgao.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "cStat")]
        public short CStat { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string XMotivo { get; set; }

        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }

        [XmlElement(ElementName = "tpEvento")]
        public string TpEvento { get; set; }

        [XmlElement(ElementName = "xEvento")]
        public string XEvento { get; set; }

        [XmlElement(ElementName = "nSeqEvento")]
        public byte NSeqEvento { get; set; }

        [XmlElement(ElementName = "dhRegEvento")]
        public DateTime? DhRegEvento { get; set; }

        [XmlElement(ElementName = "nProt")]
        public string NProt { get; set; }
    }
}