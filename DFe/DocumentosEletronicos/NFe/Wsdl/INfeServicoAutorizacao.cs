using System.Xml;

namespace DFe.DocumentosEletronicos.NFe.Wsdl
{
    public interface INfeServicoAutorizacao : INfeServico
    {
        XmlNode ExecuteZip(string nfeDadosMsgZip);
    }
}
