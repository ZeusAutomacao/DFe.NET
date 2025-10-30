using CTe.Classes.Simplificado.Informacoes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTe.Classes.Simplificado.Carga
{
    /// <summary>
    /// Detalhamento de cada entrega/prestação do CT-e Simplificado.
    /// </summary>
    public class det
    {
        /// <summary>
        /// Número do item na lista de detalhamento.
        /// </summary>
        [XmlAttribute(AttributeName = "nItem")]
        public int nItem { get; set; }

        /// <summary>
        /// Código do Município de início da prestação (IBGE).
        /// </summary>
        [XmlElement(ElementName = "cMunIni")]
        public int cMunIni { get; set; }

        /// <summary>
        /// Nome do Município do início da prestação.
        /// </summary>
        [XmlElement(ElementName = "xMunIni")]
        public string xMunIni { get; set; }

        /// <summary>
        /// Código do Município de término da prestação (IBGE).
        /// </summary>
        [XmlElement(ElementName = "cMunFim")]
        public int cMunFim { get; set; }

        /// <summary>
        /// Nome do Município do término da prestação.
        /// </summary>
        [XmlElement(ElementName = "xMunFim")]
        public string xMunFim { get; set; }

        /// <summary>
        /// Valor da Prestação do Serviço.
        /// </summary>
        [XmlElement(ElementName = "vPrest")]
        public decimal vPrest { get; set; }

        /// <summary>
        /// Valor a Receber.
        /// </summary>
        [XmlElement(ElementName = "vRec")]
        public decimal vRec { get; set; }

        /// <summary>
        /// Componentes do valor da prestação.
        /// </summary>
        [XmlElement(ElementName = "Comp")]
        public List<comp> Comp { get; set; }

        /// <summary>
        /// Informações das NF-e vinculadas à entrega.
        /// </summary>
        [XmlElement(ElementName = "infNFe")]
        public List<infNFe> infNFe { get; set; }
    }
}
