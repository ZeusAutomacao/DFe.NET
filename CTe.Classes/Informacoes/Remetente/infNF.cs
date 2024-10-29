namespace CTe.Classes.Informacoes.Remetente
{
    public class infNF
    {
        public locRet locRet;

        private string _nRoma;
        private string _nPed;
        private int _mod;
        private string _serie;
        private string _nDoc;
        private string _dEmi;
        private double _vBC;
        private double _vICMS;
        private double _vBCST;
        private double _vST;
        private double _vProd;
        private double _vNF;
        private int _nCFOP;
        private double _nPeso;
        private string _PIN;

        public string nRoma { get { return _nRoma; } set { _nRoma = value; } }
        public string nPed { get { return _nPed; } set { _nPed = value; } }
        public int mod { get { return _mod; } set { _mod = value; } }
        public string serie { get { return _serie; } set { _serie = value; } }
        public string nDoc { get { return _nDoc; } set { _nDoc = value; } }
        public string dEmi { get { return _dEmi; } set { _dEmi = value; } }
        public double vBC { get { return _vBC; } set { _vBC = value; } }
        public double vICMS { get { return _vICMS; } set { _vICMS = value; } }
        public double vBCST { get { return _vBCST; } set { _vBCST = value; } }
        public double vST { get { return _vST; } set { _vST = value; } }
        public double vProd { get { return _vProd; } set { _vProd = value; } }
        public double vNF { get { return _vNF; } set { _vNF = value; } }
        public int nCFOP { get { return _nCFOP; } set { _nCFOP = value; } }
        public double nPeso { get { return _nPeso; } set { _nPeso = value; } }
        public string PIN { get { return _PIN; } set { _PIN = value; } }
    }
}