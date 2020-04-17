using System;
using System.Xml.Serialization;
using DFe.Classes;
using DFe.Utils;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infPrazo
    {
        [XmlIgnore]
        public short nParcela { get; set; }

        [XmlElement("nParcela")]
        public string nParcelaProxy
        {
            get { return nParcela.ToString("D3"); }
            set { nParcela = short.Parse(value); }
        }

        [XmlIgnore]
        public DateTime dVenc { get; set; }

        [XmlElement("dVenc")]
        public string dVencProxy
        {
            get { return dVenc.ParaDataString(); }
            set { dVenc = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public decimal vParcela { get; set; }

        [XmlElement("vParcela")]
        public decimal vParcelaProxy
        {
            get { return vParcela.Arredondar(2); }
            set { vParcela = value.Arredondar(2); }
        }
    }
}