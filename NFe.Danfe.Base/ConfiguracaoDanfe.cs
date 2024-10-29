namespace NFe.Danfe.Base
{
    public class ConfiguracaoDanfe
    {
        /// <summary>
        /// Logomarca do emitente a ser impressa no DANFE da NFCe
        /// </summary>
        public byte[] Logomarca { get; set; }

        /// <summary>
        /// Determina se deve ser impresso uma tarja "DOCUMENTO CANCELADO", indicando que o DANFE impresso refere-se ao DANFE de uma NFe cancelada
        /// </summary>
        public bool DocumentoCancelado { get; set; }
    }
}