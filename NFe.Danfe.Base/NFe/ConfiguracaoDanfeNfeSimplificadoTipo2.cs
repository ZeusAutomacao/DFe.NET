using NFe.Utils;
using System;

namespace NFe.Danfe.Base.NFe
{
    public class ConfiguracaoDanfeNfeSimplificadoTipo2 : ConfiguracaoDanfe
    {
        public ConfiguracaoDanfeNfeSimplificadoTipo2(
            NfeSimplificadoTipo2DetalheVendaNormal detalheVendaNormal,
            NfeSimplificadoTipo2DetalheVendaContigencia detalheVendaContigencia,
            byte[] logomarca = null,
            bool imprimeDescontoItem = false,
            float margemEsquerda = 4.5F,
            float margemDireita = 4.5F,
            NfeSimplificadoTipo2ModoImpressao modoImpressao = NfeSimplificadoTipo2ModoImpressao.MultiplasPaginas,
            bool documentoCancelado = false,
            NfeSimplificadoTipo2LayoutQrCode layoutQrCode = NfeSimplificadoTipo2LayoutQrCode.Abaixo,
            VersaoQrCode versaoQrCode = VersaoQrCode.QrCodeVersao1,
            bool duasLinhas = true,
            bool quebrarLinhasObservacao = true,
            bool exibirResumoCanhoto = true) : this()
        {
            DocumentoCancelado = documentoCancelado;
            DetalheVendaNormal = detalheVendaNormal;
            DetalheVendaContigencia = detalheVendaContigencia;
            Logomarca = logomarca;
            ImprimeDescontoItem = imprimeDescontoItem;
            MargemEsquerda = margemEsquerda;
            MargemDireita = margemDireita;
            ModoImpressao = modoImpressao;
            LayoutQrCode = layoutQrCode;
            VersaoQrCode = versaoQrCode;
            SegundaViaContingencia = true;
            DuasLinhas = duasLinhas;
            QuebrarLinhasObservacao = quebrarLinhasObservacao;
            ExibirResumoCanhoto = exibirResumoCanhoto;
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização
        /// </summary>
        public ConfiguracaoDanfeNfeSimplificadoTipo2()
        {
            DocumentoCancelado = false;
            DetalheVendaNormal = NfeSimplificadoTipo2DetalheVendaNormal.UmaLinha;
            DetalheVendaContigencia = NfeSimplificadoTipo2DetalheVendaContigencia.UmaLinha;
            ImprimeDescontoItem = false;
            ImprimeFoneEmitente = false;
            MargemEsquerda = 4.5F;
            MargemDireita = 4.5F;
            ModoImpressao = NfeSimplificadoTipo2ModoImpressao.MultiplasPaginas;
            LayoutQrCode = NfeSimplificadoTipo2LayoutQrCode.Abaixo;
            VersaoQrCode = VersaoQrCode.QrCodeVersao1;
            SegundaViaContingencia = true;
            DuasLinhas = true;
            QuebrarLinhasObservacao = true;
            ExibirResumoCanhoto = true;
            ResumoCanhoto = string.Empty;
            ChaveContingencia = string.Empty;
            ExibeCampoFatura = false;
            ExibeRetencoes = false;
            ImprimirISSQN = true;
            ImprimirDescPorc = false;
            ImprimirTotalLiquido = false;
            ImprimirUnidQtdeValor = ImprimirUnidQtdeValor.Comercial;
            ExibirTotalTributos = false;
            DecimaisValorUnitario = 2;
            DecimaisQuantidadeItem = 2;
            DataHoraImpressao = null;
        }

        // ── Parâmetros específicos do layout NF-e Simplificado Tipo 2 (QR Code) ──────────────

        /// <summary>
        /// Modo de impressão do detalhe (produtos) para NF-es em ambiente Normal
        /// </summary>
        public NfeSimplificadoTipo2DetalheVendaNormal DetalheVendaNormal { get; set; }

        /// <summary>
        /// Modo de impressão do detalhe (produtos) para NF-es em contingência/homologação
        /// </summary>
        public NfeSimplificadoTipo2DetalheVendaContigencia DetalheVendaContigencia { get; set; }

        /// <summary>
        /// Determina se o desconto do item será impresso no DANFE, quando houver
        /// </summary>
        public bool ImprimeDescontoItem { get; set; }

        /// <summary>
        /// Determina se o número de telefone do emitente será impresso no DANFE
        /// </summary>
        public bool ImprimeFoneEmitente { get; set; }

        /// <summary>
        /// Margem esquerda de impressão em milímetros
        /// </summary>
        public float MargemEsquerda { get; set; }

        /// <summary>
        /// Margem direita de impressão em milímetros
        /// </summary>
        public float MargemDireita { get; set; }

        /// <summary>
        /// Determina o modo de impressão do DANFE da NF-e Simplificado Tipo 2
        /// </summary>
        public NfeSimplificadoTipo2ModoImpressao ModoImpressao { get; set; }

        /// <summary>
        /// Determina se o QRCode será impresso ao lado ou abaixo dos dados
        /// </summary>
        public NfeSimplificadoTipo2LayoutQrCode LayoutQrCode { get; set; }

        /// <summary>
        /// Versão do QRCode. 1.0 ou 2.0
        /// </summary>
        public VersaoQrCode VersaoQrCode { get; set; }

        /// <summary>
        /// Envia segunda via de contingência para a impressora (apenas suportado no FastReport clássico)
        /// </summary>
        public bool SegundaViaContingencia { get; set; }

        // ── Parâmetros herdados do layout NF-e padrão ────────────────────────────────────────

        public bool DuasLinhas { get; set; }

        public bool QuebrarLinhasObservacao { get; set; }

        public bool ExibeCampoFatura { get; set; }

        public bool ExibirResumoCanhoto { get; set; }

        public bool ExibeRetencoes { get; set; }

        public string ResumoCanhoto { get; set; }

        public string ChaveContingencia { get; set; }

        public bool ImprimirISSQN { get; set; }

        public bool ImprimirDescPorc { get; set; }

        public bool ImprimirTotalLiquido { get; set; }

        public ImprimirUnidQtdeValor ImprimirUnidQtdeValor { get; set; }

        public bool ExibirTotalTributos { get; set; }

        public int DecimaisValorUnitario { get; set; }

        public int DecimaisQuantidadeItem { get; set; }

        public DateTime? DataHoraImpressao { get; set; }
    }
}
