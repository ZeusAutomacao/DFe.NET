namespace DFeFacadeBase
{
    public interface DFeBase<T>
    {
        DFeEstado ObterEstado();
        DFeAmbiente ObterAmbiente();
        ICertificadoDigital ConfiguracaoCertificadoDigital();
        T GetConfiguracao();
    }
}