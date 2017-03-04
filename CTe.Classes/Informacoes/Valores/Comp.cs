using System;
using System.Xml.Serialization;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Valores
{
    public class Comp
    {
        private decimal _vComp;
        public string xNome { get; set; }

        public decimal vComp
        {
            get { return _vComp.Arredondar(2); }
            set { _vComp = value.Arredondar(2); }
        }
    }
}
