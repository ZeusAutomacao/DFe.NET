using System;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Complemento.Tipos;
using CTe.Classes.Informacoes.Tipos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.Complemento
{
    public class noPeriodo : comDataBase
    {
        public noPeriodo()
        {
            tpPer = tpPer.NoPeriodo;
        }

        public tpPer tpPer { get; set; } 

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