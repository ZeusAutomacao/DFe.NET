using System.Xml;
using NFe.Classes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils;

namespace NFe.Integracao
{
    public class NFeFacade
    {
        public NFeFacade()
        {
            //TODO: Carregar as informações necessárias no objeto estático "ConfiguracaoServico"
        }

        public XmlNode ConsultarStatusServico(TipoAmbiente ambiente)
        {
            return new XmlDocument(); //TODO: Implementar "ConsultarStatusServico"
        }

        public XmlNode EnviarNFe(Classes.NFe nfe)
        {
            return new XmlDocument(); //TODO: Implementar "EnviarNFe"
        }

        public XmlNode ConsultarRecibo(string recibo)
        {
            return new XmlDocument(); //TODO: Implementar "ConsultarRecibo"
        }

        public XmlNode InutilizarNumeracao(int inicial, int final)
        {
            return new XmlDocument(); //TODO: Implementar "InutilizarNumeracao"
        }

        public XmlNode ConsultarProtocolo(string protocolo)
        {
            return new XmlDocument(); //TODO: Implementar "ConsultarProtocolo"
        }
    }
}
