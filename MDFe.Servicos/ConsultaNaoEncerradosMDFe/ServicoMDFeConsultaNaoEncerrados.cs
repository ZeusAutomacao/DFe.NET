using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeConsultaNaoEncerrado;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.ConsultaNaoEncerradosMDFe
{
    public class ServicoMDFeConsultaNaoEncerrados
    {
        public MDFeRetConsMDFeNao MDFeConsultaNaoEncerrados(string cnpjCpf, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var consMDFeNaoEnc = ClassesFactory.CriarConsMDFeNaoEnc(cnpjCpf, config);
            consMDFeNaoEnc.ValidarSchema(config);
            consMDFeNaoEnc.SalvarXmlEmDisco(config);

            var webService = WsdlFactory.CriaWsdlMDFeConsNaoEnc(config);
            var retornoXml = webService.mdfeConsNaoEnc(consMDFeNaoEnc.CriaRequestWs());

            var retorno = MDFeRetConsMDFeNao.LoadXmlString(retornoXml.OuterXml, consMDFeNaoEnc);
            retorno.SalvarXmlEmDisco(cnpjCpf, config);

            return retorno;
        }
    }
}