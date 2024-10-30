using System.Xml;

namespace NFe.Wsdl
{
    public interface INfeServico
    {
        nfeCabecMsg nfeCabecMsg { get; set; }
        XmlNode Execute(XmlNode nfeDadosMsg);
    }
}