using System.Collections.Generic;

namespace DFe.Testes.Valores.DadosDeTeste
{
    public class ValorDadosDeTeste
    {
        public static IEnumerable<object[]> ObterValoresDecimaisParaArredondar()
        {
            return new List<object[]>
            {
                new object[] { 20.35m * 15.90m, 2 },
                new object[] { 0.35m * 15.90m, 2 },
                new object[] { 3.665m, 2 },
                new object[] { 4.775m, 2 },

                new object[] { 20.35m * 15.90m, 3 },
                new object[] { 0.35m * 15.90m, 3 },
                new object[] { 3.665m, 3 },
                new object[] { 4.7757m, 3 },
                new object[] { 5.44555m, 3 },
            };
        }
    }
}
