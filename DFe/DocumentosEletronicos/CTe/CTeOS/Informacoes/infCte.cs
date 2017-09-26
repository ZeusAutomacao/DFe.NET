using System.Xml.Serialization;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Emitente;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Valores;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Complemento;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Identificacao;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Impostos;
using DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Tomador;
using DFe.DocumentosEletronicos.Flags;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes
{
    public class infCte
    {
        [XmlAttribute]
        public VersaoServico versao { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        public ide ide { get; set; }

        public compl compl { get; set; }

        public emit emit { get; set; }

        public toma toma { get; set; }

        public vPrest vPrest { get; set; }

        public imp imp { get; set; }
    }
}