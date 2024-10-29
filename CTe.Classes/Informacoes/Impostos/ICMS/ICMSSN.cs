using CTe.Classes.Informacoes.Impostos.ICMS.Tipos;
using CTe.Classes.Informacoes.Tipos;

namespace CTe.Classes.Informacoes.Impostos.ICMS
{
    public class ICMSSN : ICMSBasico
    {
        public ICMSSN()
        {
            indSN = indSN.Sim;
        }

        public CST? CST { get; set; }
        public bool CSTSpecified { get { return CST.HasValue; } }

        public indSN indSN { get; set; }
    }
}