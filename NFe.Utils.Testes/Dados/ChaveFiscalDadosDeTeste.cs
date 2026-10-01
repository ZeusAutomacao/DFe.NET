using System.Collections.Generic;

namespace NFe.Utils.Testes.Dados;

public static class ChaveFiscalDadosDeTeste
{
    public static IEnumerable<object[]> ObterCaracteresEValores()
    {
        yield return new object[] { '0', 0 };
        yield return new object[] { '1', 1 };
        yield return new object[] { '2', 2 };
        yield return new object[] { '3', 3 };
        yield return new object[] { '4', 4 };
        yield return new object[] { '5', 5 };
        yield return new object[] { '6', 6 };
        yield return new object[] { '7', 7 };
        yield return new object[] { '8', 8 };
        yield return new object[] { '9', 9 };

        yield return new object[] { 'A', 17 };
        yield return new object[] { 'B', 18 };
        yield return new object[] { 'C', 19 };
        yield return new object[] { 'D', 20 };
        yield return new object[] { 'E', 21 };
        yield return new object[] { 'F', 22 };
        yield return new object[] { 'G', 23 };
        yield return new object[] { 'H', 24 };
        yield return new object[] { 'I', 25 };
        yield return new object[] { 'J', 26 };
        yield return new object[] { 'K', 27 };
        yield return new object[] { 'L', 28 };
        yield return new object[] { 'M', 29 };
        yield return new object[] { 'N', 30 };
        yield return new object[] { 'O', 31 };
        yield return new object[] { 'P', 32 };
        yield return new object[] { 'Q', 33 };
        yield return new object[] { 'R', 34 };
        yield return new object[] { 'S', 35 };
        yield return new object[] { 'T', 36 };
        yield return new object[] { 'U', 37 };
        yield return new object[] { 'V', 38 };
        yield return new object[] { 'W', 39 };
        yield return new object[] { 'X', 40 };
        yield return new object[] { 'Y', 41 };
        yield return new object[] { 'Z', 42 };
    }
}
