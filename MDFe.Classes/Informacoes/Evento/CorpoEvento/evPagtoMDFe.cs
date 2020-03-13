using System;
using System.Collections.Generic;

namespace MDFe.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    public class evPagtoMDFe : MDFeEventoContainer
    {
        public evPagtoMDFe()
        {
            descEvento = "Pagamento Operação MDF-e";
        }

        public string descEvento { get; set; }

        public string nProt { get; set; }

        public infViagens infViagens { get; set; }

        public List<infPag> infPag { get; set; }
    }

    [Serializable]
    public class infViagens
    {
        public int qtdViagens { get; set; }

        public int nroViagens { get; set; }
    }
}