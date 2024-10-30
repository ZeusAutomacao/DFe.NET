using System;
using System.Xml.Serialization;
using DFe.Classes;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infCteSubs
{
    public class refNF
    {
        private decimal _valor;
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        /// <summary>
        /// Modelos:  01, 1B, 02, 2D, 2E, 04, 06, 07, 08, 8B, 09, 10, 11, 13, 14, 15, 16, 17, 18, 20, 21, 22, 23, 24, 25, 26, 27, 28, 55
        /// </summary>
        public string mod { get; set; }

        public short serie { get; set; }

        public short? subserie { get; set; }

        public bool subserieSpecified { get { return subserie.HasValue; } }

        public int nro { get; set; }

        public decimal valor
        {
            get { return _valor.Arredondar(2); }
            set { _valor = value.Arredondar(2); }
        }

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxydEmi {
            get { return dEmi.ParaDataString(); }
            set { dEmi = Convert.ToDateTime(value); } }
    }
}