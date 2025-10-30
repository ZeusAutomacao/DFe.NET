using System.Drawing;
using System.IO;

namespace NFe.Danfe.Nativo.GraphicsPrinter
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

        public AdicionarImagem(AdicionarImagem adicionarImagem, Image novaImagem)
        {
            _graphics = adicionarImagem.Graphics;
            _imagem = novaImagem;
            _posicaoX = adicionarImagem.PosicaoX;
            _posicaoY = adicionarImagem.PosicaoY;
        }

        public Image Logo {get { return _imagem; } }
        public Graphics Graphics { get { return _graphics; } }
        public int PosicaoX { get { return _posicaoX; } }
        public int PosicaoY { get { return _posicaoY; } }

        public void Desenhar()
        {
            try
            {
                Image imagemUtilizada = null;

                if (_imagem != null)
                {
                    imagemUtilizada = _imagem;
                }

                if (File.Exists(_caminhoImagem))
                    imagemUtilizada = Image.FromFile(_caminhoImagem);

                // ReSharper disable once AssignNullToNotNullAttribute
                _graphics.DrawImage(imagemUtilizada, new Point(_posicaoX, _posicaoY));
            }
            catch
            {
            }
        } 
    }
}