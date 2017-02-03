using System;
using System.Drawing;

namespace GraphicsPrinter
{
    internal class MedidasLinha
    {
        public static Medida GetMedidas(AdicionarTexto adicionarTexto)
        {
            var medida = GetMedidas(adicionarTexto.Texto, adicionarTexto.Fonte);

            return medida;
        }

        public static Medida GetMedidas(string texto, Font fonte)
        {
            var g = Graphics.FromHwnd(IntPtr.Zero);
            var tamanhoDaString = g.MeasureString(texto, fonte);
            var alturaLinha = Convert.ToInt32(tamanhoDaString.Height);
            var larguraLinha = Convert.ToInt32(tamanhoDaString.Width);

            return new Medida(alturaLinha, larguraLinha);
        }

    }

    public class Medida
    {
        public Medida(int altura, int largura)
        {
            Altura = altura;
            Largura = largura;
        }

        public int Altura { get; private set; }
        public int Largura { get; private set; }
    }
}