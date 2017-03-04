using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Servicos.Evento
{
    public class evCCeCTe : EventoContainer
    {
        public evCCeCTe()
        {
            descEvento = "Carta de Correcao";
            xCondUso = "A Carta de Correcao e disciplinada pelo Art. 58-B do CONVENIO/SINIEF 06/89: Fica permitida a utilizacao de carta de correcao, para regularizacao de erro ocorrido na emissao de documentos fiscais relativos a prestacao de servico de transporte, desde que o erro nao esteja relacionado com: I - as variaveis que determinam o valor do imposto tais como: base de calculo, aliquota, diferenca de preco, quantidade, valor da prestacao;II - a correcao de dados cadastrais que implique mudanca do emitente, tomador, remetente ou do destinatario;III - a data de emissao ou de saida.";
        }

        public string descEvento { get; set; }

        [XmlElement(ElementName = "infCorrecao")]
        public List<infCorrecao> infCorrecao { get; set; }

        public string xCondUso { get; set; }
    }

    public class infCorrecao
    {

        public string grupoAlterado { get; set; }
        public string campoAlterado { get; set; }
        public string valorAlterado { get; set; }
        public int? nroItemAlterado { get; set; }
        public bool nroItemAlteradoSpecified => nroItemAlterado.HasValue;
    }
}