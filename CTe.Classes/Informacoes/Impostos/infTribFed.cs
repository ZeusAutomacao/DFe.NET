using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class infTribFed
    {
        private decimal? _vPis;
        private decimal? _vCofins;
        private decimal? _vIr;
        private decimal? _vInss;
        private decimal? _vCsll;

        public decimal? vPIS
        {
            get { return _vPis.Arredondar(2); }
            set { _vPis = value.Arredondar(2); }
        }

        public decimal? vCOFINS
        {
            get { return _vCofins.Arredondar(2); }
            set { _vCofins = value.Arredondar(2); }
        }

        public decimal? vIR
        {
            get { return _vIr.Arredondar(2); }
            set { _vIr = value.Arredondar(2); }
        }

        public decimal? vINSS
        {
            get { return _vInss.Arredondar(2); }
            set { _vInss = value.Arredondar(2); }
        }

        public decimal? vCSLL
        {
            get { return _vCsll.Arredondar(2); }
            set { _vCsll = value.Arredondar(2); }
        }


        public bool vPISSpecified => vPIS.HasValue;
        public bool vCOFINSSpecified => vCOFINS.HasValue;
        public bool vIRSpecified => vIR.HasValue;
        public bool vINSSSpecified => vINSS.HasValue;
        public bool vCSLLSpecified => vCSLL.HasValue;
    }
}