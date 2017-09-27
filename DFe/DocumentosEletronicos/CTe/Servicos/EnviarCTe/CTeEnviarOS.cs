using System.Collections.Generic;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.RetRecepcao;
using DFe.DocumentosEletronicos.CTe.Servicos.ConsultaLoteCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EvniarLoteCTe;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe
{
    public class CTeEnviarOS
    {
        private readonly DFeConfig _dfeConfig;
        private readonly CertificadoDigital _certificadoDigital;

        public CTeEnviarOS(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            _dfeConfig = dfeConfig;
            _certificadoDigital = certificadoDigital;
        }

        public RetornoEnviarCte Enviar(int lote, CTeOS.CTeOS cte)
        {
            CTeOSEnviarLote enviarLote = new CTeOSEnviarLote(_dfeConfig, _certificadoDigital);

            retEnviCte retEnviCte = enviarLote.EnviarLote(lote, new List<CTeOS.CTeOS> { cte });

            if (retEnviCte.cStat != 103)
            {
                return new RetornoEnviarCte(retEnviCte, null, null);
            }

            CTeConsultaLote servicoCTeConsultaRecibo = new CTeConsultaLote(_dfeConfig, _certificadoDigital);

            retConsReciCTe retConsReciCTe = servicoCTeConsultaRecibo.ConsultaLote(retEnviCte.infRec.nRec);


            cteProc cteProc = null;
            if (retConsReciCTe.cStat == 104)
            {

                if (retConsReciCTe.protCTe[0].infProt.cStat != 100)
                {
                    return new RetornoEnviarCte(retEnviCte, retConsReciCTe, null);
                }

                cteProc = new cteProc
                {
                    CTeOS = cte,
                    versao = cte.InfCte.versao,
                    protCTe = retConsReciCTe.protCTe[0]
                };
            }

            cteProc.SalvarXmlEmDisco(_dfeConfig);

            return new RetornoEnviarCte(retEnviCte, retConsReciCTe, cteProc);
        }
    }
}