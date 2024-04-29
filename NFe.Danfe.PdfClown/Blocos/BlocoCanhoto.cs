using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Modelo;
using NFe.Danfe.PdfClown.Tools;

namespace NFe.Danfe.PdfClown.Blocos
{
    internal class BlocoCanhoto : BlocoBase
    {
        public const float TextoRecebimentoAltura = 10;
        public const float AlturaLinha2 = 9;

        public BlocoCanhoto(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var textoRecebimento = new TextoSimples(estilo, viewModel.TextoRecebimento) { Height = TextoRecebimentoAltura, TamanhoFonte = 8 };
            var nfe = new NumeroNfSerie(estilo, viewModel.NfNumero.ToString(Formatador.FormatoNumeroNF), viewModel.NfSerie.ToString()) { Height = AlturaLinha2 + TextoRecebimentoAltura, Width = 30 };

            var campos = new LinhaCampos(Estilo) { Height = AlturaLinha2 }
               .ComCampo("Data de Recebimento", null)
               .ComCampo("Identificação e assinatura do recebedor", null)
               .ComLarguras(50, 0);

            var coluna1 = new VerticalStack();
            coluna1.Add(textoRecebimento, campos);

            var linha = new FlexibleLine() { Height = coluna1.Height }
            .ComElemento(coluna1)
            .ComElemento(nfe)
            .ComLarguras(0, 16);

            MainVerticalStack.Add(linha, new LinhaTracejada(2));

        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

    }
}
