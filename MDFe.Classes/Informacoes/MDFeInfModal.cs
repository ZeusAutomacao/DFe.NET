using System;
using System.Xml.Serialization;
using MDFe.Classes.Contratos;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfModal
    {
        public MDFeInfModal()
        {
            VersaoModal = MDFeVersaoModal.Versao100;
        }

        /// <summary>
        ///  2- Versão do leiaute específico para o Modal
        /// </summary>
        [XmlAttribute(AttributeName = "versaoModal")]
        public MDFeVersaoModal VersaoModal { get; set; }

        /// <summary>
        /// 2 - XML do modal Insira neste local o XML específico do modal(rodoviário, aéreo, ferroviário ou aquaviário)
        /// </summary>
        [XmlElement("rodo", typeof(MDFeRodo), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("aereo", typeof(MDFeAereo), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("aquav", typeof(MDFeAquav), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("ferrov", typeof(MDFeFerrov), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        public MDFeModalContainer Modal { get; set; }
    }
}