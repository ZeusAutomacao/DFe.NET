using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Utils;

namespace MDFe.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    [XmlRoot(ElementName = "evEncMDFe")]
    public class MDFeEvEncMDFe : MDFeEventoContainer
    {
        public MDFeEvEncMDFe()
        {
            DescEvento = "Encerramento";
        }

        [XmlElement(ElementName = "descEvento")]
        public string DescEvento { get; set; }

        [XmlElement(ElementName = "nProt")]
        public string NProt { get; set; }

        [XmlIgnore]
        public DateTime DtEnc { get; set; }

        [XmlElement(ElementName = "dtEnc")]
        public string ProxyDhEmi
        {
            get { return DtEnc.ParaDataString(); }
            set { DtEnc = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public Estado CUF { get; set; }

        [XmlElement(ElementName = "cUF")]
        public string CUFProxy
        {
            get
            {
                return CUF.GetCodigoIbgeEmString();
            }
            set { CUF = CUF.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "cMun")]
        public long CMun { get; set; }
    }
}