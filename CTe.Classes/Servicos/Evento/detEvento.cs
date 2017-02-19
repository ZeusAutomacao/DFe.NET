using System;
using System.Xml.Serialization;
using CTeDLL;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes.Entidades;

namespace CTeDLL.Classes.Servicos.Evento
{
    public class detEvento
    {
        /// <summary>
        ///     HP18 - Versão do Pedido de Cancelamento, da carta de correção ou EPEC, deve ser informado com a mesma informação da
        ///     tag verEvento (HP16)
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     HP19 - "Cancelamento", "Carta de Correção", "Carta de Correcao" ou "EPEC"
        /// </summary>
        public string descEvento { get; set; }

        #region Cancelamento

        private string _nprot;

        /// <summary>
        ///     HP20 - Informar o número do Protocolo de Autorização da NF-e a ser Cancelada.
        /// </summary>
        public string nProt
        {
            get { return _nprot; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                descEvento = "Cancelamento";
                LimpaDadosCartaCorrecao();
                LimpaDadosEpec();
                _nprot = value;
            }
        }

        private string _xjust;

        /// <summary>
        ///     HP21 - Informar a justificativa do cancelamento
        /// </summary>
        public string xJust
        {
            get { return _xjust; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                descEvento = "Cancelamento";
                LimpaDadosCartaCorrecao();
                LimpaDadosEpec();
                _xjust = value;
            }
        }

        #endregion

        #region Carta de Correção

        private Estado? _cOrgao;

        /// <summary>
        ///     P20 - Código do Órgão do Autor do Evento.
        ///     Nota: Informar o código da UF do Emitente para este evento.
        /// </summary>
        public Estado? cOrgao
        {
            get { return null; }
            set { _cOrgao = null; }
        }

        private string _xcorrecao;

        /// <summary>
        ///     HP20 - Correção a ser considerada, texto livre. A correção mais recente substitui as anteriores.
        /// </summary>
        public string xCorrecao
        {
            get { return _xcorrecao; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (cOrgao == Estado.MT || cOrgao == Estado.SP)
                {
                    descEvento = "Carta de Correcao";
                    xCondUso = "A Carta de Correcao e disciplinada pelo paragrafo 1o-A do art. 7o do Convenio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularizacao de erro ocorrido na emissao de documento fiscal, desde que o erro nao esteja relacionado com: I - as variaveis que determinam o valor do imposto tais como: base de calculo, aliquota, diferenca de preco, quantidade, valor da operacao ou da prestacao; II - a correcao de dados cadastrais que implique mudanca do remetente ou do destinatario; III - a data de emissao ou de saida.";
                }
                else
                {
                    descEvento = "Carta de Correção";
                    xCondUso = "A Carta de Correção é disciplinada pelo § 1º-A do art. 7º do Convênio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularização de erro ocorrido na emissão de documento fiscal, desde que o erro não esteja relacionado com: I - as variáveis que determinam o valor do imposto tais como: base de cálculo, alíquota, diferença de preço, quantidade, valor da operação ou da prestação; II - a correção de dados cadastrais que implique mudança do remetente ou do destinatário; III - a data de emissão ou de saída.";
                }

                cOrgao = null;
                LimpaDadosCancelamento();
                LimpaDadosEpec();

                _xcorrecao = value;
            }
        }

        private string _xconduso;

        /// <summary>
        ///     HP20a - Condições de uso da Carta de Correção
        /// </summary>
        public string xCondUso
        {
            get { return _xconduso; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                cOrgao = null;
                _xconduso = value;
            }
        }

        #endregion

        #region EPEC

        private Estado? _cOrgaoAutor;

        /// <summary>
        ///     P20 - Código do Órgão do Autor do Evento.
        ///     Nota: Informar o código da UF do Emitente para este evento.
        /// </summary>
        public Estado? cOrgaoAutor
        {
            get { return _cOrgaoAutor; }
            set
            {
                if (value == null) return;
                descEvento = "EPEC";
                LimpaDadosCancelamento();
                LimpaDadosCartaCorrecao();
                _cOrgaoAutor = value;
            }
        }

        /// <summary>
        ///     P21 - Informar "1=Empresa Emitente" para este evento.
        ///     Nota: 1=Empresa Emitente; 2=Empresa Destinatária;
        ///     3=Empresa; 5=Fisco; 6=RFB; 9=Outros Órgãos.
        /// </summary>
        public TipoAutor? tpAutor { get; set; }

        /// <summary>
        ///     P22 - Versão do aplicativo do Autor do Evento.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     P23 - Data e hora no formato UTC (Universal Coordinated Time): "AAAA-MM-DDThh:mm:ss TZD".
        /// </summary>
        public string dhEmi { get; set; }

        /// <summary>
        ///     P24 - 0 - CT-e Normal; 1 - CT-e de Complemento de Valores; 2 - CT-e de Anulação; 3 - CT-e Substituto;
        /// </summary>
        public tpCTe? tpCTe { get; set; }

        /// <summary>
        ///     P25 - IE do Emitente
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        ///     P26
        /// </summary>
        public dest dest { get; set; }

        public bool ShouldSerializecOrgaoAutor()
        {
            return cOrgaoAutor.HasValue;
        }

        public bool ShouldSerializecOrgao()
        {
            return cOrgao.HasValue;
        }

        public bool ShouldSerializetpAutor()
        {
            return tpAutor.HasValue;
        }

        public bool ShouldSerializetpNF()
        {
            return tpCTe.HasValue;
        }

        private void LimpaDadosCancelamento()
        {
            nProt = "";
            xJust = "";
        }

        private void LimpaDadosCartaCorrecao()
        {
            xCorrecao = "";
            xCondUso = "";
            cOrgao = null;
        }

        private void LimpaDadosEpec()
        {
            cOrgao = null;
            cOrgaoAutor = null;
            tpAutor = null;
            verAplic = null;
            dhEmi = null;
            tpCTe = null;
            IE = null;
            dest = null;
            //vNF = null;
            //vICMS = null;
            //vST = null;
        }

        #endregion
    }
}