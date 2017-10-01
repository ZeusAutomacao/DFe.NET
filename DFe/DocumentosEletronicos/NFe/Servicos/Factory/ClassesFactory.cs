using System.Collections.Generic;
using DFe.DocumentosEletronicos.Ext;
using DFe.DocumentosEletronicos.NFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.NFe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.NFe.Classes.Servicos.Status;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Flags;

namespace DFe.DocumentosEletronicos.NFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static consStatServ CriaConsStatServ(NFeBaseConfig config)
        {
            var consStatServ = new consStatServ
            {
                versao = config.VersaoNfeStatusServico.GetVersaoString(),
                tpAmb = config.TipoAmbiente,
                cUF = config.EstadoUf
            };

            return consStatServ;
        }

        public static enviNFe3 CriaEnviNFe3(NFeBaseConfig config, int idLote, IndicadorSincronizacao indicadorSincronizacao, List<Classes.Informacoes.NFe> nfes) 
        {
            var versaoServico = ServicoNFe.NFeAutorizacao.VersaoServicoParaString(config.VersaoNFeAutorizacao);

            return new enviNFe3(versaoServico, idLote, indicadorSincronizacao, nfes);
        }
    }
}