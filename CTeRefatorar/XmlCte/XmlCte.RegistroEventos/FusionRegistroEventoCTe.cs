using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.RegistroEventos
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "eventoCTe")]
    public class FusionRegistroEventoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infEvento")]
        public FusionInformacaoEventoCTe InformacaoEvento { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public FusionAssinaturaDigital AssinaturaDigital { get; set; }

        public FusionRegistroEventoCTe()
        {
            InformacaoEvento = new FusionInformacaoEventoCTe();
        }
    }

    [Serializable]
    public class FusionInformacaoEventoCTe
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "cOrgao")]
        public byte CodigoOrgao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "chCTe")]
        public string Chave { get; set; }

        [XmlElement(ElementName = "dhEvento")]
        public string HoraEvento { get; set; }

        [XmlElement(ElementName = "tpEvento")]
        public int TipoEvento { get; set; }

        [XmlElement(ElementName = "nSeqEvento")]
        public byte SequencialEvento { get; set; }

        [XmlElement(ElementName = "detEvento")]
        public FusionDetalheEventoCTe DetalheEvento { get; set; }

        public FusionInformacaoEventoCTe()
        {
            DetalheEvento = new FusionDetalheEventoCTe();
        }
    }

    [Serializable]
    public class FusionDetalheEventoCTe
    {
        [XmlAttribute(AttributeName = "versaoEvento")]
        public string VersaoEvento { get; set; }

        [XmlElement(ElementName = "evCancCTe")]
        public FusionEventoCancelamentoCTe EventoCancelamento { get; set; }

        [XmlElement(ElementName = "evCCeCTe")]
        public FusionEventoCartaCorrecaoCTe EventoCartaCorrecao { get; set; }
    }

    [Serializable]
    public class FusionEventoCancelamentoCTe
    {
        [XmlElement(ElementName = "descEvento")]
        public string DescricaoEvento { get; set; } = "Cancelamento";

        [XmlElement(ElementName = "nProt")]
        public string NumeroProtocolo { get; set; }

        [XmlElement(ElementName = "xJust")]
        public string Justificativa { get; set; }
    }

    [Serializable]
    public class FusionEventoCartaCorrecaoCTe
    {
        [XmlElement(ElementName = "descEvento")]
        public string DescricaoEvento { get; set; } = "Carta de Correcao";

        [XmlElement(ElementName = "infCorrecao")]
        public List<FusionInfoCorrecaoCTe> InfoCorrrecoes { get; set; }

        [XmlElement(ElementName = "xCondUso")]
        public string CondicaoUso { get; set; } =
            @"A Carta de Correcao e disciplinada pelo Art. 58-B do CONVENIO/SINIEF 06/89: Fica permitida a utilizacao de carta de correcao, para regularizacao de erro ocorrido na emissao de documentos fiscais relativos a prestacao de servico de transporte, desde que o erro nao esteja relacionado com: I - as variaveis que determinam o valor do imposto tais como: base de calculo, aliquota, diferenca de preco, quantidade, valor da prestacao;II - a correcao de dados cadastrais que implique mudanca do emitente, tomador, remetente ou do destinatario;III - a data de emissao ou de saida."
            ;

        public FusionEventoCartaCorrecaoCTe()
        {
            InfoCorrrecoes = new List<FusionInfoCorrecaoCTe>();
        }
    }

    [Serializable]
    public class FusionInfoCorrecaoCTe
    {
        [XmlElement(ElementName = "grupoAlterado")]
        public string GrupoAlterado { get; set; }

        [XmlElement(ElementName = "campoAlterado")]
        public string CampoAlterado { get; set; }

        [XmlElement(ElementName = "valorAlterado")]
        public string ValorAlterado { get; set; }

        [XmlElement(ElementName = "nroItemAlterado")]
        public string NumeroItem { get; set; }
    }
}