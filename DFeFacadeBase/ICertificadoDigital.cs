namespace DFeFacadeBase
{
    public interface ICertificadoDigital
    {
        DFeTipoCertificado TipoCertificado { get; }
        bool ManterEmCache { get; set; }
        string CacheId { get; set; }
    }
}