using System;
using System.Collections.Generic;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Tipos;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Servicos.Factory;
using DFe.DocumentosEletronicos.CTe.Servicos.Recepcao;
using DFe.DocumentosEletronicos.Flags;
using DFe.Ext;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EvniarLoteCTe
{
    public class CTeOSEnviarLote
    {
        private readonly DFeConfig _dfeConfig;
        private readonly CertificadoDigital _certificadoDigital;

        public CTeOSEnviarLote(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            _dfeConfig = dfeConfig;
            _certificadoDigital = certificadoDigital;
        }

        public event EventHandler<AntesEnviarRecepcao> AntesDeEnviar;

        public retEnviCte EnviarLote(int lote, List<CTeOS.CTeOS> cteEletronicosList)
        {
            var enviCte = ClassesFactory.CriaEnviCTeOS(lote, cteEletronicosList, _dfeConfig);

            foreach (var cte in cteEletronicosList)
            {
                cte.InfCte.ide.ProxydhEmi = cte.InfCte.ide.dhEmi.ParaDataHoraStringUtc();
                cte.InfCte.versao = VersaoServico.Versao300;
                cte.InfCte.infCTeNorm.infModal.versaoModal = versaoModal.veM300;
            }

            if (_dfeConfig.TipoAmbiente == TipoAmbiente.Homologacao)
            {
                foreach (var cte in enviCte.CTeOs)
                {
                    const string razaoSocial = "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
                    
                    // todo verificar oque deve ser colocado com a string acima hehe
                }
            }


            foreach (var cte in enviCte.CTeOs)
            {
                cte.Assina(_dfeConfig, _certificadoDigital);
                cte.ValidaSchema(_dfeConfig);
                cte.SalvarXmlEmDisco(_dfeConfig);
            }

            enviCte.ValidaSchema(_dfeConfig);
            enviCte.SalvarXmlEmDisco(_dfeConfig);

            var webService = WsdlFactory.CriaWsdlCteRecepcao(_dfeConfig, _certificadoDigital);

            

            var retornoXml = webService.cteRecepcaoLote(enviCte.CriaRequestWs(_dfeConfig));

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);

            retorno.SalvarXmlEmDisco(_dfeConfig);

            return retorno;
        }
    }
}