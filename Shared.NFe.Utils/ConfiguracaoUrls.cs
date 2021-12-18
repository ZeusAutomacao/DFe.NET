using NFe.Utils.Enderecos;

namespace NFe.Utils
{
    public static class ConfiguracaoUrls
    {
        private static FactoryUrl _factoryUrl;

        static ConfiguracaoUrls()
        {
            FactoryUrl = FactoryUrl.CriaFactoryUrl();
        }

        public static FactoryUrl FactoryUrl
        {
            get { return _factoryUrl; }
            set
            {
                _factoryUrl = value;
                Enderecador.CarregarEnderecos();
            }
        }
    }
}