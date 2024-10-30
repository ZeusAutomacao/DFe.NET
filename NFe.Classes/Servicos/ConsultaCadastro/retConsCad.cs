using System.Xml.Serialization;

namespace NFe.Classes.Servicos.ConsultaCadastro
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class retConsCad : IRetornoServico
    {
        /// <summary>
        ///     GR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     GR03 - Dados da consulta
        /// </summary>
        public infConsRet infCons { get; set; }
    }
}