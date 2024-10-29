using NFe.Utils;

namespace NFe.Danfe.Base.NFCe
{
    public class ConfiguracaoDanfeNfce: ConfiguracaoDanfe
    {
        public ConfiguracaoDanfeNfce(NfceDetalheVendaNormal detalheVendaNormal,
            NfceDetalheVendaContigencia detalheVendaContigencia, byte[] logomarca = null,
            bool imprimeDescontoItem = false, float margemEsquerda = 4.5F, float margemDireita = 4.5F, 
            NfceModoImpressao modoImpressao = NfceModoImpressao.MultiplasPaginas,
            bool documentoCancelado = false, NfceLayoutQrCode nfceLayoutQrCode = NfceLayoutQrCode.Abaixo, VersaoQrCode versaoQrCode = VersaoQrCode.QrCodeVersao1)
        {
            DocumentoCancelado = documentoCancelado;
            DetalheVendaNormal = detalheVendaNormal;
            DetalheVendaContigencia = detalheVendaContigencia;
            Logomarca = logomarca;
            ImprimeDescontoItem = imprimeDescontoItem;
            MargemEsquerda = margemEsquerda;
            MargemDireita = margemDireita;
            ModoImpressao = modoImpressao;
            NfceLayoutQrCode = nfceLayoutQrCode;
            VersaoQrCode = versaoQrCode;
            SegundaViaContingencia = true;
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização
        /// </summary>
        public ConfiguracaoDanfeNfce()
        {
            DocumentoCancelado = false;
        }

        /// <summary>
        /// Modo de impressão do detalhe (produtos) para NFCes emitidos em ambiente Normal
        /// </summary>
        public NfceDetalheVendaNormal DetalheVendaNormal { get; set; }

        /// <summary>
        /// Modo de impressão do detalhe (produtos) para NFCes emitidos em ambiente de Homologação
        /// Nesse modo a informação do detalhe é obrigatória. Vide Manual de Padrões Padrões Técnicos do DANFE-NFC-e e QR Code, versão 3.2
        /// </summary>
        public NfceDetalheVendaContigencia DetalheVendaContigencia { get; set; }

        /// <summary>
        /// Determina se o desconto do item será impresso no DANTE, quando houver
        /// </summary>
        public bool ImprimeDescontoItem { get; set; }

        /// <summary>
        /// Determina se o número de telefone do emitente será impresso no danfe
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
        /// Determina o modo de impressão do DANFE da NFCe.
        /// 
        /// </summary>
        public NfceModoImpressao ModoImpressao { get; set; }

        /// <summary>
        /// Determina se o QRCode do Nfce será impresso ao lado ou abaixo dos dados do consumidor 
        /// </summary>
        public NfceLayoutQrCode NfceLayoutQrCode { get; set; }

        /// <summary>
        /// Versão do QRCode da NFCe. 1.0 ou 2.0
        /// </summary>
        public VersaoQrCode VersaoQrCode { get; set; }

        /// <summary>
        /// Envia segunda via de contingencia para a impressora (apenas suportado no fastreport clássico)
        /// </summary>
        public bool SegundaViaContingencia { get; set; }
    }
}