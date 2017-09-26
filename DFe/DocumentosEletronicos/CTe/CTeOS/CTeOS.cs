using System.Xml.Serialization;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes;
using DFe.DocumentosEletronicos.Flags;

namespace DFe.DocumentosEletronicos.CTe.CTeOS
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class CTeOS
    {
        [XmlAttribute]
        public VersaoServico versao { get; set; }

        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public infCte infCte { get; set; }
    }
}