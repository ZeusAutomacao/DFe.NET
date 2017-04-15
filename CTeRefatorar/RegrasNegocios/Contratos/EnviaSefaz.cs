using System.Security.Cryptography.X509Certificates;
using System.Xml;
using FusionCore.DFe.XmlCte;

namespace FusionCore.DFe.RegrasNegocios.contratos
{
    public interface EnviaSefaz
    {
        XmlNode Executa(XmlNode xmlEnvio,
            FusionEstadoUFCTe estado,
            X509Certificate2 certificado,
            FusionTipoAmbienteCTe ambiente);
    }
}