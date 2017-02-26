using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class idDocAntPap
    {
        public tpDocAnterior tpDoc { get; set; }

        public short serie { get; set; }

        public short? subser { get; set; }
        public bool subserSpecified => subser.HasValue;

        public string nDoc { get; set; }

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxydEmi
        {
            get { return dEmi.ParaDataString();}
            set { dEmi = Convert.ToDateTime(value); }
        }
    }
}
