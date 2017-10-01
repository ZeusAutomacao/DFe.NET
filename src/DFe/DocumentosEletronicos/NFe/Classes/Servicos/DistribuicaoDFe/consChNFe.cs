using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.NFe.Classes.Servicos.DistribuicaoDFe
{
    /// <summary>
    /// A11 - Grupo para consultar uma NF-e pela chave de acesso
    /// </summary>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class consChNFe
    {
        /// <summary>
        /// A12 - Chave de acesso específica
        /// </summary>
        public string chNFe { get; set; }
    }
}