using System.Drawing;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;
using NFe.Danfe.PdfClown.Tools;
using org.pdfclown.documents.contents.colorSpaces;

namespace NFe.Danfe.PdfClown.Elementos
{
    internal class Tabela : ElementoBase
    {
        public List<TabelaColuna> Colunas { get; private set; }
        public List<List<string>> Linhas { get; private set; }
        public float PaddingSuperior { get; private set; }
        public float PaddingInferior { get; private set; }
        public float PaddingHorizontal { get; private set; }

        public int LinhaAtual { get; private set; }
        public float TamanhoFonteCabecalho { get; private set; }

        private float _DY;
        private float _DY1;

        public Fonte FonteCorpo { get; private set; }
        public Fonte FonteCabecalho { get; private set; }

        public Tabela(Estilo estilo) : base(estilo)
        {
            Colunas = new List<TabelaColuna>();
            Linhas = new List<List<string>>();
            LinhaAtual = 0;
            TamanhoFonteCabecalho = 6;

            PaddingHorizontal = 0.6F;
            PaddingSuperior = 0.75F;
            PaddingInferior = 0.3F;

            // 7.7.7 Conteúdo dos Campos do Quadro “Dados dos Produtos/Serviços”
            // Deverá ter tamanho mínimo de seis(6) pontos, ou 17 CPP.

            FonteCorpo = estilo.CriarFonteRegular(6F);
            FonteCabecalho = estilo.CriarFonteRegular(6F);
        }

        /// <summary>
        ///     Menor corpo de fonte aceito pelo ajuste automático das colunas de valor. Abaixo dele
        ///     o número deixaria de ser legível no impresso, e o comportamento anterior (quebra de
        ///     linha) é preservado.
        /// </summary>
        private const float TamanhoMinimoDaCelulaAjustada = 4F;

        public Tabela ComColuna(float larguraP, AlinhamentoHorizontal ah, params string[] cabecalho)
        {
            Colunas.Add(new TabelaColuna(cabecalho, larguraP, ah));
            return this;
        }

        /// <summary>
        ///     Acrescenta uma coluna de valor: o conteúdo é indivisível e, se não couber na célula,
        ///     encolhe para caber em uma única linha. Ver <see cref="TabelaColuna.AjustarFonteParaCaber"/>.
        /// </summary>
        public Tabela ComColunaDeValor(float larguraP, AlinhamentoHorizontal ah, params string[] cabecalho)
        {
            Colunas.Add(new TabelaColuna(cabecalho, larguraP, ah, ajustarFonteParaCaber: true));
            return this;
        }

        public void AdicionarLinha(List<string> linha)
        {
            if (linha.Count != Colunas.Count) throw new ArgumentException(nameof(linha));
            Linhas.Add(linha);
        }

        public Tabela ComColunaTributos(float largura, params string[] cabecalho)
        {
            Colunas.Add(new TabelaColuna(cabecalho, largura) { RotulosEValores = true });
            return this;
        }

        public void AjustarLarguraColunas()
        {
            var sw = Colunas.Sum(x => x.PorcentagemLargura);

            if (sw > 100F) throw new InvalidOperationException();

            var w = (100F - sw) / (float)Colunas.Where(x => x.PorcentagemLargura == 0).Count();

            foreach (var c in Colunas.Where(x => x.PorcentagemLargura == 0))
                c.PorcentagemLargura = w;

        }

        /// <summary>
        ///     Fonte de uma célula. Por padrão, a fonte do corpo da tabela. Numa coluna de valor
        ///     (<see cref="TabelaColuna.AjustarFonteParaCaber"/>) cujo conteúdo é indivisível — sem
        ///     espaços — e mais largo que a célula, o corpo da fonte é reduzido o suficiente para o
        ///     conteúdo caber em uma única linha, respeitando
        ///     <see cref="TamanhoMinimoDaCelulaAjustada"/>.
        /// </summary>
        /// <param name="coluna">Coluna da célula.</param>
        /// <param name="valor">Conteúdo da célula.</param>
        /// <param name="larguraUtil">Largura disponível da célula, em milímetros.</param>
        private Fonte FonteDaCelula(TabelaColuna coluna, string valor, float larguraUtil)
        {
            if (!coluna.AjustarFonteParaCaber || larguraUtil <= 0) return FonteCorpo;

            foreach (var caractere in valor)
            {
                if (char.IsWhiteSpace(caractere)) return FonteCorpo;
            }

            float largura = FonteCorpo.MedirLarguraTexto(valor);

            if (largura <= larguraUtil || largura <= 0) return FonteCorpo;

            // A folga de 0.5% evita que o arredondamento da medição encoste o valor na borda.
            float tamanho = FonteCorpo.Tamanho * (larguraUtil / largura) * 0.995F;

            return tamanho < TamanhoMinimoDaCelulaAjustada
                ? FonteCorpo
                : new Fonte(FonteCorpo.FonteInterna, tamanho);
        }

