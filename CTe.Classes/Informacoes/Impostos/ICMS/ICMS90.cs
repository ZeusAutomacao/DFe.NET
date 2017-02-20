using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS90 : ICMSBasico
    {
        public string CST { get; set; } = "90";

        public decimal pRedBC { get; set; }

        public decimal vBC { get; set; }

        public decimal pICMS { get; set; }

        public decimal vICMS { get; set; }

        public decimal vCred { get; set; }
    }
}
