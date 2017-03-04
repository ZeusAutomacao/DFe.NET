using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class refNF
    {
        private decimal _valor;
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public mod mod { get; set; }

        public short serie { get; set; }

        public short? subserie { get; set; }

        public bool subserieSpecified => subserie.HasValue;

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
