using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Validacao;
using System;
using Xunit;

namespace NFe.Integracao.Testes
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            Validador.Valida(ServicoNFe.NFeAutorizacao, VersaoServico.ve100, null, false, "C:\\works\\nfe-products-api\\schemas");

        }
    }
}
