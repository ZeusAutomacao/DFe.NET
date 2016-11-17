using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento
{
    [Serializable]
    public class MDFeInfEvento
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlIgnore]
        public EstadoUF COrgao { get; set; }

        [XmlElement(ElementName = "cOrgao")]
        public string COrgaoProxy
        {
            get
            {
                return COrgao.GetCodigoIbgeEmString();
            }
            set { COrgao = COrgao.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }

        [XmlIgnore]
        public DateTime DhEvento { get; set; }

        [XmlElement(ElementName = "dhEvento")]
        public string ProxyDhEvento
        {
            get { return DhEvento.ToString("yyyy-MM-ddTHH:mm:dd"); }
            set { DhEvento = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "tpEvento")]
        public MDFeTipoEvento TpEvento { get; set; }

        [XmlElement(ElementName = "nSeqEvento")]
        public byte NSeqEvento { get; set; }

        [XmlElement(ElementName = "detEvento")]
        public MDFeDetEvento DetEvento { get; set; }
    }
}