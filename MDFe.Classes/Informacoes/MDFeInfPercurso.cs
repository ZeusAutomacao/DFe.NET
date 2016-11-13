using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    public class MDFeInfPercurso
    {
        [XmlIgnore]
        public EstadoUF UFPer { get; set; }

        [XmlElement(ElementName = "UFPer")]
        public string ProxyUFPer {
            get { return UFPer.GetSiglaUfString(); }
            set { UFPer = UFPer.SiglaParaEstado(value); }
        }
    }
}