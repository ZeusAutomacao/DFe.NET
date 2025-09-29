using DadosDeTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valor = DFe.Classes.Valor;

namespace DFe.Testes.Valores
{
    [TestClass]
    public class ValorTesteUnitario
    {
        // Normativa ABNT NBR5891
        [TestMethod(displayName: "Dado valor para arredondamento, quando o arredondar, então deve retornar o valor arredondado seguindo as normas da ABNT.")]
        [DynamicData(nameof(ValorDadosDeTeste.ObterValoresParaArredondamentoSegundoNormativaAbnt), typeof(ValorDadosDeTeste), DynamicDataSourceType.Method)]
        public void DadoValorParaArredondamentoQuandoArredondarEntaoDeveRetornarOValorArredondadoSeguindoAsNormasDaAbnt(decimal valor, decimal valorEsperado)
        {
            var casasDecimaisParaArredondamento = 2;
            var valorArredondadoDfe = Valor.Arredondar(valor,casasDecimaisParaArredondamento);

            Assert.AreEqual(valorArredondadoDfe, valorEsperado);
        }
        
        [TestMethod(displayName: "Dado valor para arredondamento nulo, quando o arredondar, então deve retornar nulo.")]
        public void DadoValorParaArredondamentoQuandoArredondarEntaoOValorArredondadoDeveSeguirAsNormasDaAbnt()
        {
            var casasDecimaisParaArredondamento = 2;
            var valorArredondadoDfe = Valor.Arredondar(null,casasDecimaisParaArredondamento);

            Assert.IsNull(valorArredondadoDfe);
        }
    }
}
