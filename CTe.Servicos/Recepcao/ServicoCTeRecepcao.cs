using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Tipos;
using CTe.Servicos.Enderecos.Helpers;
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using CTe.Utils.Recepcao;
using DFe.Classes.Flags;
using CTeEletronico = CTe.Classes.CTe;

namespace CTe.Servicos.Recepcao
{
    public class ServicoCTeRecepcao
    {
        public event EventHandler<AntesEnviarRecepcao> AntesDeEnviar;

        public retEnviCte CTeRecepcao(int lote, List<CTeEletronico> cteEletronicosList, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var enviCte = PreparaEnvioCTe(lote, cteEletronicosList, configServico);

            var webService = WsdlFactory.CriaWsdlCteRecepcao(configServico);

            OnAntesDeEnviar(enviCte);

            var retornoXml = webService.cteRecepcaoLote(enviCte.CriaRequestWs(configServico));

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);
            retorno.SalvarXmlEmDisco(configServico);

            return retorno;
        }

        public async Task<retEnviCte> CTeRecepcaoAsync(int lote, List<CTeEletronico> cteEletronicosList, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var enviCte = PreparaEnvioCTe(lote, cteEletronicosList, configServico);

            var webService = WsdlFactory.CriaWsdlCteRecepcao(configServico);

            OnAntesDeEnviar(enviCte);

            var retornoXml = await webService.cteRecepcaoLoteAsync(enviCte.CriaRequestWs(configServico));

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);
            retorno.SalvarXmlEmDisco(configServico);

            return retorno;
        }

        public retCTe CTeRecepcaoSincronoV4(CTeEletronico cte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaConfiguracao = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaConfiguracao.tpAmb == TipoAmbiente.Homologacao && instanciaConfiguracao.VersaoLayout == versao.ve300)
            {
                const string razaoSocial = "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                cte.infCte.rem.xNome = razaoSocial;
                cte.infCte.dest.xNome = razaoSocial;
            }

            if (instanciaConfiguracao.tpAmb == TipoAmbiente.Homologacao && instanciaConfiguracao.VersaoLayout == versao.ve400)
            {
                const string razaoSocial = "CTE EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                cte.infCte.rem.xNome = razaoSocial;
                cte.infCte.dest.xNome = razaoSocial;
            }

            cte.infCte.ide.tpEmis = instanciaConfiguracao.TipoEmissao;
            cte.Assina(instanciaConfiguracao);
            cte.infCTeSupl = cte.QrCode(instanciaConfiguracao.X509Certificate2, Encoding.UTF8, instanciaConfiguracao.IsAdicionaQrCode, UrlHelper.ObterUrlQrCode(instanciaConfiguracao));
            cte.SalvarXmlEmDisco(instanciaConfiguracao); //salva em disco antes de validas os schemas, facilitando encontrar possíveis erros
            cte.ValidaSchema(instanciaConfiguracao);
            cte.SalvarXmlEmDisco(instanciaConfiguracao);

            var webService = WsdlFactory.CriaWsdlCteRecepcaoSincronoV4(instanciaConfiguracao);

            //OnAntesDeEnviar(enviCte);

            var retornoXml = webService.CTeRecepcaoSincV4(cte.CriaRequestWs(instanciaConfiguracao));

            var retorno = retCTe.LoadXml(retornoXml.OuterXml, cte);
            retorno.SalvarXmlEmDisco(cte.Chave(), instanciaConfiguracao);

            return retorno;
        }

        private static enviCTe PreparaEnvioCTe(int lote, List<CTeEletronico> cteEletronicosList, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaConfiguracao = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var enviCte = ClassesFactory.CriaEnviCTe(lote, cteEletronicosList, instanciaConfiguracao);

            if (instanciaConfiguracao.tpAmb == TipoAmbiente.Homologacao)
            {
                foreach (var cte in enviCte.CTe)
                {
                    const string razaoSocial = "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                    cte.infCte.rem.xNome = razaoSocial;
                    cte.infCte.dest.xNome = razaoSocial;
                }
            }


            foreach (var cte in enviCte.CTe)
            {
                cte.infCte.ide.tpEmis = instanciaConfiguracao.TipoEmissao;
                cte.Assina(instanciaConfiguracao);
                cte.infCTeSupl = cte.QrCode(instanciaConfiguracao.X509Certificate2, Encoding.UTF8, instanciaConfiguracao.IsAdicionaQrCode, UrlHelper.ObterUrlQrCode(instanciaConfiguracao));

                if (configuracaoServico.IsValidaSchemas)
                    cte.ValidaSchema(instanciaConfiguracao);

                cte.SalvarXmlEmDisco(instanciaConfiguracao);
            }

            if (configuracaoServico.IsValidaSchemas)
                enviCte.ValidaSchema(instanciaConfiguracao);

            enviCte.SalvarXmlEmDisco(instanciaConfiguracao);
            return enviCte;
        }

        protected virtual void OnAntesDeEnviar(enviCTe enviCTe)
        {
            var handler = AntesDeEnviar;
            if (handler != null) handler(this, new AntesEnviarRecepcao(enviCTe));
        }
    }
}