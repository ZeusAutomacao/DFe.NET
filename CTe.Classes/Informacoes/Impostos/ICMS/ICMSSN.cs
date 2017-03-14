using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMSSN : ICMSBasico
    {
        public indSN indSN { get; set; } = indSN.Sim;
    }
}
