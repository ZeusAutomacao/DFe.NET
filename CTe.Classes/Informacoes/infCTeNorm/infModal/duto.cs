using System;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class duto
    {
        public decimal? vTar { get; set; }
        public bool vTarSpecified => vTar.HasValue;

        [XmlIgnore]
        public DateTime dIni { get; set; }

        [XmlElement(ElementName = "dIni")]
        public string ProxydIni
        {
            get { return dIni.ParaDataString(); }
            set { dIni = Convert.ToDateTime(value); }
        }

        [XmlIgnore]
        public DateTime dFim { get; set; }

        [XmlElement(ElementName = "dFim")]
        public string ProxydFim
        {
            get { return dFim.ParaDataString(); }
            set { dFim = Convert.ToDateTime(value); }
        }
    }
}