using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Servicos.Evento
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class envEvento
    {
        /// <summary>
        ///     HP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     HP03 - Identificador de controle do Lote de envio do Evento.
        ///     Número sequencial autoincremental único para identificação do Lote. A responsabilidade de gerar e controlar é
        ///     exclusiva do autor do evento. O Web Service não faz qualquer uso deste identificador.
        /// </summary>
        public int idLote { get; set; }

        /// <summary>
        ///     HP04 - Evento, um lote pode conter até 20 eventos
        /// </summary>
        [XmlElement("evento")]
        public List<eventoCTe> evento { get; set; }
    }
}