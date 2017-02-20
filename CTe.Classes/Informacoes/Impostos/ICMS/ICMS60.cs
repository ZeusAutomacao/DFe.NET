using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS60 : ICMSBasico
    {
        public string CST { get; set; } = "60";

        public decimal vBCSTRet { get; set; }

        public decimal vICMSSTRet { get; set; }

        public decimal pICMSSTRet { get; set; }

        public decimal vCred { get; set; }
    }
}
