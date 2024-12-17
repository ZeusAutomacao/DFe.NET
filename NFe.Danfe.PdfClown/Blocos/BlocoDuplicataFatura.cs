using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Modelo;

namespace NFe.Danfe.PdfClown.Blocos
{
    internal class BlocoDuplicataFatura : BlocoBase
    {

        public BlocoDuplicataFatura(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var de = viewModel.Duplicatas.Select(x => new Duplicata(estilo, x)).ToList();
            var eh = de.First().Height;

            int numeroElementosLinha = ViewModel.Orientacao == Orientacao.Paisagem ? 7 : 6;

            int i = 0;

            while (i < de.Count)
            {
                FlexibleLine fl = new FlexibleLine(Width, eh);

                int i2;
                for (i2 = 0; i2 < numeroElementosLinha && i < de.Count; i2++, i++)
                {
                    fl.ComElemento(de[i]);
                }

                for (; i2 < numeroElementosLinha; i2++)
                    fl.ComElemento(new ElementoVazio());


                fl.ComLargurasIguais();
                MainVerticalStack.Add(fl);
            }

        }

        public override string Cabecalho => "Fatura / Duplicata";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
    }
}
