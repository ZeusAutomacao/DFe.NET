namespace NFe.Utils.Enderecos
{
    public class FactoryUrl
    {
        public FactoryUrl(IZeusEnderecosUrls ceara)
        {
            Ceara = ceara;
        }

        public IZeusEnderecosUrls Ceara { get; }


        public static FactoryUrl CriaFactoryUrl()
        {
            return new FactoryUrl(new CearaUrl());
        }
    }
}