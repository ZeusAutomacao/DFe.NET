using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviario;
using CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS;
using CTe.Classes.Informacoes.Tipos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
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
        public bool lotaSpecified { get { return lota.HasValue; } }

        public string CIOT { get; set; }

        [XmlElement("occ")]
        public List<occ> occ;

        [XmlElement(ElementName = "valePed")]
        public List<valePed> valePed { get; set; }

        [XmlElement(ElementName = "veic")]
        public List<veic> veic { get; set; }

        [XmlElement(ElementName = "lacRodo")]
        public List<lacRodo> lacRodo { get; set; }

        [XmlElement(ElementName = "moto")]
        public List<moto> moto { get; set; }

        public bool ShouldSerializeveic()
        {
            return veic != null;
        }

        public bool ShouldSerializemoto()
        {
            return moto != null;
        }
    }
}