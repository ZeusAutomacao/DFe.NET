using NFe.Danfe.Base;
using NFe.Danfe.Base.NFCe;
using NFe.Danfe.Base.NFe;

namespace NFe.Danfe.AppTeste.NetCore
{
    public class ConfiguracaoConsole
    {
        public ConfiguracaoConsole()
        {
            ConfiguracaoDanfeNfe = new ConfiguracaoDanfeNfe();
            ConfiguracaoDanfeNfce = new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha);
        }

        public ConfiguracaoDanfeNfe ConfiguracaoDanfeNfe { get; set; }

        public ConfiguracaoDanfeNfce ConfiguracaoDanfeNfce { get; set; }

        /// <summary>
        /// Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ
        /// </summary>
        public string CIdToken { get; set; }

        /// <summary>
        /// Código de Segurança do Contribuinte(antigo Token)
        /// </summary>
        public string Csc { get; set; }
    }
}
