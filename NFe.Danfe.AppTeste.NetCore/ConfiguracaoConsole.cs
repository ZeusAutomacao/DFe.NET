using NFe.Danfe.Base.NFe;

namespace NFe.Danfe.AppTeste.NetCore
{
    public class ConfiguracaoConsole
    {
        public ConfiguracaoConsole()
        {
            ConfiguracaoDanfeNfe = new ConfiguracaoDanfeNfe();
        }

        public ConfiguracaoDanfeNfe ConfiguracaoDanfeNfe { get; set; }
    }
}
