using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infNF
    {
        public string nRoma { get; set; }
        public string nPed { get; set; }
        public mod mod { get; set; }
        public short serie { get; set; }
        public string nDoc { get; set; }

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        /// <summary>
        /// Proxy para dEmi no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dEmi")]
        public string ProxyddEmi
        {
            get
            {
                    return dEmi.ParaDataString();
            }
            set { dEmi = DateTime.Parse(value); }
        }


        public decimal vBC { get; set; }
        public decimal vICMS { get; set; }
        public decimal vBCST { get; set; }
        public decimal vST { get; set; }
        public decimal vProd { get; set; }
        public decimal vNF { get; set; }
        public int nCFOP { get; set; }
        public decimal nPeso { get; set; }
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
