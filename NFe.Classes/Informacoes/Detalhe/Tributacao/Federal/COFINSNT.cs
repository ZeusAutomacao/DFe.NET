using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class COFINSNT : COFINSBasico
    {
        /// <summary>
        ///     S06 - Código de Situação Tributária da COFINS
        /// </summary>
        /// 
        [XmlElement(Order = 1)]
        public CSTCOFINS CST { get; set; }
    }
}