using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace CTeDLL.Classes.Informacoes.Identificacao
{
    public class infPercurso
    {
        [XmlIgnore]
        public Estado UFPer { get; set; }

        [XmlElement(ElementName = "UFPer")]
        public string ProxyUFPer { get { return UFPer.GetSiglaUfString(); } set
        {
            UFPer = UFPer.SiglaParaEstado(value);
        } }


    }
}