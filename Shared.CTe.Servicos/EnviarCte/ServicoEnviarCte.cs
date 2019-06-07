using System.Collections.Generic;
using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Recepcao.Retorno;
using CTe.Servicos.ConsultaRecibo;
using CTe.Servicos.Recepcao;
using CTe.Utils.CTe;

namespace CTe.Servicos.EnviarCte
{
    public class ServicoEnviarCte
    {
        public RetornoEnviarCte Enviar(int lote, Classes.CTe cte, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            
            ServicoCTeRecepcao servicoRecepcao = new ServicoCTeRecepcao();

            retEnviCte retEnviCte = servicoRecepcao.CTeRecepcao(lote, new List<Classes.CTe> {cte}, configServico);

            if (retEnviCte.cStat != 103)
            {
                return new RetornoEnviarCte(retEnviCte, null, null);
            }

            ConsultaReciboServico servicoConsultaRecibo = new ConsultaReciboServico(retEnviCte.infRec.nRec);

            retConsReciCTe retConsReciCTe = servicoConsultaRecibo.Consultar(configServico);


            cteProc cteProc = null;
            if (retConsReciCTe.cStat == 104)
            {

                if (retConsReciCTe.protCTe[0].infProt.cStat != 100)
                {
                    return new RetornoEnviarCte(retEnviCte, retConsReciCTe, null);
                }

                cteProc = new cteProc
                {
                    CTe = cte,
                    versao = configServico.VersaoLayout,
                    protCTe = retConsReciCTe.protCTe[0]
                };
            }

            cteProc.SalvarXmlEmDisco(configServico);

            return new RetornoEnviarCte(retEnviCte, retConsReciCTe, cteProc);
        }

        public async Task<RetornoEnviarCte> EnviarAsync(int lote, Classes.CTe cte, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            ServicoCTeRecepcao servicoRecepcao = new ServicoCTeRecepcao();

            retEnviCte retEnviCte = await servicoRecepcao.CTeRecepcaoAsync(lote, new List<Classes.CTe> { cte }, configServico);

            if (retEnviCte.cStat != 103)
            {
                return new RetornoEnviarCte(retEnviCte, null, null);
            }

            ConsultaReciboServico servicoConsultaRecibo = new ConsultaReciboServico(retEnviCte.infRec.nRec);

            retConsReciCTe retConsReciCTe = await servicoConsultaRecibo.ConsultarAsync(configServico);


            cteProc cteProc = null;
            if (retConsReciCTe.cStat == 104)
            {

                if (retConsReciCTe.protCTe[0].infProt.cStat != 100)
                {
                    return new RetornoEnviarCte(retEnviCte, retConsReciCTe, null);
                }

                cteProc = new cteProc
                {
                    CTe = cte,
                    versao = configServico.VersaoLayout,
                    protCTe = retConsReciCTe.protCTe[0]
                };
            }

            cteProc.SalvarXmlEmDisco(configServico);

            return new RetornoEnviarCte(retEnviCte, retConsReciCTe, cteProc);
        }
    }
}