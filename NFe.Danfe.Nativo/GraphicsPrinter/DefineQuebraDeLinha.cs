using System.Collections.Generic;
using System.Drawing;

namespace NFe.Danfe.Nativo.GraphicsPrinter
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

            string linha = _adicionarTexto.Texto.Replace("\n", " ").Replace("\r", "");
            string[] palavras = linha.Split(' ');
            string linhaFormat = string.Empty;

            Dictionary<int, string> partesDaLinha = new Dictionary<int, string>();
            string parte = string.Empty;

            int parteContador = 0;

            foreach (string palavra in palavras)
            {
                Medida medidaParte = MedidasLinha.GetMedidas(parte, _adicionarTexto.Fonte);
                Medida medidaPalavra = MedidasLinha.GetMedidas(palavra, _adicionarTexto.Fonte);
                int larguraLinha = medidaParte.Largura + medidaPalavra.Largura;

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

            int qtdQuebras = 0;
            foreach (KeyValuePair<int, string> item in partesDaLinha)
            {
                linhaFormat += item.Value;
                if (qtdQuebras >= partesDaLinha.Count - 1) continue;

                linhaFormat += "\n";
                qtdQuebras++;
            }

            SolidBrush br = CriaSolidBrushParaQuebrarLinhas();
            return new AdicionarTexto(graphics, linhaFormat, _adicionarTexto.TamanhoFonte, br);
        }

        private static SolidBrush CriaSolidBrushParaQuebrarLinhas()
        {
            SolidBrush br = new SolidBrush(SystemColors.ControlText) {Color = Color.Black};
            return br;
        }
    }
}