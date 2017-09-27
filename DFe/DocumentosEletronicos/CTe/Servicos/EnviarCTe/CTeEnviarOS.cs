using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Tipos;
using DFe.DocumentosEletronicos.CTe.CTeOS.Extensoes;
using DFe.DocumentosEletronicos.CTe.CTeOS.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Servicos.Factory;
using DFe.DocumentosEletronicos.Flags;
using DFe.Ext;

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

        public retCTeOS Enviar(CTeOS.CTeOS cte)
        {
            cte.InfCte.ide.ProxydhEmi = cte.InfCte.ide.dhEmi.ParaDataHoraStringUtc();
            cte.InfCte.versao = VersaoServico.Versao300;
            cte.InfCte.infCTeNorm.infModal.versaoModal = versaoModal.veM300;

            cte.Assina(_dfeConfig, _certificadoDigital);
            cte.ValidaSchema(_dfeConfig);
            cte.SalvarXmlEmDisco(_dfeConfig);


            var webService = WsdlFactory.CriaWsdlCteRecepcaoOs(_dfeConfig, _certificadoDigital);

            var retornoXml = webService.cteRecepcaoOS(cte.CriaRequestWs(_dfeConfig));

            var retCteOs = retCTeOS.LoadXml(retornoXml.OuterXml, cte);

            retCteOs.SalvarXmlEmDisco(_dfeConfig);

            return retCteOs;
        }
    }
}