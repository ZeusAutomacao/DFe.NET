using System;
using System.Xml.Serialization;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class veicNovos
    {
        private decimal _vUnit;
        public string chassi { get; set; }

        public string cCor { get; set; }

        public string xCor { get; set; }

        public int cMod { get; set; }

        public decimal vUnit
        {
            get { return _vUnit.Arredondar(2); }
            set { _vUnit = value.Arredondar(2); }
        }

        public decimal vFrete { get; set; }
    }
}
