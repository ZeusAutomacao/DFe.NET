using System;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Complemento.Tipos;
using CTe.Classes.Informacoes.Tipos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.Complemento
{
    public class comHora : comHoraBase
    {
        public tpHor tpHor { get; set; }

        [XmlIgnore]
        public TimeSpan hProg { get; set; }

        [XmlElement(ElementName = "hProg")]
        public string ProxyhProg
        {
            get
            {
                return hProg.ParaHoraString();

            }
            set { hProg = TimeSpan.Parse(value); }
        }
    }
}