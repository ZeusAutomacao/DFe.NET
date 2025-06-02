using System.Security.Cryptography;
using System.Text;
using DadosDeTestes.AssinaturaDigital;
using DFe.Utils.Assinatura;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DFe.Testes.Utils.Assinatura;

[TestClass]
public class AssinaturaDigitalTesteUnitario
{
    [TestMethod(displayName: "Dado dados para geração do hash sha1 bytes, quando obter hash sha1 bytes, então deve obter hash sha1 em bytes igual do SHA1CryptoServiceProvider.")]
    [DynamicData(nameof(AssinaturaDigitalTesteDados.ObterDadosParaGeracaoDoHashSha1Bytes), typeof(AssinaturaDigitalTesteDados), DynamicDataSourceType.Method)]
    public void DadoDadosParaGeracaoDoHashSha1BytesQuandoObterHashSha1BytesEntaoDeveObterHashSha1EmBytesIgualDoSha1CryptoServiceProvider(string dadosEmString)
    {
        // Arrange
        var dadosEmBytes = Encoding.UTF8.GetBytes(dadosEmString);
        var bytesEsperados = ObterHashSha1StringUsandoSha1CryptoServiceProvider(dadosEmBytes);

        // Act
        var bytesRetornado = AssinaturaDigital.ObterHashSha1Bytes(dadosEmBytes);

        // Assert
        CollectionAssert.AreEqual(bytesEsperados, bytesRetornado);
    }

    private byte[] ObterHashSha1StringUsandoSha1CryptoServiceProvider(byte[] dados)
    {
        var sha1 = new SHA1CryptoServiceProvider();
        var hashSha1Bytes = sha1.ComputeHash(dados);

        return hashSha1Bytes;
    }
}