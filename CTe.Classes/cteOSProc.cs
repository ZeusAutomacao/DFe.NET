using System;
using System.Xml.Serialization;
using CTe.Classes.Protocolo;
using CTe.Classes.Servicos.Tipos;
using DFe.Utils;

namespace CTe.Classes
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "cteOSProc")]
    public class cteOSProc
    {
        [XmlAttribute]
        public versao versao { get; set; }

        public CTe CTeOS { get; set; }

        public protCTe protCTe { get; set; }


        public static cteOSProc LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<cteOSProc>(xml);
        }

        public static cteOSProc LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<cteOSProc>(caminhoArquivoXml);
        }
    }
}