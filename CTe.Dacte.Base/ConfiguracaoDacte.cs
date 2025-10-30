using System.Drawing;
using System.IO;

namespace CTe.Dacte.Base
{
    public class ConfiguracaoDacte
    {
        public ConfiguracaoDacte()
        {
            this.Logomarca = null;
            this.DocumentoCancelado = false;
            this.QuebrarLinhasObservacao = false;
        }

        /// <summary>
        /// Logomarca do emitente a ser impressa no DACTe do CTe
        /// </summary>
        public byte[] Logomarca { get; set; }

        /// <summary>
        /// Determina se deve ser impresso uma tarja "DOCUMENTO CANCELADO", indicando que o DACTe impresso refere-se a um CTe cancelado
        /// </summary>
        public bool DocumentoCancelado { get; set; }

        /// <summary>
        /// Informa o nome ou site do desenvolvedor do DACTe
        /// </summary>
        public string Desenvolvedor { get; set; }

        /// <summary>
        /// Substitui ; (ponto e virgula) por quebra de linha no DACTe
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