using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    public class evPagtoOperMDFe : MDFeEventoContainer
    {
        public evPagtoOperMDFe()
        {
            descEvento = "Pagamento Operacao MDF-e";
        }

        public string descEvento { get; set; }

        public string nProt { get; set; }

        public infViagens infViagens { get; set; }

        [XmlElement(ElementName = "infPag")]
        public List<infPag> infPag { get; set; }
    }

    [Serializable]
    public class infViagens
    {
        public int qtdViagens { get; set; }

        public int nroViagens { get; set; }
    }
}