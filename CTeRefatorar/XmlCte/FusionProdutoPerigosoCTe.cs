using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionProdutoPerigosoCTe
    {
        [XmlElement(ElementName = "nONU")]
        public string NumeroOnuUn { get; set; }

        [XmlElement(ElementName = "xNomeAE")]
        public string NomeApropriado { get; set; }

        [XmlElement(ElementName = "xClaRisco")]
        public string ClasseRisco { get; set; }

        [XmlElement(ElementName = "grEmb")]
        public string GrupoEmbalagem { get; set; }

        [XmlElement(ElementName = "qTotProd")]
        public string QuantidadeTotalPorProduto { get; set; }

        [XmlElement(ElementName = "qVolTipo")]
        public string QuantidadeETiposVolume { get; set; }

        [XmlElement(ElementName = "pontoFulgor")]
        public string PontoFulgor { get; set; }
    }
}