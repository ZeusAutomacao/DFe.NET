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