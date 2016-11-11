using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeVeicTracao
    {
        [XmlElement(ElementName = "cInt")]
        public string CInt { get; set; }

        [XmlElement(ElementName = "placa")]
        public string Placa { get; set; }

        [XmlElement(ElementName = "RENAVAM")]
        public string RENAVAM { get; set; }

        [XmlElement(ElementName = "tara")]
        public int? Tara { get; set; }

        [XmlElement(ElementName = "capKG")]
        public int? CapKG { get; set; }

        [XmlElement(ElementName = "capM3")]
        public int? CapM3 { get; set; }

        [XmlElement(ElementName = "prop")]
        public MDFeProp Prop { get; set; }

        [XmlElement(ElementName = "condutor")]
        public MDFeCondutor Condutor { get; set; }

        [XmlElement(ElementName = "tpRod")]
        public MDFeTpRod TpRod { get; set; }

        [XmlElement(ElementName = "tpCar")]
        public MDFeTpCar TpCar { get; set; }

        [XmlIgnore]
        public EstadoUF UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get
            {
                return UF.GetSiglaUfString();
            }
            set { UF = UF.SiglaParaEstado(value); }
        }
    }
}