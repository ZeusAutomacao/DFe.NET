using System.Web.Services.Protocols;
using DFe.Classes.Entidades;

namespace NFe.Wsdl
{
    public class nfeCabecMsg : SoapHeader
    {
        public Estado cUF { get; set; }
        public string versaoDados { get; set; }
    }
}