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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GraphicsPrinter
{
    public class RedimensionaImagemPara
    {
        private readonly AdicionarImagem _adicionarImagem;
        private readonly int _largura;
        private readonly int _altura;

        public RedimensionaImagemPara(AdicionarImagem adicionarImagem, int largura, int altura)
        {
            _adicionarImagem = adicionarImagem;
            _largura = largura;
            _altura = altura;
        }

        public void Desenhar()
        {
            Image logo = _adicionarImagem.Logo;

            if (logo.Size.Width != 50 || logo.Size.Height != 24)
            {
                logo = Redimensionar(logo, _largura, _altura);
            }

            new AdicionarImagem(_adicionarImagem, logo).Desenhar();
        }

        /// <summary>
        /// Redimensione a imagem para a largura e altura especificadas.
        /// </summary>
        /// <param name="logo">A imagem para redimensionar.</param>
        /// <param name="largura">A largura para redimensionar para.</param>
        /// <param name="altura">A altura para redimensionar para.</param>
        /// <returns>A imagem redimensionada.</returns>
        private static Bitmap Redimensionar(Image logo, int largura, int altura)
        {
            var destRect = new Rectangle(0, 0, largura, altura);
            var destImage = new Bitmap(largura, altura);

            destImage.SetResolution(logo.HorizontalResolution, logo.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(logo, destRect, 0, 0, logo.Width, logo.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}