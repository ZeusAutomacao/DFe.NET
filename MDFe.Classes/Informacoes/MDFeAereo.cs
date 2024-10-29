using System;
using System.Xml.Serialization;
using DFe.Utils;
using MDFe.Classes.Contratos;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeAereo : MDFeModalContainer
    {
        /// <summary>
        /// 1 - Marca da Nacionalidade da aeronave 
        /// </summary>
        [XmlElement(ElementName = "nac")]
        public string Nac { get; set; }

        /// <summary>
        /// 1 - Marca de Matrícula da aeronave 
        /// </summary>
        [XmlElement(ElementName = "matr")]
        public string Matr { get; set; }

        /// <summary>
        /// 1 - Número do Voo 
        /// </summary>
        [XmlElement(ElementName = "nVoo")]
        public string NVoo { get; set; }

        /// <summary>
        /// 1 - Aeródromo de Embarque 
        /// </summary>
        [XmlElement(ElementName = "cAerEmb")]
        public string CAerEmb { get; set; }

        /// <summary>
        /// 1 - Aeródromo de Destino 
        /// </summary>
        [XmlElement(ElementName = "cAerDes")]
        public string CAerDes { get; set; }

        /// <summary>
        /// 1 - Data do Voo 
        /// </summary>
        [XmlIgnore]
        public DateTime DVoo { get; set; }

        /// <summary>
        /// Proxy para converter DVoo em string yyyy-MM-dd
        /// </summary>
        [XmlElement(ElementName = "dVoo")]
        public string ProxyDVoo
        {
            get { return DVoo.ParaDataString(); }
            set { DVoo = DateTime.Parse(value); }
        }
    }
}