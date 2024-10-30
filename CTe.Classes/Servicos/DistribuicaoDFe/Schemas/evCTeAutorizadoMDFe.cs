using System.Xml.Serialization;
using CTe.Classes.Informacoes.Emitente;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    public class evCTeAutorizadoMDFe
   {

       [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
       public MDFe MDFe { get; set; }

        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
       public emit emit { get; set; }

        public string descEvento { get; set; }
   
    }
}