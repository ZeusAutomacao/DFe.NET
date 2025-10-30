using System;
using System.Xml.Serialization;
using DFe.Classes;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeVag
    {
        private decimal _pesoBc;
        private decimal _pesoR;
        private decimal _tu;

        public decimal pesoBC
        {
            get { return _pesoBc.Arredondar(3); }
            set { _pesoBc = value.Arredondar(3); }
        }

        public decimal pesoR
        {
            get { return _pesoR.Arredondar(3); }
            set { _pesoR = value.Arredondar(3); }
        }

        public string tpVag { get; set; }

        /// <summary>
        /// 2 - Serie de Identificação do vagão
        /// </summary>
        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        /// <summary>
        /// 2 - Número de Identificação do vagão 
        /// </summary>
        [XmlElement(ElementName = "nVag")]
        public long NVag { get; set; }

        /// <summary>
        /// 2 - Sequencia do vagão na composição
        /// </summary>
        [XmlElement(ElementName = "nSeq")]
        public short? NSeq { get; set; }

        public bool NSeqSpecified { get { return NSeq.HasValue; } }

        /// <summary>
        /// 2 - Tonelada Útil 
        /// </summary>
        [XmlElement(ElementName = "TU")]
        public decimal TU
        {
            get { return _tu.Arredondar(3); }
            set { _tu = value.Arredondar(3); }
        }
    }
}