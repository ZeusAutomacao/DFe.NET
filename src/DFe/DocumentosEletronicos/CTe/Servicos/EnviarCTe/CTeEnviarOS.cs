using System;
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
        public event EventHandler<AntesDeEnviarCteOs> AntesDeEnviarCteOs;
        public event EventHandler<AntesDeValidarSchema> AntesDeValidarSchema;
        public event EventHandler<AntesDeAssinar> AntesDeAssinar; 

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

            OnAntesDeAssinar(new AntesDeAssinar(cte));
            cte.Assina(_dfeConfig, _certificadoDigital);

            OnAntesDeValidarSchema(new AntesDeValidarSchema(cte));
            cte.ValidaSchema(_dfeConfig);

            cte.SalvarXmlEmDisco(_dfeConfig);


            var webService = WsdlFactory.CriaWsdlCteRecepcaoOs(_dfeConfig, _certificadoDigital);


            OnAntesDeEnviarCteOs(new AntesDeEnviarCteOs(cte));
            var retCteOs = webService.Autorizar(cte.CriaRequestWs(_dfeConfig));

            retCteOs.LoadXml(cte);

            retCteOs.SalvarXmlEmDisco(_dfeConfig);

            return retCteOs;
        }

        protected virtual void OnAntesDeEnviarCteOs(AntesDeEnviarCteOs e)
        {
            AntesDeEnviarCteOs?.Invoke(this, e);
        }

        protected virtual void OnAntesDeValidarSchema(AntesDeValidarSchema e)
        {
            AntesDeValidarSchema?.Invoke(this, e);
        }

        protected virtual void OnAntesDeAssinar(AntesDeAssinar e)
        {
            AntesDeAssinar?.Invoke(this, e);
        }
    }
}