using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMSSN : ICMSBasico
    {
        private int _indSN;

        public int indSN { get { return _indSN; } set { _indSN = value; } }
    }
}
