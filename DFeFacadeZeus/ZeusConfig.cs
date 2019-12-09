using DFeFacadeBase;

namespace DFeFacadeZeus
{
    public class ZeusConfig : DFeBase
    {
        public override DFeEstado ObterEstado()
        {
            return DFeEstado.GO;
        }

        public override ICertificadoDigital ConfiguracaoCertificadoDigital()
        {
            return new CertificadoDigitalA1(
                @"C:\Users\rober\OneDrive\Roberto\Documentos\Certificados\AGIL4 TECNOLOGIA LTDA VENC 16-10-2019.pfx"
                ,"agil4@321");
        }
    }
}