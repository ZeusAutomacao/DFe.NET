using System.Drawing;

namespace GraphicsPrinter
{
    public class AdicionarTexto
    {
        public static FontFamily FontPadrao { get; set; }
        private readonly Graphics _graphics;
        private float _pontoX;
        private float _pontoY;
        private readonly int _tamanhoFonte;
        private readonly SolidBrush _br;

        public AdicionarTexto(Graphics graphics, string texto, int tamanhoFonte, SolidBrush br = null, Font font = null)
        {
            _graphics = graphics;
            Texto = texto;
            _tamanhoFonte = tamanhoFonte;
            _br = br;
            Fonte = font ?? new Font(FontPadrao, _tamanhoFonte);
            Medida = MedidasLinha.GetMedidas(this);
        }

        public void Desenhar(float pontoX, float pontoY)
        {
            _pontoX = pontoX;
            _pontoY = pontoY;

            if (_br != null)
            {
                _graphics.DrawString(Texto, Fonte, _br, new PointF(_pontoX, _pontoY));
                return;
            }

            _graphics.DrawString(Texto, Fonte, Brushes.Black, new PointF(_pontoX, _pontoY));
        }

        public Medida Medida { get; private set; }

        public string Texto { get; private set; }
        public Font Fonte { get; private set; }

        public int TamanhoFonte => _tamanhoFonte;
    }
}