using CTe.Classes;
using CTe.CTeOSDocumento.CTe.CTeOS.Extensoes;
using CTe.CTeOSDocumento.CTe.CTeOS.Servicos.Autorizacao;
using CTe.Servicos.Enderecos.Helpers;
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using System;
using System.Text;
using CTeEletronico = CTe.CTeOSClasses.CTeOS;

namespace CTe.Servicos.Recepcao
{
    public class ServicoCTeOSRecepcao
    {
        public event EventHandler<AntesEnviarRecepcao> AntesDeEnviar;

        public retCTeOS CTeRecepcaoSincronoV4(CTeEletronico cte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaConfiguracao = configuracaoServico ?? ConfiguracaoServico.Instancia;

            cte.InfCte.ide.tpEmis = instanciaConfiguracao.TipoEmissao;
            cte.Assina(instanciaConfiguracao);
            cte.infCTeSupl = cte.QrCode(instanciaConfiguracao.X509Certificate2, Encoding.UTF8, instanciaConfiguracao.IsAdicionaQrCode, UrlHelper.ObterUrlQrCode(instanciaConfiguracao));
            cte.SalvarXmlEmDisco(instanciaConfiguracao); //salva em disco antes de validas os schemas, facilitando encontrar possíveis erros
            cte.ValidaSchema(instanciaConfiguracao);
            cte.SalvarXmlEmDisco(instanciaConfiguracao);

            var webService = WsdlFactory.CriaWsdlCteRecepcaoSincronoOSV4(configuracaoServico);

            //OnAntesDeEnviar(enviCte);

            var retornoXml = webService.CTeRecepcaoSincV4(cte.CriaRequestWs(configuracaoServico));

            var retorno = retCTeOS.LoadXml(retornoXml.OuterXml, cte);

            retorno.SalvarXmlEmDisco(cte.Chave(), configuracaoServico);

            return retorno;
        }


    }
}
