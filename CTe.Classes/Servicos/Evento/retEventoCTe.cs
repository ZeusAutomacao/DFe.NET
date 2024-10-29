using System.Xml.Serialization;
using CTe.Classes.Servicos.Tipos;
using DFe.Utils;

namespace CTe.Classes.Servicos.Evento
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retEventoCTe : RetornoBase
    {
        /// <summary>
        ///     HR10 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     HR11 - Grupo de informações do registro do Evento
        /// </summary>
        public infEventoRet infEvento { get; set; }


        public static retEventoCTe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retEventoCTe>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }

        public static retEventoCTe LoadXml(string xml, eventoCTe evento)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(evento);

            return retorno;
        }
    }
}