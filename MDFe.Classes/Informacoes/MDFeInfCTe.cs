using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public class MDFeInfCTe
    {
        /// <summary>
        /// 4 - Conhecimento Eletrônico - Chave de Acesso
        /// </summary>
        [XmlElement(ElementName = "chCTe")]
        public string ChCTe { get; set; }

        /// <summary>
        /// 4 - Segundo código de barras 
        /// </summary>
        [XmlElement(ElementName = "SegCodBarra")]
        public string SegCodBarra { get; set; }

        /// <summary>
        /// Indicador de Reentrega
        /// Versão 3.0
        /// </summary>
        [XmlElement(ElementName = "indReentrega")]
        public byte? IndReentrega { get; set; }

        public bool IndReentregaSpecified { get { return IndReentrega.HasValue; } }

        /// <summary>
        /// 4 - Informações das Unidades de Transporte (Carreta/Reboque/Vagão) 
        /// </summary>
        [XmlElement(ElementName = "infUnidTransp")]
        public List<MDFeInfUnidTransp> InfUnidTransps { get; set; }

        /// <summary>
        /// Preenchido quando for transporte de produtos classificados
        /// pela ONU como perigosos.
        /// MDF-e 3.0
        /// </summary>
        [XmlElement(ElementName = "peri")]
        public List<MDFePeri> Peri { get; set; }

        public infEntregaParcial infEntregaParcial { get; set; }
    }
}