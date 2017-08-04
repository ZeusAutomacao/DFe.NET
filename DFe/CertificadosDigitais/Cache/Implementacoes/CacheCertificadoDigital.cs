using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DFe.CertificadosDigitais.Cache.Implementacoes
{
    public class CacheCertificadoDigital : IProxyCacheCertificadoDigital
    {
        private static IDictionary<string, CertificadoDigital> Cache { get; } = new ConcurrentDictionary<string, CertificadoDigital>();

        public void Adicionar(string cnpjEmitente, CertificadoDigital certificadoDigital)
        {
            Cache.Add(cnpjEmitente, certificadoDigital);
        }

        public void Remover(string cnpjEmitente)
        {
            Cache.Remove(cnpjEmitente);
        }

        public CertificadoDigital BuscarPorCnpjEmitente(string cnpjEmitente)
        {
            return !Cache.ContainsKey(cnpjEmitente) ? null : Cache[cnpjEmitente];
        }

        public void RemoverTodos()
        {
            Cache.Clear();
        }
    }
}