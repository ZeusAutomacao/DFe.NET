using System;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Consulta;

namespace DFe.DocumentosEletronicos.CTe.Servicos.ConsultaProtocoloCTe
{
    public class AntesDeConsultar : EventArgs
    {
        public consSitCTe ConsSitCTe { get; }

        public AntesDeConsultar(consSitCTe consSitCTe)
        {
            ConsSitCTe = consSitCTe;
        }
    }
}