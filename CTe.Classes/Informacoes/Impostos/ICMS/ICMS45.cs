using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS45 : ICMSBasico
    {
        private string _CST;

        public string CST { get { return _CST; } set { _CST = value; } }
    }
}
