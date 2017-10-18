using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Consulta;

namespace DFe.DocumentosEletronicos.CTe.Classes.Retorno.Evento
{
    public static class CTeCancelado
    {
        public static bool IsCancelado(this retConsSitCTe retEventoCTe)
        {
            return retEventoCTe.cStat == 631;
        }
    }
}