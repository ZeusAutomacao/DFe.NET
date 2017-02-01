using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infCteSub
    {
        public tomaICMS tomaICMS;
        public tomaNaoICMS tomaNaoICMS;

        private string _chCte;

        public string chCte { get { return _chCte; } set { _chCte = value; } }
    }
}
