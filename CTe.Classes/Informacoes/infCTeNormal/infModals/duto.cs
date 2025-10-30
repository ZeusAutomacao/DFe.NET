using System;
using System.Xml.Serialization;
using DFe.Classes;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
{
    public class duto : ContainerModal
    {
        private decimal? _vTar;

        public decimal? vTar
        {
            get { return _vTar.Arredondar(6); }
            set { _vTar = value.Arredondar(6); }
        }

        public bool vTarSpecified { get { return vTar.HasValue; } }

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