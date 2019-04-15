using System;
using System.Xml.Serialization;
using CTe.Classes.Servicos.Tipos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS
{
    public class infFretamento
    {
        public tpFretamento tpFretamento { get; set; }

        [XmlIgnore]
        public DateTime? dhViagem { get; set; }

        [XmlElement(ElementName = "dhViagem")]
        public string ProxydhEmi
        {
            get
            {
                return dhViagem.ParaDataHoraStringUtc();
            }
            set
            {
                if (value == null)
                {
                    dhViagem = null;
                    return;
                }
                dhViagem = DateTime.Parse(value);
            }
        }
    }
}