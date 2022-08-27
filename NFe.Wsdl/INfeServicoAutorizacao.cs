using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl
{
    public interface INfeServicoAutorizacao : INfeServico
    {
        XmlNode ExecuteZip(string nfeDadosMsgZip);
        Task<XmlNode> ExecuteZipAsync(string nfeDadosMsgZip);
    }
}
