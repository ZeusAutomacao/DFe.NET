namespace DFeFacadeBase
{
    public abstract class DFeBase
    {
        public abstract DFeEstado ObterEstado();
        public abstract DFeAmbiente ObterAmbiente();
        public abstract ICertificadoDigital ConfiguracaoCertificadoDigital();
    }
}