using System;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe
{
    public class AntesDeEnviarCteOs : EventArgs
    {
        public AntesDeEnviarCteOs(CTeOS.CTeOS cteOs)
        {
            CteOsOs = cteOs;
        }

        public CTeOS.CTeOS CteOsOs { get; }
    }
}