using System.Drawing;

namespace GraphicsPrinter
{
    public class LinhaHorizontal
    {
        private readonly Graphics _graphics;
        private readonly Pen _cor;
        private readonly float _x;
        private readonly float _y;
        private readonly float _x2;
        private readonly float _y2;

        public LinhaHorizontal(Graphics graphics, Pen cor, float x, float y, float x2, float y2)
        {
            _graphics = graphics;
            _cor = cor;
            _x = x;
            _y = y;
            _x2 = x2;
            _y2 = y2;
        }

        public void Desenhar()
        {
            _graphics.DrawLine(_cor, _x, _y, _x2, _y2);
        }
    }
}