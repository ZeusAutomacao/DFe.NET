using System;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe
{
    public class AntesDeAssinar : EventArgs
    {
        public CTeOS.CTeOS CteOs { get; }

        public AntesDeAssinar(CTeOS.CTeOS cteOs)
        {
            CteOs = cteOs;
        }
    }
}