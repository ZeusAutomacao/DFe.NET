using DFe.Testes.Valores.DadosDeTeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes;

namespace DFe.Testes.Valores
{
    [TestClass]
    public class ValorTesteUnitario
    {
        [TestMethod(displayName: "Dado um valor e uma quantidade de casas decimais, quando o arredondamento for feito utilizando os métodos da DFe e NFe, então o valor arredondado deve ser igual em ambos os casos")]
        [DynamicData(nameof(ValorDadosDeTeste.ObterValoresDecimaisParaArredondar), typeof(ValorDadosDeTeste), DynamicDataSourceType.Method)]
        public void DadoUmValorEUmaQuantidadeDeCasasDecimaisQuandoOArredondamentoForFeitoUtilizandoOsMetodosDaDfeENfeEntaoOValorArredondadoDeveSerIgualEmAmbosOsCasos(decimal valor, int casasDecimais)
        {
            var valorArredondadoDfe = Classes.Valor.Arredondar(valor, casasDecimais);
            var valorArredondadoNfe = Valor.Arredondar(valor, casasDecimais);

            Assert.AreEqual(valorArredondadoDfe, valorArredondadoNfe);
        }
    }
}
