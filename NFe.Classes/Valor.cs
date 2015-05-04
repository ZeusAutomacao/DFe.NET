using System;
using System.Globalization;

namespace NFe.Classes
{
    public static class Valor
    {
        public static decimal Arredondar(decimal valor, int casasDecimais)
        {
            var valorNovo = Decimal.Round(valor, casasDecimais);
            var valorNovoStr = valorNovo.ToString("F" + casasDecimais, CultureInfo.CurrentCulture);
            return Decimal.Parse(valorNovoStr);
        }

        public static decimal? Arredondar(decimal? valor, int casasDecimais)
        {
            if (valor == null) return null;
            return Arredondar(valor.Value, casasDecimais);
        }
    }
}
