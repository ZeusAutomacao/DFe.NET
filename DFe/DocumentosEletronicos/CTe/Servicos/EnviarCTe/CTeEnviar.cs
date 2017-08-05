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
    public class CTeEnviar
    {
        private readonly DFeConfig _dfeConfig;
        private readonly CertificadoDigital _certificadoDigital;

        public CTeEnviar(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            _dfeConfig = dfeConfig;
            _certificadoDigital = certificadoDigital;
        }

        public RetornoEnviarCte Enviar(int lote, Classes.Informacoes.CTe cte)
        {
            CTeEnviarLote enviarLote = new CTeEnviarLote(_dfeConfig, _certificadoDigital);

            retEnviCte retEnviCte = enviarLote.EnviarLote(lote, new List<Classes.Informacoes.CTe> {cte});

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
                    CTe = cte,
                    versao = cte.infCte.versao,
                    protCTe = retConsReciCTe.protCTe[0]
                };
            }

            cteProc.SalvarXmlEmDisco(_dfeConfig);

            return new RetornoEnviarCte(retEnviCte, retConsReciCTe, cteProc);
        }
    }
}