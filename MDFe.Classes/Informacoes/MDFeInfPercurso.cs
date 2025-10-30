using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace MDFe.Classes.Informacoes
{
    public class MDFeInfPercurso
    {
        /// <summary>
        /// 3 - Sigla das Unidades da Federação do percurso do veículo.
        /// </summary>
        [XmlIgnore]
        public Estado UFPer { get; set; }

        /// <summary>
        /// Proxy para UFPer
        /// </summary>
        [XmlElement(ElementName = "UFPer")]
        public string ProxyUFPer {
            get { return UFPer.GetSiglaUfString(); }
            set { UFPer = UFPer.SiglaParaEstado(value); }
        }


    }
}