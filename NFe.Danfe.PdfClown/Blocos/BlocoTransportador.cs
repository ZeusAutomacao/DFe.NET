using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Modelo;
using NFe.Danfe.PdfClown.Tools;

namespace NFe.Danfe.PdfClown.Blocos
{
    internal class BlocoTransportador : BlocoBase
    {
        public const float LarguraCampoPlacaVeiculo = 22F * Proporcao;
        public const float LarguraCampoCodigoAntt = 30F * Proporcao;
        public const float LarguraCampoCnpj = 31F * Proporcao;
        public const float LarguraCampoUf = 7F * Proporcao;
        public const float LarguraFrete = 34F * Proporcao;

        public BlocoTransportador(DanfeViewModel viewModel, Estilo campoEstilo) : base(viewModel, campoEstilo)
        {
            var transportadora = viewModel.Transportadora;

            AdicionarLinhaCampos()
                .ComCampo(Strings.RazaoSocial, transportadora.RazaoSocial)
                .ComCampo("Frete", transportadora.ModalidadeFreteString, AlinhamentoHorizontal.Centro)
                .ComCampo("Código ANTT", transportadora.CodigoAntt, AlinhamentoHorizontal.Centro)
                .ComCampo("Placa do Veículo", transportadora.Placa, AlinhamentoHorizontal.Centro)
                .ComCampo(Strings.UF, transportadora.VeiculoUf, AlinhamentoHorizontal.Centro)
                .ComCampo(Strings.CnpjCpf, Formatador.FormatarCnpj(transportadora.CnpjCpf), AlinhamentoHorizontal.Centro)
                .ComLarguras(0, LarguraFrete, LarguraCampoCodigoAntt, LarguraCampoPlacaVeiculo, LarguraCampoUf, LarguraCampoCnpj);

            AdicionarLinhaCampos()
                .ComCampo(Strings.Endereco, transportadora.EnderecoLogadrouro)
                .ComCampo(Strings.Municipio, transportadora.Municipio)
                .ComCampo(Strings.UF, transportadora.EnderecoUf, AlinhamentoHorizontal.Centro)
                .ComCampo(Strings.InscricaoEstadual, transportadora.Ie, AlinhamentoHorizontal.Centro)
                .ComLarguras(0, LarguraCampoPlacaVeiculo + LarguraCampoCodigoAntt, LarguraCampoUf, LarguraCampoCnpj);

            var l = (float)(LarguraCampoCodigoAntt + LarguraCampoPlacaVeiculo + LarguraCampoUf + LarguraCampoCnpj) / 3F;

            AdicionarLinhaCampos()
                .ComCampoNumerico(Strings.Quantidade, transportadora.QuantidadeVolumes, 3)
                .ComCampo("Espécie", transportadora.Especie)
                .ComCampo("Marca", transportadora.Marca)
                .ComCampo("Numeração", transportadora.Numeracao)
                .ComCampoNumerico("Peso Bruto", transportadora.PesoBruto, 3)
                .ComCampoNumerico("Peso Líquido", transportadora.PesoLiquido, 3)
                .ComLarguras(20F / 200F * 100, 0, 0, l, l, l);

        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override string Cabecalho => "Transportador / Volumes Transportados";
    }
}
