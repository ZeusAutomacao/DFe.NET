using System.Drawing;
using System.IO;

namespace MDFe.Damdfe.Base
{
    public class ConfiguracaoDamdfe
    {
        public ConfiguracaoDamdfe()
        {
            this.Logomarca = null;
            this.DocumentoCancelado = false;
            this.DocumentoEncerrado = false;
            this.QuebrarLinhasObservacao = false;
        }

        /// <summary>
        /// Logomarca do emitente a ser impressa no DAMDFe do MDFe
        /// </summary>
        public byte[] Logomarca { get; set; }

        /// <summary>
        /// Determina se deve ser impresso uma tarja "DOCUMENTO CANCELADO", indicando que o DAMDFe impresso refere-se a um MDFe cancelado
        /// </summary>
        public bool DocumentoCancelado { get; set; }

        /// <summary>
        /// Determina se deve ser impresso uma tarja "DOCUMENTO ENCERRADO", indicando que o DAMDFe impresso refere-se a um MDFe já encerrado
        /// </summary>
        public bool DocumentoEncerrado { get; set; }

        /// <summary>
        /// Informa o nome ou site do desenvolvedor do DAMDFe
        /// </summary>
        public string Desenvolvedor { get; set; }

        /// <summary>
        /// Substitui ; (ponto e virgula) por quebra de linha no DAMDFe
        /// </summary>
        public bool QuebrarLinhasObservacao { get; set; }

        /// <summary>
        /// Retorna um objeto do tipo Image a partir da logo armazenada na propriedade Logomarca 
        /// </summary>
        /// <returns></returns>
        public Image ObterLogo()
        {
            if (Logomarca == null)
                return null;
            var ms = new MemoryStream(Logomarca);
            var image = Image.FromStream(ms);
            return image;
        }
    }
}