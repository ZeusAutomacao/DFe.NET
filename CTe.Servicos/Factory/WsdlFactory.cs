using CTe.Classes.Ext;
using CTeDLL.Servicos.Enderecos.Helpers;
using CTeDLL.Wsdl.Configuracao;
using CTeDLL.Wsdl.Status;
using DFe.Classes.Extencoes;

namespace CTeDLL.Servicos.Factory
{
    public class WsdlFactory
    {
        public static CteStatusServico CriaWsdlCteStatusServico()
        {
            var configuracaoWsdl = CriaConfiguracao();

            return new CteStatusServico(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao()
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            var codigoEstado = configuracaoServico.cUF.GetCodigoIbgeEmString();
            var certificadoDigital = configuracaoServico.X509Certificate2;
            var versaoEmString = configuracaoServico.VersaoLayout.GetString();
            var timeOut = configuracaoServico.TimeOut;
            var url = UrlHelper.ObterUrlServico().CteStatusServico;

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