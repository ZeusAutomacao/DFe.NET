using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public class MDFeInfEntregaParcial
    {
        /// <summary>
        /// 5 - Quantidade total de volumes
        /// /// </summary>
        [XmlElement(ElementName = "qtdTotal")]
        public decimal QtdTotal { get; set; }

        /// <summary>
        /// 5 - Quantidade de volumes enviados no MDF-e
        /// </summary>
        [XmlElement(ElementName = "qtdParcial")]
        public decimal QtdParcial { get; set; }
    }
}