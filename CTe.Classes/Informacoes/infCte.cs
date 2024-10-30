using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Complemento;
using CTe.Classes.Informacoes.Destinatario;
using CTe.Classes.Informacoes.Emitente;
using CTe.Classes.Informacoes.Expedidor;
using CTe.Classes.Informacoes.infCTeNormal;
using CTe.Classes.Informacoes.Identificacao;
using CTe.Classes.Informacoes.Impostos;
using CTe.Classes.Informacoes.Recebedor;
using CTe.Classes.Informacoes.Remetente;
using CTe.Classes.Informacoes.Valores;
using CTe.Classes.Servicos.Tipos;

namespace CTe.Classes.Informacoes
{
    public class infCte
    {
        [XmlAttribute]
        public versao versao { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        public ide ide { get; set; }

        public compl compl { get; set; }

        public emit emit { get; set; }

        public tomaCteOs toma { get; set; }

        public rem rem { get; set; }

        public exped exped { get; set; }

        public receb receb { get; set; }

        public dest dest { get; set; }

        public vPrest vPrest { get; set; }

        public imp imp { get; set; }

        public infCTeNorm infCTeNorm { get; set; }

        public infCteComp.infCteComp infCteComp { get; set; }

        public infCteAnu.infCteAnu infCteAnu { get; set; }

        [XmlElement("autXML")]
        public List<autXML> autXML { get; set; }

        public infRespTec.infRespTec infRespTec { get; set; }

    }
}