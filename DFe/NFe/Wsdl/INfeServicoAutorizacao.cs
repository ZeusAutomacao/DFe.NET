using System.Xml;

namespace DFe.NFe.Wsdl
{
    public interface INfeServicoAutorizacao : INfeServico
    {
        XmlNode ExecuteZip(string nfeDadosMsgZip);
    }
}
