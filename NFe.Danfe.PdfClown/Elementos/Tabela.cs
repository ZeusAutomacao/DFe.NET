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

        public Tabela ComColuna(float larguraP, AlinhamentoHorizontal ah, params string[] cabecalho)
        {
            Colunas.Add(new TabelaColuna(cabecalho, larguraP, ah));
            return this;
        }

        public void AdicionarLinha(List<string> linha)
        {
            if (linha.Count != Colunas.Count) throw new ArgumentException(nameof(linha));
            Linhas.Add(linha);
        }

        public void AjustarLarguraColunas()
        {
            var sw = Colunas.Sum(x => x.PorcentagemLargura);

            if (sw > 100F) throw new InvalidOperationException();

            var w = (100F - sw) / (float)Colunas.Where(x => x.PorcentagemLargura == 0).Count();

            foreach (var c in Colunas.Where(x => x.PorcentagemLargura == 0))
                c.PorcentagemLargura = w;

        }

        private Boolean DesenharLinha(Gfx gfx)
        {
            float x = X;
            _DY1 = _DY;

            TextBlock[] tb = new TextBlock[Colunas.Count];

            for (int i = 0; i < Colunas.Count; i++)
            {
                var c = Colunas[i];
                var v = Linhas[LinhaAtual][i];

                float w = (Width * c.PorcentagemLargura) / 100F;

                if (!string.IsNullOrWhiteSpace(v))
                {

                    tb[i] = new TextBlock(v, FonteCorpo)
                    {
                        Width = w - 2F * Estilo.PaddingHorizontal,
                        X = x + PaddingHorizontal,
                        Y = _DY + PaddingSuperior,
                        AlinhamentoHorizontal = c.AlinhamentoHorizontal
                    };
                }

                x += w;

            }

            var tbm = tb.Where(t => t != null).Max(t => t.Height);
            if (tbm + _DY + PaddingInferior + PaddingSuperior > BoundingBox.Bottom) return false;

            for (int i = 0; i < Colunas.Count; i++)
            {
                if (tb[i] != null)
                    tb[i].Draw(gfx);
            }

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
