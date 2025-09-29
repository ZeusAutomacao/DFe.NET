namespace DadosDeTestes
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
                new object[] { 4.77575m, 4 },
                new object[] { 5.445545m, 5 }
            };
        }
        
        public static IEnumerable<object[]> ObterValoresParaArredondamentoSegundoNormativaAbnt()
        {
            return new List<object[]>
            {
                new object[] { 0.342m, 0.34m },
                new object[] { 0.346m, 0.35m },
                new object[] { 0.3452m, 0.35m },
                new object[] { 0.3450m, 0.34m },
                new object[] { 0.332m, 0.33m },
                new object[] { 0.336m, 0.34m },
                new object[] { 0.3352m, 0.34m },
                new object[] { 0.3350m, 0.34m },
                new object[] { 0.3050m, 0.30m },
                new object[] { 0.3150m, 0.32m }
            };
        }
        
        public static IEnumerable<object[]> ObterValoresParaArredondamentoParaBaixo()
        {
            return new List<object[]>
            {
                new object[] { 123.4567m, 2, 123.45m },
                new object[] { 123.4599m, 2, 123.45m },
                new object[] { 123.451m, 2, 123.45m },
                new object[] { 123.4001m, 3, 123.400m },
                new object[] { 123.0009m, 3, 123.000m },
                new object[] { 123.0m, 2, 123.00m },
                new object[] { 0.9999m, 3, 0.999m },
                new object[] { 0m, 2, 0.00m },
                new object[] { 987.654m, 0, 987m }
            };
        }
    }
}
