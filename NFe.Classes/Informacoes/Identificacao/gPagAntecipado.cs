using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Identificacao
{
    public class gPagAntecipado
    {
        [XmlElement("refNFe")]
        public List<string> refNFe { get; set; }
    }
}