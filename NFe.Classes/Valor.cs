using System;
using System.Globalization;

namespace NFe.Classes
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

        public static decimal ArredondarParaBaixo(this decimal valor, int casasDecimais)
        {
            var divisor = (decimal)Math.Pow(10, casasDecimais);
            var dividendo = (long)Math.Truncate(divisor * valor);
            return dividendo / divisor;
        }
    }
}
