using DadosDeTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DFeArredondar = DFe.Classes.Valor;

namespace NFe.Classes.Testes;

[TestClass]
public class ValorTesteUnitario
{
    [TestMethod(displayName: "Dado um valor e uma quantidade de casas decimais, quando o arredondamento for feito utilizando os métodos da DFe e NFe, então o valor arredondado deve ser igual em ambos os casos.")]
    [DynamicData(nameof(ValorDadosDeTeste.ObterValoresDecimaisParaArredondar), typeof(ValorDadosDeTeste), DynamicDataSourceType.Method)]
    public void DadoUmValorEUmaQuantidadeDeCasasDecimaisQuandoOArredondamentoForFeitoUtilizandoOsMetodosDaDfeENfeEntaoOValorArredondadoDeveSerIgualEmAmbosOsCasos(decimal valor, int casasDecimais)
    {
        var valorArredondadoDfe = DFeArredondar.Arredondar(valor,casasDecimais);
        var valorArredondadoNfe = valor.Arredondar(casasDecimais);

        Assert.AreEqual(valorArredondadoDfe, valorArredondadoNfe);
    }
        
    // Normativa ABNT NBR5891
    [TestMethod(displayName: "Dado valor para arredondamento, quando o arredondar, então deve retornar o valor arredondado seguindo as normas da ABNT.")]
    [DynamicData(nameof(ValorDadosDeTeste.ObterValoresParaArredondamentoSegundoNormativaAbnt), typeof(ValorDadosDeTeste), DynamicDataSourceType.Method)]
    public void DadoValorParaArredondamentoQuandoArredondarEntaoDeveRetornarOValorArredondadoSeguindoAsNormasDaAbnt(decimal valor, decimal valorEsperado)
    {
        var casasDecimaisParaArredondamento = 2;
        var valorArredondadoNfe = valor.Arredondar(casasDecimaisParaArredondamento);

        Assert.AreEqual(valorArredondadoNfe, valorEsperado);
    }
    
    [TestMethod(displayName: "Dado valor para arredondamento, quando o arredondar para baixo, então deve retornar o valor arredondado para baixo.")]
    [DynamicData(nameof(ValorDadosDeTeste.ObterValoresParaArredondamentoParaBaixo), typeof(ValorDadosDeTeste), DynamicDataSourceType.Method)]
    public void DadoValorParaArredondamentoQuandoArredondarParaBaixoEntaoDeveRetornarOValorArredondadoParaBaixo(decimal valor, int casasDecimais, decimal valorEsperado)
    {
        var valorArredondadoParaBaixoNfe = valor.ArredondarParaBaixo(casasDecimais);

        Assert.AreEqual(valorArredondadoParaBaixoNfe, valorEsperado);
    }
}