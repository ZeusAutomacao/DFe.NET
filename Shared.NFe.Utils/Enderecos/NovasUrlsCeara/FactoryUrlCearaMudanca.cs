using NFe.Utils.Enderecos;

namespace Shared.NFe.Utils.Enderecos.NovasUrlsCeara
{
    public static class FactoryUrlCearaMudanca
    {
        public static FactoryUrl CriaFactoryUrl()
        {
            return new FactoryUrl(new CearaSVRS());
        }
    }
}