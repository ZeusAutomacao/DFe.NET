using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfContratante
    {
        /// <summary>
        /// 3 - Razão social ou Nome do contratante 
        /// </summary>
        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        /// <summary>
        /// 3 - Número do CPF do contratante do serviço
        /// </summary>
        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }

        /// <summary>
        /// 3 - Número do CNPJ do contratante serviço
        /// </summary>
        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        /// <summary>
        /// 3 - Identificador do contratante em caso de contratante estrangeiro
        /// </summary>
        [XmlElement(ElementName = "idEstrangeiro")]
        public string IdEstrangeiro { get; set; }
    }
}