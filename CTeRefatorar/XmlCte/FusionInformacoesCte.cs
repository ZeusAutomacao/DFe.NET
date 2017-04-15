using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionInformacoesCTe
    {
        private string _id;
        private string _versao;

        [XmlAttribute("Id")]
        public string Id
        {
            get { return _id; }
            set { _id = value.Replace(" ", "").Trim(); }
        }

        [XmlAttribute("versao")]
        public string Versao
        {
            get { return _versao; }
            set { _versao = value.Trim(); }
        }

        [XmlElement(ElementName = "ide")]
        public FusionIdentificacaoCTe Identificacao { get; set; }

        [XmlElement(ElementName = "compl")]
        public FusionDadosComplementaresCTe DadosComplementares { get; set; }

        [XmlElement(ElementName = "emit")]
        public FusionEmitenteCTe Emitente { get; set; }

        [XmlElement(ElementName = "rem")]
        public FusionRemetenteCTe Remetente { get; set; }

        [XmlElement(ElementName = "exped")]
        public FusionExpedidorCTe Expedidor { get; set; }

        [XmlElement(ElementName = "receb")]
        public FusionRecebedorCTe Recebedor { get; set; }

        [XmlElement(ElementName = "dest")]
        public FusionDestinatarioCTe Destinatario { get; set; }

        [XmlElement(ElementName = "vPrest")]
        public FusionValoresPrestacaoServicoCTe ValoresPrestacaoServico { get; set; }

        [XmlElement(ElementName = "imp")]
        public FusionImpostoCTe Imposto { get; set; }

        [XmlElement(ElementName = "infCTeNorm")]
        public FusionInformacaoCTeNormalCTe InformacoesCTeNormal { get; set; }

        public FusionInformacoesCTe()
        {
            Identificacao = new FusionIdentificacaoCTe();
            DadosComplementares = new FusionDadosComplementaresCTe();
            Emitente = new FusionEmitenteCTe();
            Remetente = new FusionRemetenteCTe();
            Destinatario = new FusionDestinatarioCTe();
            ValoresPrestacaoServico = new FusionValoresPrestacaoServicoCTe();
            Imposto = new FusionImpostoCTe();
            InformacoesCTeNormal = new FusionInformacaoCTeNormalCTe();
        }
    }
}