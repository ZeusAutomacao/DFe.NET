using System;
using System.Globalization;

namespace DFe.Classes
{
    public static class Valor
    {
        public static decimal Arredondar(this decimal valor, int casasDecimais)
        {
            var valorArredondado = decimal.Round(valor, casasDecimais, MidpointRounding.ToEven);
            var valorArredondadoFormatado = valorArredondado.ToString("F" + casasDecimais, CultureInfo.CurrentCulture);
            return decimal.Parse(valorArredondadoFormatado);
        }

        public static decimal? Arredondar(this decimal? valor, int casasDecimais)
        {
            if (valor == null) return null;
            return Arredondar(valor.Value, casasDecimais);
        }
    }
}
