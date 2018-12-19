using System.IO;
using DFe.Utils;

namespace SMDFe.Tests.Dao
{
    public class CertificadoDao
    {
        private ConfiguracaoCertificado _certificado;

        public CertificadoDao()
        {
            _certificado = new ConfiguracaoCertificado()
            {
                TipoCertificado = TipoCertificado.A1ByteArray,
                ArrayBytesArquivo = CarregarArray(),
                ManterDadosEmCache = false
            };
        }

        private byte[] CarregarArray()
        {
            return  File.ReadAllBytes(@"Resources\certificado.txt");
        }

        public ConfiguracaoCertificado getConfiguracaoCertificado()
        {
            return _certificado;
        }
    }
}
