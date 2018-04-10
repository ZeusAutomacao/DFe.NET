using System;
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Informacoes.Detalhe
{
    public class rastro
    {
        private decimal _qLote;
        public string nLote { get; set; }

        public decimal qLote
        {
            get { return _qLote.Arredondar(3); }
            set { _qLote = value.Arredondar(3); }
        }

        [XmlIgnore]
        public DateTime dFab { get; set; }

        [XmlElement("dFab")]
        public string ProxydFab
        {
            get { return dFab.ParaDataString(); }
            set { dFab = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public DateTime dVal { get; set; }

        [XmlElement("dVal")]
        public string ProxydVal
        {
            get { return dVal.ParaDataString(); }
            set { dVal = DateTime.Parse(value); }
        }

        public string cAgreg { get; set; }
    }
}