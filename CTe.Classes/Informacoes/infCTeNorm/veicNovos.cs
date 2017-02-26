using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class veicNovos
    {
        public string chassi { get; set; }

        public string cCor { get; set; }

        public string xCor { get; set; }

        public int cMod { get; set; }

        public decimal vUnit { get; set; }

        public decimal vFrete { get; set; }
    }
}
