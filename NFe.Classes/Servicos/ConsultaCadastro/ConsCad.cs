using System.Xml.Serialization;

namespace NFe.Classes.Servicos.ConsultaCadastro
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class ConsCad
    {
        /// <summary>
        ///     GP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     GP03 - Dados da consulta
        /// </summary>
        public infConsEnv infCons { get; set; }
    }
}