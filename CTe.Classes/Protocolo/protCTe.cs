using System.Xml.Serialization;
using CTeDLL.Classes.Servicos.Tipos;

namespace CTeDLL.Classes.Protocolo
{
    public class protCTe
    {
        public protCTe()
        {
            infProt = new infProt();
        }

        /// <summary>
        ///     PR02 - Versão do leiaute das informações de Protocolo.
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     PR03 - Informações do Protocolo de resposta. TAG a ser assinada
        /// </summary>
        public infProt infProt { get; set; }
    }
}