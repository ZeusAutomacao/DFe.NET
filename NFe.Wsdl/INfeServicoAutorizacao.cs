using NFe.Classes.Servicos.Autorizacao;
using System.Xml;

namespace NFe.Wsdl
{
    public interface INfeServicoAutorizacao : INfeServico
    {
        XmlNode ExecuteZip(string nfeDadosMsgZip);
    }
}
