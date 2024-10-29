using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{

    /// <summary>
    /// Leiaute de compartilhamento de solicitação de registro de evento do CT-e
    /// </summary>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte", IsNullable = false)]
    public class procEventoCTe
    {
        [XmlAttribute()]
        public decimal versao { get; set; }

        [XmlAttribute()]
        public string ipTransmissor { get; set; }

        /// <summary>
        /// Mensagem de solicitação de registro de evento do CT-e 
        /// </summary>
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public eventoCTe eventoCTe { get; set; }

        /// <summary>
        /// Mensagem de retorno do resultado da solicitação de registro de evento do CT-e 
        /// </summary>
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public retEventoCTe retEventoCTe { get; set; }


    }
}