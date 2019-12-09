namespace DFeFacadeBase
{
    public interface IWStatusServico<T>
    {
        IConsultaStatusRetorno ConsultaStatus(DFeBase<T> dfeBase);
    }
}