using System.Drawing;
using System.IO;

namespace NFe.Impressao.NFCe
{
    public class ConfiguracaoDanfeNfce
    {

        public ConfiguracaoDanfeNfce(NfceDetalheVendaNormal detalheVendaNormal, NfceDetalheVendaContigencia detalheVendaContigencia, string cIdToken, string csc, byte[] logomarca = null)
        {
            DetalheVendaNormal = detalheVendaNormal;
            DetalheVendaContigencia = detalheVendaContigencia;
            Logomarca = logomarca;
            this.cIdToken = cIdToken;
            CSC = csc;
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização
        /// </summary>
        private ConfiguracaoDanfeNfce()
        {
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
        /// Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ
        /// </summary>
        public string cIdToken { get; set; }

        /// <summary>
        /// Código de Segurança do Contribuinte(antigo Token)
        /// </summary>
        public string CSC { get; set; }

        /// <summary>
        /// Logomarca do emitente a ser impressa no DANFE da NFCe
        /// </summary>
        public byte[] Logomarca { get; set; }

        /// <summary>
        /// Retorna um objeto do tipo Image a partir da logo armazenada na propriedade Logomarca 
        /// </summary>
        /// <returns></returns>
        public Image ObterLogo()
        {
            if (Logomarca == null)
                return null;
            using (var ms = new MemoryStream(Logomarca, 0, Logomarca.Length))
            {
                var image = Image.FromStream(ms, true);
                return image;
            }
        }
    }
}
