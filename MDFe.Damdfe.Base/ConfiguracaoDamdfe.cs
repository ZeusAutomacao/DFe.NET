/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System.Drawing;
using System.IO;

namespace MDFe.Damdfe.Base
{
    public class ConfiguracaoDamdfe
    {
        public ConfiguracaoDamdfe()
        {
            Logomarca = null;
            DocumentoCancelado = false;
            DocumentoEncerrado = false;
            QuebrarLinhasObservacao = false;
            MargemDireita = 6;
            MargemEsquerda = 6;
            MargemSuperior = 8;
            MargemInferior = 8;
        }

        /// <summary>
        /// Margem direita da página em MM
        /// </summary>
        public float MargemDireita { get; set; }

        /// <summary>
        /// Margem esquerda da página em MM
        /// </summary>
        public float MargemEsquerda { get; set; }

        /// <summary>
        /// Margem superior da página em MM
        /// </summary>
        public float MargemSuperior { get; set; }

        /// <summary>
        /// Margem inferior da página em MM
        /// </summary>
        public float MargemInferior { get; set; }

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

        /// <summary>
        /// Retorna um objeto do tipo MemoryStream a partir da logo armazenada na propriedade Logomarca 
        /// </summary>
        /// <returns></returns>
        public MemoryStream ObterLogoMemory()
        {
            if (Logomarca == null)
                return new MemoryStream();
            var ms = new MemoryStream(Logomarca);

            return ms;
        }
    }
}