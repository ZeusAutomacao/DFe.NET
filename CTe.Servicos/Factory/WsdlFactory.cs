using CTe.Classes.Ext;
using CTeDLL.Servicos.Enderecos.Helpers;
using CTeDLL.Wsdl.Configuracao;
using CTeDLL.Wsdl.ConsultaProtocolo;
using CTeDLL.Wsdl.Inutilizacao;
using CTeDLL.Wsdl.RetRecepcao;
using CTeDLL.Wsdl.Status;
using DFe.Classes.Extencoes;

namespace CTeDLL.Servicos.Factory
{
    public class WsdlFactory
    {
        public static CteStatusServico CriaWsdlCteStatusServico()
        {
            var url = UrlHelper.ObterUrlServico().CteStatusServico;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteStatusServico(configuracaoWsdl);
        }

        public static CteConsulta CriaWsdlConsultaProtocolo()
        {
            var url = UrlHelper.ObterUrlServico().CteConsulta;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteConsulta(configuracaoWsdl);
        }

        public static CteInutilizacao CriaWsdlCteInutilizacao()
        {
            var url = UrlHelper.ObterUrlServico().CteInutilizacao;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteInutilizacao(configuracaoWsdl);
        }

        public static CteRetRecepcao CriaWsdlCteRetRecepcao()
        {
            var url = UrlHelper.ObterUrlServico().CteRetRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteRetRecepcao(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            var codigoEstado = configuracaoServico.cUF.GetCodigoIbgeEmString();
            var certificadoDigital = configuracaoServico.X509Certificate2;
            var versaoEmString = configuracaoServico.VersaoLayout.GetString();
            var timeOut = configuracaoServico.TimeOut;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versaoEmString,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = timeOut
            };
        }
    }
}