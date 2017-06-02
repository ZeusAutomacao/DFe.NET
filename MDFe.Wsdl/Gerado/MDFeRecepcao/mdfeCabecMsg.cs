using System.Web.Services.Protocols;

namespace MDFe.Wsdl.Gerado.MDFeRecepcao.Teste
{
    public class mdfeCabecMsg : SoapHeader
    {
        public string cUF { get; set; }
        public string versaoDados { get; set; }
    }
}