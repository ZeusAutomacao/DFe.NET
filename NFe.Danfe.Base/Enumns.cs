namespace NFe.Danfe.Base
{
    public enum NfceDetalheVendaNormal
    {
        NaoImprimir = 0,
        UmaLinha = 1,
        DuasLinhas = 2,
        Completo = 3
    }

    public enum NfceDetalheVendaContigencia
    {
        UmaLinha = 1,
        DuasLinhas = 2,
        Completo = 3
    }

    public enum NfceModoImpressao
    {
        //Imprime o conteúdo em múltiplas páginas
        MultiplasPaginas = 0,

        //Imprime o conteúdo em uma única página, mesmo que o tamanho da página exceda o tamanho pré-definido (A4)
        UnicaPagina = 1
    }

    /// <summary>
    /// Layout de impressão do DANFE:
    /// Abaixo - QRCode abaixo dos dados do cliente; Lateral - QRCode ao lado dos dados do cliente (usa menos papel)
    /// </summary>
    public enum NfceLayoutQrCode
    {
        Abaixo = 0,
        Lateral = 1
    }
}