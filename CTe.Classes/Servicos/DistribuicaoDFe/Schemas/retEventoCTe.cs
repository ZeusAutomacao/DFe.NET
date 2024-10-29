using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{

    /// <summary>
    /// Mensagem de retorno do resultado da solicitação de registro de evento do CT-e 
    /// </summary>
    
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retEventoCTe
    {
        public retInfEvento infEvento { get; set; }

        [XmlAttribute()]
        public decimal versao { get; set; }
    }
}