using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class IPINT : IPIBasico
    {
        /// <summary>
        ///     O09 - Código da Situação Tributária do IPI:
        /// </summary>
        /// 
        [XmlElement(Order = 1)]
        public CSTIPI CST { get; set; }
    }
}