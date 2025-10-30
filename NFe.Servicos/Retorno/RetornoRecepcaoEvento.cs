using System.Collections.Generic;
using NFe.Classes.Servicos.Consulta;
using NFe.Classes.Servicos.Evento;

namespace NFe.Servicos.Retorno
{
    public class RetornoRecepcaoEvento : RetornoBasico
    {
        public RetornoRecepcaoEvento(string envioStr, string retornoStr, string retornoCompletaStr, retEnvEvento retorno, List<procEventoNFe> procEventosNFe)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
            ProcEventosNFe = procEventosNFe;
        }

        public new retEnvEvento Retorno { get; set; }

        public List<procEventoNFe> ProcEventosNFe { get; set; }
    }
}