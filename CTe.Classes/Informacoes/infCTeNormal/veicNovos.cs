using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class veicNovos
    {
        private decimal _vUnit;
        private decimal _vFrete;
        public string chassi { get; set; }

        public string cCor { get; set; }

        public string xCor { get; set; }

        public string cMod { get; set; }

        public decimal vUnit
        {
            get { return _vUnit.Arredondar(2); }
            set { _vUnit = value.Arredondar(2); }
        }

        public decimal vFrete
        {
            get { return _vFrete.Arredondar(2); }
            set { _vFrete = value.Arredondar(2); }
        }
    }
}