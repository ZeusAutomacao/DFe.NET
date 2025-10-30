using System.IO;
using DFe.Utils;
using NFe.Danfe.Base;
using NFe.Danfe.Base.NFCe;
using NFe.Danfe.Base.NFe;

namespace NFe.Danfe.AppTeste
{
    public class ConfiguracaoApp
    {
        public ConfiguracaoApp()
        {
            ConfiguracaoDanfeNfce = new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha);

            ConfiguracaoDanfeNfe = new ConfiguracaoDanfeNfe();
        }

        public ConfiguracaoDanfeNfce ConfiguracaoDanfeNfce { get; set; }

        public ConfiguracaoDanfeNfe ConfiguracaoDanfeNfe { get; set; }


        /// <summary>
        /// Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ
        /// </summary>
        public string CIdToken { get; set; }

        /// <summary>
        /// Código de Segurança do Contribuinte(antigo Token)
        /// </summary>
        public string Csc { get; set; }

        /// <summary>
        ///     Salva os dados de CfgServico em um arquivo XML
        /// </summary>
        /// <param name="arquivo">Arquivo XML onde será salvo os dados</param>
        public void SalvarParaAqruivo(string arquivo)
        {
            var dir = Path.GetDirectoryName(arquivo);
            if (dir != null && !Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Diretório " + dir + " não encontrado!");
            }

            FuncoesXml.ClasseParaArquivoXml(this, arquivo);
        }
    }
}