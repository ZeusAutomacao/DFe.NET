using NFe.Classes.Informacoes;
using Shared.NFe.Utils.InfRespTec;
using Xunit;

namespace NFe.Utils.Testes;

public class GerarHashCsrtTesteUnitario
{
    [Fact(DisplayName = "Dado o Id da NFe, quando gerar o hash CSRT, então deve usar a chave de 44 posições.")]
    public void DadoIdDaNfeQuandoGerarHashCsrtEntaoDeveUsarAChave()
    {
        var chave = new string('1', 44);
        var csrt = "CSRTTESTE1234567890";
        var nfe = new global::NFe.Classes.NFe
        {
            infNFe = new infNFe
            {
                Id = "NFe" + chave
            }
        };

        var hashDoDocumento = GerarHashCSRT.HashCSRT(csrt, nfe);
        var hashDaChave = GerarHashCSRT.HashCSRT(csrt, chave);

        Assert.Equal(hashDaChave, hashDoDocumento);
    }
}
