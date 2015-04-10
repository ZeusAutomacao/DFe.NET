using System;

namespace NFe.Classes
{
    public static class Valor
    {
        public static decimal Arredondar(decimal valor, int casasDecimais)
        {
            return Decimal.Round(valor, casasDecimais);
        }

        public static decimal? Arredondar(decimal? valor, int casasDecimais)
        {
            return valor.HasValue ? decimal.Round(valor.Value, casasDecimais) : (decimal?) null;
        }
    }
}
