using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infDocumentos
{
    public class infNFe
    {
        [XmlElement(Order = 1)]
        public string chave { get; set; }

        [XmlElement(Order = 2)]
        public string PIN { get; set; }

        [XmlIgnore]
        public DateTime? dPrev { get; set; }

        /// <summary>
        /// Proxy para dPrev no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dPrev", Order = 3)]
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

        
        [XmlElement("infUnidTransp", Order = 5)]
        public List<infUnidTransp> infUnidTransp;

        [XmlElement("infUnidCarga", Order = 4)]
        public List<infUnidCarga> infUnidCarga;
    }
}