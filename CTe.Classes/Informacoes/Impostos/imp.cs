using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class imp
    {
        public ICMS ICMS;

        private string _infAdFisco;

        public string infAdFisco { get { return _infAdFisco; } set { _infAdFisco = value; } }
    }
}
