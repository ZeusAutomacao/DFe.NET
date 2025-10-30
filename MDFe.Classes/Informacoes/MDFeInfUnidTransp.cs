using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfUnidTransp
    {
        /// <summary>
        /// 5 - Tipo da Unidade de Transporte 
        /// </summary>
        [XmlElement(ElementName = "tpUnidTransp")]
        public MDFeTpUnidTransp TpUnidTransp { get; set; }

        /// <summary>
        /// 5 - Identificação da Unidade de Transporte
        /// </summary>
        [XmlElement(ElementName = "idUnidTransp")]
        public string IdUnidTransp { get; set; }

        /// <summary>
        /// 5 - Lacres das Unidades de Transporte
        /// </summary>
        [XmlElement(ElementName = "lacUnidTransp")]
        public List<MDFeLacUnidTransp> LacUnidTransps { get; set; }

        /// <summary>
        /// 5 - Informações das Unidades de Carga (Containeres/ULD/Outros)
        /// </summary>
        [XmlElement(ElementName = "infUnidCarga")]
        public List<MDFeInfUnidCarga> InfUnidCargas { get; set; }

        /// <summary>
        /// 5 - Quantidade rateada (Peso,Volume) 
        /// </summary>
        [XmlElement(ElementName = "qtdRat")]
        public decimal? QtdRat { get; set; }

        public bool QtdRatSpecified { get { return QtdRat.HasValue; } }
   }
}