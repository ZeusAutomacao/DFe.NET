using System.Collections.Generic;
using DFe.DocumentosEletronicos.CTe.Classes;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.RetRecepcao;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Servicos.ConsultaLoteCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EvniarLoteCTe;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe
{
    public class CTeEnviar
    {
        public RetornoEnviarCte Enviar(int lote, Classes.Informacoes.CTe cte)
        {
            CTeEnviarLote enviarLote = new CTeEnviarLote();

            retEnviCte retEnviCte = enviarLote.CTeRecepcao(lote, new List<Classes.Informacoes.CTe> {cte});

            if (retEnviCte.cStat != 103)
            {
                return new RetornoEnviarCte(retEnviCte, null, null);
            }

            CTeConsultaLote servicoCTeConsultaRecibo = new CTeConsultaLote(retEnviCte.infRec.nRec);

            retConsReciCTe retConsReciCTe = servicoCTeConsultaRecibo.Consultar();


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
                    versao = ConfiguracaoServico.Instancia.VersaoLayout,
                    protCTe = retConsReciCTe.protCTe[0]
                };
            }

            cteProc.SalvarXmlEmDisco();

            return new RetornoEnviarCte(retEnviCte, retConsReciCTe, cteProc);
        }
    }
}