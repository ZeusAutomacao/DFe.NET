using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infNFe
    {
        public string chave { get; set; }
        public string PIN { get; set; }

        [XmlIgnore]
        public DateTime? dPrev { get; set; }

        /// <summary>
        /// Proxy para dPrev no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dPrev")]
        public string ProxyddPrev
        {
            get
            {
                if (dPrev != null)
                {
                    return dPrev.Value.ParaDataString();
                }
                return null;
            }
            set { dPrev = DateTime.Parse(value); }
        }

        [XmlElement("infUnidTransp")]
        public List<infUnidTransp> infUnidTransp;

        [XmlElement("infUnidCarga")]
        public List<infUnidCarga> infUnidCarga;
    }
}
