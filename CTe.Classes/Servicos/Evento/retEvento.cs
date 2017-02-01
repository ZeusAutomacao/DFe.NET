using System.Xml.Serialization;

namespace CTeDLL.Classes.Servicos.Evento
{
    public class retEvento
    {
        /// <summary>
        ///     HR10 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     HR11 - Grupo de informações do registro do Evento
        /// </summary>
        public infEventoRet infEvento { get; set; }
    }
}