using System.Xml;

namespace CTeDLL.Wsdl
{
    public interface ICteServico
    {
        cteCabecMsg cteCabecMsg { get; set; }
        XmlNode Execute(XmlNode cteDadosMsg);
    }
}