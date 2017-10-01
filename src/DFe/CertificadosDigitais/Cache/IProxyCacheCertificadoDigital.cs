namespace DFe.CertificadosDigitais.Cache
{
    public interface IProxyCacheCertificadoDigital
    {
        void Adicionar(string cnpjEmitente, CertificadoDigital certificadoDigital);

        void Remover(string cnpjEmitente);

        CertificadoDigital BuscarPorCnpjEmitente(string cnpjEmitente);

        void RemoverTodos();
    }
}