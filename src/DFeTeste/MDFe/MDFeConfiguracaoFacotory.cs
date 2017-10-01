using DFe.DocumentosEletronicos.Flags;

namespace DFeTeste.MDFe
{
    internal static class MDFeConfiguracaoFacotory
    {
        public static MDFeConfiguracao CriaConfiguracao()
        {
            return new MDFeConfiguracao
            {
                VersaoServico = VersaoServico.Versao300,
                TipoAmbiente = TipoAmbiente.Homologacao
            };
        }
    }
}