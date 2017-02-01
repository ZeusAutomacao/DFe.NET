using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class tomaICMS
    {
        public refNF refNf;

        private string _refNFe;
        private string _refCte;

        public string refNFe { get { return _refNFe; } set { _refNFe = value; } }
        public string refCte { get { return _refCte; } set { _refCte = value; } }
    }
}
