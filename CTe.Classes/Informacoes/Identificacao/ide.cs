using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTeDLL.Classes.Informacoes.Identificacao
{
    public class ide
    {
        public toma03 toma03;
        public toma4 toma4;

        private int _cUF;
        private string _cCT;
        private int _CFOP;
        private string _natOp;
        private int _forPag;
        private int _mod;
        private int _serie;
        private int _nCT;
        private string _dhEmi;
        private int _tpImp;
        private int _tpEmis;
        private int _cDV;
        private int _tpAmb;
        private int _tpCTe;
        private int _procEmi;
        private string _verProc;
        private string _refCTE;
        private string _cMunEnv;
        private string _xMunEnv;
        private string _UFEnv;
        private int _modal;
        private int _tpServ;
        private string _cMunIni;
        private string _xMunIni;
        private string _UFIni;
        private string _cMunFim;
        private string _xMunFim;
        private string _UFFim;
        private int _retira;
        private string _xDetRetira;

        /// <summary>
        ///     B02 - Código da UF do emitente do Documento Fiscal. Utilizar a Tabela do IBGE.
        /// </summary>
        public Estado cUF { get; set; }

        public string cCT { get { return _cCT; } set { _cCT = value; } }
        public int CFOP { get { return _CFOP; } set { _CFOP = value; } }
        public string natOp { get { return _natOp; } set { _natOp = value; } }
        public int forPag { get { return _forPag; } set { _forPag = value; } }
        
        /// <summary>
        ///     B06 - Modelo do Documento Fiscal
        /// </summary>
        public ModeloDocumento mod { get; set; }

        public int serie { get { return _serie; } set { _serie = value; } }
        public int nCT { get { return _nCT; } set { _nCT = value; } }
        public string dhEmi { get { return _dhEmi; } set { _dhEmi = value; } }
        public int tpImp { get { return _tpImp; } set { _tpImp = value; } }
        public int tpEmis { get { return _tpEmis; } set { _tpEmis = value; } }
        public int cDV { get { return _cDV; } set { _cDV = value; } }

        /// <summary>
        ///     B24 - Identificação do Ambiente
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     B11 - Tipo do Documento Fiscal
        /// </summary>
        public TipoCTe tpCTe { get; set; }

        public int procEmi { get { return _procEmi; } set { _procEmi = value; } }
        public string verProc { get { return _verProc; } set { _verProc = value; } }
        public string refCTE { get { return _refCTE; } set { _refCTE = value; } }
        public string cMunEnv { get { return _cMunEnv; } set { _cMunEnv = value; } }
        public string xMunEnv { get { return _xMunEnv; } set { _xMunEnv = value; } }
        public string UFEnv { get { return _UFEnv; } set { _UFEnv = value; } }
        public int modal { get { return _modal; } set { _modal = value; } }
        public int tpServ { get { return _tpServ; } set { _tpServ = value; } }
        public string cMunIni { get { return _cMunIni; } set { _cMunIni = value; } }
        public string xMunIni { get { return _xMunIni; } set { _xMunIni = value; } }
        public string UFIni { get { return _UFIni; } set { _UFIni = value; } }
        public string cMunFim { get { return _cMunFim; } set { _cMunFim = value; } }
        public string xMunFim { get { return _xMunFim; } set { _xMunFim = value; } }
        public string UFFim { get { return _UFFim; } set { _UFFim = value; } }
        public int retira { get { return _retira; } set { _retira = value; } }
        public string xDetRetira { get { return _xDetRetira; } set { _xDetRetira = value; } }
    }
}
