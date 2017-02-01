using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos;

namespace CTeDLL.Classes.Informacoes.InfCTeComplementar
{
    public class ICMSComp
    {
        public ICMS00 ICMS00;
        public ICMS20 ICMS20;
        public ICMS45 ICMS45;
        public ICMS60 ICMS60;
        public ICMS90 ICMS90;
        public ICMSOutraUF ICMSOutraUF;
        public ICMSSN ICMSSN;
    }
}
