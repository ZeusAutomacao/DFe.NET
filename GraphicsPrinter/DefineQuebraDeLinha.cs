using System.Collections.Generic;
using System.Drawing;

namespace GraphicsPrinter
{
    public class DefineQuebraDeLinha
    {
        private readonly AdicionarTexto _adicionarTexto;
        private readonly ComprimentoMaximo _comprimentoMaximo;
        private readonly int _larguraDoTexto;

        public DefineQuebraDeLinha(AdicionarTexto adicionarTexto, ComprimentoMaximo comprimentoMaximo, int larguraDoTexto)
        {
            _adicionarTexto = adicionarTexto;
            _comprimentoMaximo = comprimentoMaximo;
            _larguraDoTexto = larguraDoTexto;
        }

        public AdicionarTexto DesenharComQuebras(Graphics graphics)
        {
            if (_larguraDoTexto <= _comprimentoMaximo.GetComprimentoMaximo()) return _adicionarTexto;

            var linha = _adicionarTexto.Texto.Replace("\n", " ").Replace("\r", "");
            var palavras = linha.Split(' ');
            var linhaFormat = string.Empty;

            var partesDaLinha = new Dictionary<int, string>();
            var parte = string.Empty;

            var parteContador = 0;

            foreach (var palavra in palavras)
            {
                var medidaParte = MedidasLinha.GetMedidas(parte, _adicionarTexto.Fonte);
                var medidaPalavra = MedidasLinha.GetMedidas(palavra, _adicionarTexto.Fonte);
                var larguraLinha = medidaParte.Largura + medidaPalavra.Largura;

                if (larguraLinha < _comprimentoMaximo.GetComprimentoMaximo())
                {
                    parte += string.IsNullOrEmpty(parte) ? palavra : " " + palavra;
                }
                else
                {
                    partesDaLinha.Add(parteContador, parte);
                    parte = palavra;
                    parteContador++;
                }
            }
            partesDaLinha.Add(parteContador, parte);

            var qtdQuebras = 0;
            foreach (var item in partesDaLinha)
            {
                linhaFormat += item.Value;
                if (qtdQuebras >= partesDaLinha.Count - 1) continue;

                linhaFormat += "\n";
                qtdQuebras++;
            }

            var br = CriaSolidBrushParaQuebrarLinhas();
            return new AdicionarTexto(graphics, linhaFormat, _adicionarTexto.TamanhoFonte, br);
        }

        private static SolidBrush CriaSolidBrushParaQuebrarLinhas()
        {
            var br = new SolidBrush(SystemColors.ControlText) {Color = Color.Black};
            return br;
        }
    }
}