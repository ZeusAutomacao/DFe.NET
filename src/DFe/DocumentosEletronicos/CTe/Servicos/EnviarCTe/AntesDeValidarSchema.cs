using System;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe
{
    public class AntesDeValidarSchema : EventArgs
    {
        public AntesDeValidarSchema(CTeOS.CTeOS cte)
        {
            this.Cte = cte;
        }

        public CTeOS.CTeOS Cte { get; }
    }
}