using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class compl
    {
        public fluxo fluxo;
        public Entrega Entrega;
        public ObsCont ObsCont;
        public ObsFisco ObsFisco;

        private string _xCaracAd;
        private string _xCaracSer;
        private string _xEmi;
        private string _origCalc;
        private string _destCalc;
        private string _xObs;

        public string xCaracAd { get { return _xCaracAd; } set { _xCaracAd = value; } }
        public string xCaracSer { get { return _xCaracSer; } set { _xCaracSer = value; } }
        public string xEmi { get { return _xEmi; } set { _xEmi = value; } }
        public string origCalc { get { return _origCalc; } set { _origCalc = value; } }
        public string destCalc { get { return _destCalc; } set { _destCalc = value; } }
        public string xObs { get { return _xObs; } set { _xObs = value; } }
    }
}
