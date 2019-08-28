using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes;
using CTe.Classes.Informacoes.infCteAnu;
using CTe.Classes.Informacoes.infCteComp;
using CTe.Classes.Informacoes.infRespTec;
using DFe.Classes.Flags;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Emitente;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Valores;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Complemento;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Identificacao;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Impostos;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.InfCTeNormal;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Tomador;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes
{
    public class infCteOS
    {
        [XmlAttribute]
        public VersaoServico versao { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        [XmlElement(ElementName = "ide")]
        public ideOs ide { get; set; }

        [XmlElement(ElementName = "compl")]
        public complOs compl { get; set; }

        [XmlElement(ElementName = "emit")]
        public emitOs emit { get; set; }

        [XmlElement(ElementName = "toma")]
        public tomaOs toma { get; set; }

        [XmlElement(ElementName = "vPrest")]
        public vPrestOs vPrest { get; set; }

        [XmlElement(ElementName = "imp")]
        public impOs imp { get; set; }

        [XmlElement(ElementName = "infCTeNorm")]
        public infCTeNormOs infCTeNorm { get; set; }

        public infCteComp infCteComp { get; set; }

        public infCteAnu infCteAnu { get; set; }

        public List<autXML> autXml { get; set; }

        [XmlElement(ElementName = "infRespTec")]
        public infRespTec infRespTec { get; set; }
    }
}