using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class veicNovos
    {
        private string _chassi;
        private string _cCor;
        private string _xCor;
        private int _cMod;
        private double _vUnit;
        private double _vFrete;

        public string chassi { get { return _chassi; } set { _chassi = value; } }
        public string cCor { get { return _cCor; } set { _cCor = value; } }
        public string xCor { get { return _xCor; } set { _xCor = value; } }
        public int cMod { get { return _cMod; } set { _cMod = value; } }
        public double vUnit { get { return _vUnit; } set { _vUnit = value; } }
        public double vFrete { get { return _vFrete; } set { _vFrete = value; } }
    }
}
