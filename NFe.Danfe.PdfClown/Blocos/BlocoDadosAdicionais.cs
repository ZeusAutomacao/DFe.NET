using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;
using NFe.Danfe.PdfClown.Modelo;

namespace NFe.Danfe.PdfClown.Blocos
{
    internal class BlocoDadosAdicionais : BlocoBase
    {
        public const float AlturaMinima = 25;
        private CampoMultilinha _cInfComplementares;
        private FlexibleLine _Linha;
        private Campo _cReservadoFisco;
        public const float InfComplementaresLarguraPorcentagem = 75;

        public BlocoDadosAdicionais(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            _cInfComplementares = new CampoMultilinha("Informações Complementares", ViewModel.TextoAdicional(), estilo);
            _cReservadoFisco = new CampoMultilinha("Reservado ao fisco", ViewModel.TextoAdicionalFisco(), estilo);

            _Linha = new FlexibleLine() { Height = _cInfComplementares.Height }
            .ComElemento(_cInfComplementares)
            .ComElemento(_cReservadoFisco)
            .ComLarguras(InfComplementaresLarguraPorcentagem, 0);

            MainVerticalStack.Add(_Linha);
        }

        public override float Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                // Força o ajuste da altura do InfComplementares
                if (_cInfComplementares != null && _Linha != null)
                {
                    _Linha.Width = value;
                    _Linha.Posicionar();
                    _cInfComplementares.Height = AlturaMinima;
                    _Linha.Height = _cInfComplementares.Height;
                }
            }
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Base;
        public override string Cabecalho => "Dados adicionais";
    }
}
