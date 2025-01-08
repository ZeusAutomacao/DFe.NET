using DFe.Testes.Valores.DadosDeTeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes;

namespace DFe.Testes.Valores
{
    [TestClass]
    public class ValorTesteUnitario
    {
        [TestMethod]
        [DynamicData(nameof(ValorDadosDeTeste.ObterValoresDecimaisParaArredondar), typeof(ValorDadosDeTeste), DynamicDataSourceType.Method)]
        public void Teste(decimal valor, int casasDecimais)
        {
            var valorArredondadoNfe = Classes.Valor.Arredondar(valor, casasDecimais);
            var valorArredondadoDfe = Valor.Arredondar(valor, casasDecimais);

            Assert.AreEqual(valorArredondadoNfe, valorArredondadoDfe);
        }
    }
}
