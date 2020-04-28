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
        [XmlIgnore]
        public int qtdViagens { get; set; }

        [XmlElement("qtdViagens")]
        public string qtdViagensProxy
        {
            get { return qtdViagens.ToString("D5"); }
            set { qtdViagens = int.Parse(value); }
        }

        [XmlIgnore]
        public int nroViagem { get; set; }

        [XmlElement("nroViagem")]
        public string nroViagemProxy
        {
            get { return nroViagem.ToString("D5"); }
            set { nroViagem = int.Parse(value); }
        }
    }
}