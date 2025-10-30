using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Utils.Configuracoes;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Classes.Informacoes.Evento
{
    [Serializable]
    public class MDFeInfEvento
    {
        [XmlIgnore]
        private readonly VersaoServico _versaoServico = MDFeConfiguracao.Instancia.VersaoWebService.VersaoLayout;

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlIgnore]
        public Estado COrgao { get; set; }

        [XmlElement(ElementName = "cOrgao")]
        public string COrgaoProxy
        {
            get
            {
                return COrgao.GetCodigoIbgeEmString();
            }
            set { COrgao = COrgao.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }

        [XmlIgnore]
        public DateTime DhEvento { get; set; }

        [XmlElement(ElementName = "dhEvento")]
        public string ProxyDhEvento
        {
            get
            {
                switch (_versaoServico)
                {
                    case VersaoServico.Versao100:
                        return DhEvento.ParaDataHoraStringSemUtc();
                    case VersaoServico.Versao300:
                        return DhEvento.ParaDataHoraStringUtc();
                    default:
                        throw new InvalidOperationException("Versão inválida do mdf-e");
                }
            }
            set { DhEvento = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "tpEvento")]
        public MDFeTipoEvento TpEvento { get; set; }

        [XmlElement(ElementName = "nSeqEvento")]
        public byte NSeqEvento { get; set; }

        [XmlElement(ElementName = "detEvento")]
        public MDFeDetEvento DetEvento { get; set; }
    }
}