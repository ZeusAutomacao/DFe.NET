using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class noInter : comHoraBase
    {
        public tpHor tpHor { get; set; } = tpHor.NoIntervaloDeTempo;

        [XmlIgnore]
        public TimeSpan hIni { get; set; }

        [XmlElement(ElementName = "hIni")]
        public string ProxyhIni
        {
            get
            {
                return hIni.ParaHoraString();

            }
            set { hIni = TimeSpan.Parse(value); }
        }


        [XmlIgnore]
        public TimeSpan hFim { get; set; }


        [XmlElement(ElementName = "hFim")]
        public string ProxyhFim
        {
            get
            {
                return hFim.ParaHoraString();

            }
            set { hFim = TimeSpan.Parse(value); }
        }
    }
}
