using NFe.Utils.Enderecos;

namespace NFe.Utils
{
    public static class ConfiguracaoUrls
    {
        static ConfiguracaoUrls()
        {
            FactoryUrl = FactoryUrl.CriaFactoryUrl();
        }

        public static FactoryUrl FactoryUrl { get; set; }
    }
}