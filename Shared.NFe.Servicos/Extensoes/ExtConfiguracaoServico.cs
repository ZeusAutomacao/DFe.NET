using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Utils;
using NFe.Utils.Enderecos;

namespace NFe.Servicos.Extensoes
{
    public static class ExtConfiguracaoServico
    {
        /// <summary>
        /// Retorna verdadeiro para as UFs que utilizam a SVAN - Sefaz Virtual do Ambiente Nacional, para serviços com versão 4.0
        /// </summary>
        /// <returns></returns>
        public static bool UsaSvanNFe4(this ConfiguracaoServico cfgServico, VersaoServico versaoServico)
        {
            return Enderecador.EstadosQueUsamSvanParaNfe().Contains(cfgServico.cUF)
                   && versaoServico == VersaoServico.Versao400
                   && cfgServico.ModeloDocumento == ModeloDocumento.NFe;
        }

        /// <summary>
        /// Retorna verdadeiro para as UFs que utilizam SVC-AN, caso o tipo de emissão seja SVC-AN e o documento seja NF-e
        /// </summary>
        /// <param name="cfgServico"></param>
        /// <returns></returns>
        public static bool UsaSvcanNFe4(this ConfiguracaoServico cfgServico, VersaoServico versaoServico)
        {
            return Enderecador.EstadosQueUsamSvcAnParaNfe().Contains(cfgServico.cUF)
                   && cfgServico.tpEmis == TipoEmissao.teSVCAN
                   && cfgServico.ModeloDocumento == ModeloDocumento.NFe
                   && versaoServico == VersaoServico.Versao400
                    ;
        }
    }
}