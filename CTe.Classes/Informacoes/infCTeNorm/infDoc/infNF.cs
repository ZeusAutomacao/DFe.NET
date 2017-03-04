using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;
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


        public decimal vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        public decimal vICMS
        {
            get { return _vIcms.Arredondar(2); }
            set { _vIcms = value.Arredondar(2); }
        }

        public decimal vBCST
        {
            get { return _vBcst.Arredondar(2); }
            set { _vBcst = value.Arredondar(2); }
        }

        public decimal vST
        {
            get { return _vSt.Arredondar(2); }
            set { _vSt = value.Arredondar(2); }
        }

        public decimal vProd
        {
            get { return _vProd.Arredondar(2); }
            set { _vProd = value.Arredondar(2); }
        }

        public decimal vNF
        {
            get { return _vNf.Arredondar(2); }
            set { _vNf = value.Arredondar(2); }
        }

        public int nCFOP { get; set; }

        public decimal nPeso
        {
            get { return _nPeso.Arredondar(3); }
            set { _nPeso = value.Arredondar(3); }
        }

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

        private decimal _vBc;
        private decimal _vIcms;
        private decimal _vBcst;
        private decimal _vSt;
        private decimal _vProd;
        private decimal _vNf;
        private decimal _nPeso;
    }
}
