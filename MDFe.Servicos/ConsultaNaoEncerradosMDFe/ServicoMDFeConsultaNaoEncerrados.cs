using MDFe.Classes.Extencoes;
using MDFe.Classes.Retorno.MDFeConsultaNaoEncerrado;
using MDFe.Servicos.Factory;

namespace MDFe.Servicos.ConsultaNaoEncerradosMDFe
{
    public class ServicoMDFeConsultaNaoEncerrados
    {
        public MDFeRetConsMDFeNao MDFeConsultaNaoEncerrados(string cnpjCpf)
        {
            var consMDFeNaoEnc = ClassesFactory.CriarConsMDFeNaoEnc(cnpjCpf);
            consMDFeNaoEnc.ValidarSchema();
            consMDFeNaoEnc.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlMDFeConsNaoEnc();
            var retornoXml = webService.mdfeConsNaoEnc(consMDFeNaoEnc.CriaRequestWs());

            var retorno = MDFeRetConsMDFeNao.LoadXmlString(retornoXml.OuterXml, consMDFeNaoEnc);
            retorno.SalvarXmlEmDisco(cnpjCpf);

            return retorno;
        }
    }
}