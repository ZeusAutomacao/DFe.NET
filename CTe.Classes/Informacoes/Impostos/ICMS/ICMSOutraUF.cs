using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMSOutraUF : ICMSBasico
    {
        public string CST { get; set; } = "90";

        public decimal pRedBCOutraUF { get; set; }

        public decimal vBCOutraUF { get; set; }

        public decimal pICMSOutraUF { get; set; }

        public decimal vICMSOutraUF { get; set; }
    }
}
