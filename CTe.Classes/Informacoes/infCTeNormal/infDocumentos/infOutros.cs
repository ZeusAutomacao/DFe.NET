using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infDocumentos
{
    public class infOutros
    {
        [XmlElement(Order = 1)]
        public tpDoc tpDoc { get; set; }
        [XmlElement(Order = 2)]
        public string descOutros { get; set; }
        [XmlElement(Order = 3)]
        public string nDoc { get; set; }

        [XmlIgnore]
        public DateTime? dEmi { get; set; }

        /// <summary>
        /// Proxy para dPrev no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dEmi", Order = 4)]
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

        [XmlElement(Order = 5)]
        public decimal? vDocFisc
        {
            get { return _vDocFisc.Arredondar(2); }
            set { _vDocFisc = value.Arredondar(2); }
        }

        public bool vDocFiscSpecified { get { return vDocFisc.HasValue; } }

        [XmlIgnore]
        public DateTime? dPrev { get; set; }

        /// <summary>
        /// Proxy para dPrev no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dPrev", Order = 6)]
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

        [XmlElement("infUnidTransp", Order = 7)]
        public List<infUnidTransp> infUnidTransp;

        [XmlElement("infUnidCarga", Order = 8)]
        public List<infUnidCarga> infUnidCarga;

        private decimal? _vDocFisc;
    }
}