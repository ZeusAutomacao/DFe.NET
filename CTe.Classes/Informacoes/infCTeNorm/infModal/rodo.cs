using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class rodo : ContainerModal
    {
        public string RNTRC { get; set; }

        [XmlIgnore]
        public DateTime? dPrev { get; set; }

        [XmlElement(ElementName = "dPrev")]
        public string ProxydPrev {
            get
            {

                if (dPrev == null) return null;

                return dPrev.Value.ParaDataString();
            }
            set
            {
                dPrev = Convert.ToDateTime(value);
            }
        }

        public lota? lota { get; set; }
        public bool lotaSpecified => lota.HasValue;

        public string CIOT { get; set; }

        [XmlElement("occ")]
        public List<occ> occ;

        [XmlElement(ElementName = "valePed")]
        public List<valePed> valePed { get; set; }

        public List<veic> veic { get; set; }

        [XmlElement(ElementName = "lacRodo")]
        public List<lacRodo> lacRodo { get; set; }

        public List<moto> moto { get; set; }
    }
}