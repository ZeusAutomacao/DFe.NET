using System;
using System.Xml.Serialization;
using DFe.Classes;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeTot
    {
        private decimal _vCarga;
        private decimal _qCarga;

        /// <summary>
        /// 2 - Quantidade total de CT-e relacionados no Manifesto
        /// </summary>
        [XmlElement(ElementName = "qCTe")]
        public int? QCTe { get; set; }

        /// <summary>
        /// 2 - Quantidade total de NF-e relacionadas no Manifesto
        /// </summary>
        [XmlElement(ElementName = "qNFe")]
        public int? QNFe { get; set; }

        /// <summary>
        /// 2 - Quantidade total de MDF-e relacionados no Manifesto Aquaviário
        /// </summary>
        [XmlElement(ElementName = "qMDFe")]
        public int? QMDFe { get; set; }

        /// <summary>
        /// 2 - Valor total da carga / mercadorias transportadas
        /// </summary>
        [XmlElement(ElementName = "vCarga")]
        public decimal vCarga
        {
            get { return _vCarga; }
            set { _vCarga = value.Arredondar(2); }
        }

        /// <summary>
        /// 2 - Codigo da unidade de medida do Peso Bruto da Carga / Mercadorias transportadas
        /// </summary>
        [XmlElement(ElementName = "cUnid")]
        public MDFeCUnid CUnid { get; set; }

        /// <summary>
        /// 2 - Peso Bruto Total da Carga / Mercadorias transportadas
        /// </summary>
        [XmlElement(ElementName = "qCarga")]
        public decimal QCarga
        {
            get { return _qCarga; }
            set { _qCarga = value.Arredondar(4); }
        }


        /// <summary>
        /// Se null não aparece no xml
        /// </summary>
        public bool QCTeSpecified { get { return QCTe.HasValue; } }

        /// <summary>
        /// Se null não aparece no xml
        /// </summary>
        public bool QNFeSpecified { get { return QNFe.HasValue; } }

        /// <summary>
        /// Se null não aparece no xml
        /// </summary>
        public bool QMDFeSpecified { get { return QMDFe.HasValue; } }
    }
}