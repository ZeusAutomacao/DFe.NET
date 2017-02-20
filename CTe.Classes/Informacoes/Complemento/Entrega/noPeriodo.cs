using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class noPeriodo : comDataBase
    {
        public tpPer tpPer { get; set; } = tpPer.NoPeriodo;

        [XmlIgnore]
        public DateTime dIni { get; set; }

        [XmlElement(ElementName = "dIni")]
        public string ProxydIni
        {
            get
            {
                return dIni.ParaDataString();

            }
            set { dIni = DateTime.Parse(value); }
        }


        [XmlIgnore]
        public DateTime dFim { get; set; }

        [XmlElement(ElementName = "dFim")]
        public string ProxydFim
        {
            get
            {
                return dFim.ParaDataString();

            }
            set { dFim = DateTime.Parse(value); }
        }
    }
}
