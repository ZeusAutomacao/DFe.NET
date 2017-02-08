using System.Web.Services.Protocols;
using DFe.Classes.Entidades;

namespace CTeDLL.Wsdl
{
    public class cteCabecMsg : SoapHeader
    {
        public Estado cUF { get; set; }
        public string versaoDados { get; set; }
    }
}