using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.Complemento
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
