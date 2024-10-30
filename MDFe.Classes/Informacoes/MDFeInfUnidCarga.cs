using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfUnidCarga
    {
        /// <summary>
        /// 6 - Tipo da Unidade de Carga
        /// </summary>
        [XmlElement(ElementName = "tpUnidCarga")]
        public MDFeTpUnidCarga TpUnidCarga { get; set; }

        /// <summary>
        /// 6 - Identificação da Unidade de Carga 
        /// </summary>
        [XmlElement(ElementName = "idUnidCarga")]
        public string IdUnidCarga { get; set; }

        /// <summary>
        /// 6 - Lacres das Unidades de Carga 
        /// </summary>
        [XmlElement(ElementName = "lacUnidCarga")]
        public List<MDFeLacUnidCarga> LacUnidCargas { get; set; }

        /// <summary>
        /// 6 - Quantidade rateada (Peso,Volume) 
        /// </summary>
        [XmlElement(ElementName = "qtdRat")]
        public decimal? QtdRat { get; set; }

        public bool QtdRatSpecified { get { return QtdRat.HasValue; } }
   }
}