using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao;
using CTeDLL.Classes.Informacoes.Complemento;
using CTeDLL.Classes.Informacoes.Emitente;
using CTeDLL.Classes.Informacoes.Remetente;
using CTeDLL.Classes.Informacoes.Expedidor;
using CTeDLL.Classes.Informacoes.Recebedor;
using CTeDLL.Classes.Informacoes.Destinatario;
using CTeDLL.Classes.Informacoes.Valores;
using CTeDLL.Classes.Informacoes.Impostos;
using CTeDLL.Classes.Informacoes.InfCTeNormal;
using CTeDLL.Classes.Informacoes.InfCTeComplementar;
using CTeDLL.Classes.Informacoes.InfCTeAnulacao;

namespace CTeDLL.Classes.Informacoes
{
    public class infCte
    {
        [XmlAttribute]
        public string versao { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        public ide ide { get; set; }

        public compl compl { get; set; }

        public emit emit { get; set; }

        public rem rem { get; set; }

        public exped exped { get; set; }

        public receb receb { get; set; }

        public dest dest { get; set; }

        public vPrest vPrest { get; set; }

        public imp imp { get; set; }

        public infCTeNorm infCTeNorm { get; set; }

        public infCteComp infCteComp { get; set; }

        public infCteAnu infCteAnu { get; set; }

        [XmlElement("autXML")]
        public List<autXML> autXML { get; set; }

    }
}
