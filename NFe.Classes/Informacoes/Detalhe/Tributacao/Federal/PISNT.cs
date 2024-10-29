using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class PISNT : PISBasico
    {
        /// <summary>
        ///     Q06 - Código de Situação Tributária do PIS
        /// </summary>
        /// 
        [XmlElement(Order = 1)]
        public CSTPIS CST { get; set; }
    }
}