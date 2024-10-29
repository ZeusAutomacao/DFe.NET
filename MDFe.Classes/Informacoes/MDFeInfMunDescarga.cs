using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfMunDescarga
    {
        /// <summary>
        /// 3 - Código do Município de Descarregamento
        /// </summary>
        [XmlElement(ElementName = "cMunDescarga")]
        public string CMunDescarga { get; set; }

        /// <summary>
        /// 3 - Nome do Município de Descarregamento
        /// </summary>
        [XmlElement(ElementName = "xMunDescarga")]
        public string XMunDescarga { get; set; }

        /// <summary>
        /// 3 - Conhecimentos de Tranporte - usar este grupo quando for prestador de serviço de transporte
        /// </summary>
        [XmlElement(ElementName = "infCTe")]
        public List<MDFeInfCTe> InfCTe { get; set; }

        /// <summary>
        /// 3 - Nota Fiscal Eletronica
        /// </summary>
        [XmlElement(ElementName = "infNFe")]
        public List<MDFeInfNFe> InfNFe { get; set; }

        /// <summary>
        /// 3 - Manifesto Eletrônico de Documentos Fiscais.Somente para modal Aquaviário (vide regras MOC)
        /// </summary>
        [XmlElement(ElementName = "infMDFeTransp")]
        public List<MDFeInfMDFeTransp> InfMdFeTransps { get; set; }
    }
}