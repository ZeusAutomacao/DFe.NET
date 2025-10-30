using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infDocumentos
{
    public class infNF
    {
        [XmlElement(Order = 1)]
        public string nRoma { get; set; }
        [XmlElement(Order = 2)]
        public string nPed { get; set; }
        [XmlElement(Order = 3)]
        public mod mod { get; set; }
        [XmlElement(Order = 4)]
        public string serie { get; set; }
        [XmlElement(Order = 5)]
        public string nDoc { get; set; }

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        /// <summary>
        /// Proxy para dEmi no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dEmi", Order = 6)]
        public string ProxyddEmi
        {
            get
            {
                return dEmi.ParaDataString();
            }
            set { dEmi = DateTime.Parse(value); }

        }

        [XmlElement(Order = 7)]
        public decimal vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }
        [XmlElement(Order = 8)]
        public decimal vICMS
        {
            get { return _vIcms.Arredondar(2); }
            set { _vIcms = value.Arredondar(2); }
        }
        [XmlElement(Order = 9)]
        public decimal vBCST
        {
            get { return _vBcst.Arredondar(2); }
            set { _vBcst = value.Arredondar(2); }
        }
        [XmlElement(Order = 10)]
        public decimal vST
        {
            get { return _vSt.Arredondar(2); }
            set { _vSt = value.Arredondar(2); }
        }
        [XmlElement(Order = 11)]
        public decimal vProd
        {
            get { return _vProd.Arredondar(2); }
            set { _vProd = value.Arredondar(2); }
        }
        [XmlElement(Order = 12)]
        public decimal vNF
        {
            get { return _vNf.Arredondar(2); }
            set { _vNf = value.Arredondar(2); }
        }
        [XmlElement(Order = 13)]
        public int nCFOP { get; set; }
        [XmlElement(Order = 14)]
        public decimal? nPeso
        {
            get { return _nPeso.Arredondar(3); }
            set { _nPeso = value.Arredondar(3); }
        }
        [XmlElement(Order = 15)]
        public bool nPesoSpecified { get { return _nPeso.HasValue; } }
        [XmlElement(Order = 16)]
        public string PIN { get; set; }

        [XmlIgnore]
        public DateTime? dPrev { get; set; }

        /// <summary>
        /// Proxy para dPrev no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dPrev", Order = 17)]
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

        [XmlElement("infUnidTransp", Order = 18)]
        public List<infUnidTransp> infUnidTransp;

        [XmlElement("infUnidCarga", Order = 19)]
        public List<infUnidCarga> infUnidCarga;

        private decimal _vBc;
        private decimal _vIcms;
        private decimal _vBcst;
        private decimal _vSt;
        private decimal _vProd;
        private decimal _vNf;
        private decimal? _nPeso;
    }
}