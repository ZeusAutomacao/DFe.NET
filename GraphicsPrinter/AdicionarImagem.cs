using System.Drawing;

namespace GraphicsPrinter
{
    public class AdicionarImagem
    {
        private readonly Graphics _graphics;
        private readonly string _caminhoImagem;
        private readonly int _posicaoX;
        private readonly int _posicaoY;
        private readonly Image _imagem;

        public AdicionarImagem(Graphics graphics, string caminhoImagem, int posicaoX, int posicaoY)
        {
            _graphics = graphics;
            _caminhoImagem = caminhoImagem;
            _posicaoX = posicaoX;
            _posicaoY = posicaoY;
        }

        public AdicionarImagem(Graphics graphics, Image imagem, int posicaoX, int posicaoY)
        {
            _graphics = graphics;
            _imagem = imagem;
            _posicaoX = posicaoX;
            _posicaoY = posicaoY;
        }

        public void Desenhar()
        {
            try
            {
                var logo = _imagem ?? Image.FromFile(_caminhoImagem);
                _graphics.DrawImage(logo, new Point(_posicaoX, _posicaoY));
            }
            catch
            {
            }
        } 
    }
}