using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infOutros
    {
        public tpDoc tpDoc { get; set; }
        public string descOutros { get; set; }
        public string nDoc { get; set; }

        [XmlIgnore]
        public DateTime? dEmi { get; set; }

        /// <summary>
        /// Proxy para dPrev no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dPrev")]
        public string ProxyddEmi
        {
            get
            {
                if (dEmi != null)
                {
                    return dEmi.Value.ParaDataString();
                }
                return null;
            }
            set { dEmi = DateTime.Parse(value); }
        }



        public decimal? vDocFisc { get; set; }
        public bool vDocFiscSpecified => vDocFisc.HasValue;

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