        private Boolean DesenharLinha(Gfx gfx)
        {
            float x = X;
            _DY1 = _DY;

            TextBlock[] tb = new TextBlock[Colunas.Count];
            var camposTributos = new List<TextBlock>();
            float alturaTributos = 0;

            for (int i = 0; i < Colunas.Count; i++)
            {
                var c = Colunas[i];
                var v = Linhas[LinhaAtual][i];

                float w = (Width * c.PorcentagemLargura) / 100F;

                if (c.RotulosEValores)
                {
                    float y = _DY + PaddingSuperior;
                    foreach (var linha in v.Split('\n'))
                    {
                        var campos = linha.Split('\t');
                        int pares = campos.Length / 2;
                        float larguraCelula = w - 2 * PaddingHorizontal;
                        float alturaLinha = FonteCorpo.AlturaLinha;
                        for (int par = 0; par < pares; par++)
                        {
                            float larguraPar = pares == 2 ? larguraCelula * (par == 0 ? 0.56F : 0.44F) : larguraCelula;
                            string rotulo = campos[par * 2];
                            string valor = campos[par * 2 + 1];
                            float inicio = x + PaddingHorizontal + (par == 0 ? 0 : larguraCelula * 0.56F);
                            float larguraRotulo = FonteCorpo.MedirLarguraTexto(rotulo) + 0.7F;
                            float larguraValor = FonteCorpo.MedirLarguraTexto(valor);
                            bool empilhar = larguraRotulo + larguraValor > larguraPar - 1.2F;
                            var titulo = new TextBlock(rotulo, FonteCorpo)
                            {
                                X = inicio, Y = y, Width = larguraPar - 1.2F,
                                AlinhamentoHorizontal = AlinhamentoHorizontal.Esquerda
                            };
                            var numero = new TextBlock(valor, FonteCorpo)
                            {
                                X = inicio + (empilhar ? 0 : larguraRotulo),
                                Y = y + (empilhar ? titulo.Height : 0),
                                Width = larguraPar - 1.2F - (empilhar ? 0 : larguraRotulo),
                                AlinhamentoHorizontal = AlinhamentoHorizontal.Direita
                            };
                            camposTributos.Add(titulo);
                            if (!string.IsNullOrEmpty(valor)) camposTributos.Add(numero);
                            alturaLinha = Math.Max(alturaLinha, numero.Y - y + numero.Height);
                        }
                        y += alturaLinha + 0.5F;
                    }
                    alturaTributos = Math.Max(alturaTributos, y - (_DY + PaddingSuperior));
                    x += w;
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(v))
                {
                    float larguraUtil = w - 2F * Estilo.PaddingHorizontal;

                    tb[i] = new TextBlock(v, FonteDaCelula(c, v, larguraUtil))
                    {
                        Width = larguraUtil,
                        X = x + PaddingHorizontal,
                        Y = _DY + PaddingSuperior,
                        AlinhamentoHorizontal = c.AlinhamentoHorizontal
                    };
                }

                x += w;

            }

            var tbm = Math.Max(alturaTributos, tb.Where(t => t != null).Select(t => t.Height).DefaultIfEmpty(0).Max());
            if (tbm + _DY + PaddingInferior + PaddingSuperior > BoundingBox.Bottom) return false;

            for (int i = 0; i < Colunas.Count; i++)
            {
                if (tb[i] != null)
                    tb[i].Draw(gfx);
            }
            foreach (var campo in camposTributos)
                campo.Draw(gfx);

            _DY += Math.Max(tbm, FonteCorpo.AlturaLinha) + PaddingSuperior + PaddingInferior;

            return true;
        }

        public void DesenharCabecalho(Gfx gfx)
        {
            var ml = Colunas.Max(c => c.Cabecalho.Length);
            float ac = ml * FonteCabecalho.AlturaLinha + 2F;

            float x = X;
            _DY = Y;

            foreach (var coluna in Colunas)
            {
                float w = (Width * coluna.PorcentagemLargura) / 100F;
                var r = new RectangleF(x, _DY, w, ac);

                var tb = new TextStack(r.InflatedRetangle(1F));
                tb.AlinhamentoVertical = AlinhamentoVertical.Centro;
                tb.AlinhamentoHorizontal = AlinhamentoHorizontal.Centro;

                foreach (var item in coluna.Cabecalho)
                {
                    tb.AddLine(item, FonteCabecalho);
                }

                tb.Draw(gfx);

                x += w;

                gfx.DrawRectangle(r);
                gfx.DrawRectangle(r.X, BoundingBox.Y, r.Width, BoundingBox.Height);
            }

            _DY += ac;

            gfx.Stroke();
        }


        public override void Draw(Gfx gfx)
        {

            base.Draw(gfx);
            gfx.SetLineWidth(0.25F);

            DesenharCabecalho(gfx);


            while (LinhaAtual < Linhas.Count)
            {
                Boolean r = DesenharLinha(gfx);

                if (r)
                {
                    if (LinhaAtual > 0)
                    {
                        gfx.PrimitiveComposer.BeginLocalState();
                        gfx.PrimitiveComposer.SetStrokeColor(new DeviceRGBColor(0.5, 0.5, 0.5));
                        gfx.PrimitiveComposer.SetLineDash(new org.pdfclown.documents.contents.LineDash(new double[] { 6, 1 }));
                        gfx.PrimitiveComposer.DrawLine(new PointF(BoundingBox.Left, _DY1).ToPointMeasure(), new PointF(BoundingBox.Right, _DY1).ToPointMeasure());
                        gfx.PrimitiveComposer.Stroke();
                        gfx.PrimitiveComposer.End();
                    }

                    LinhaAtual++;
                }
                else
                {
                    break;
                }
            }







        }



        public override bool PossuiContono => false;
    }
}
