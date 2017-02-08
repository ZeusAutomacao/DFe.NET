using System.Collections.Generic;
using CTeDLL.Classes.Servicos.Consulta;
using CTeDLL.Classes.Servicos.Evento;

namespace CTeDLL.Servicos.Retorno
{
    public class RetornoRecepcaoEvento : RetornoBasico
    {
        public RetornoRecepcaoEvento(string envioStr, string retornoStr, string retornoCompletaStr, retEnvEvento retorno, List<procEventoCTe> procEventosCTe)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
            ProcEventosCTe = procEventosCTe;
        }

        public new retEnvEvento Retorno { get; set; }

        public List<procEventoCTe> ProcEventosCTe { get; set; }
    }
}