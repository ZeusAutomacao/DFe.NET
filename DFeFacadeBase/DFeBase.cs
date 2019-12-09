namespace DFeFacadeBase
{
    public interface DFeBase<T>
    {
        DFeEstado ObterEstado();
        DFeAmbiente ObterAmbiente();
        DFeModeloDocumento ObterModeloDocumento();
        DFeTipoEmissao ObterTipoEmissao();
        int ObterTimeOut();
        ICertificadoDigital ConfiguracaoCertificadoDigital();
        T ObterConfiguracao();
    }
}