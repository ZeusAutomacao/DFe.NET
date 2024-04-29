using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Modelo;

namespace NFe.Danfe.PdfClown.Blocos
{
    class BlocoLocalEntrega : BlocoLocalEntregaRetirada
    {
        public BlocoLocalEntrega(DanfeViewModel viewModel, Estilo estilo)
            : base(viewModel, estilo, viewModel.LocalEntrega)
        {
        }

        public override string Cabecalho => "Informações do local de entrega";
    }
}
