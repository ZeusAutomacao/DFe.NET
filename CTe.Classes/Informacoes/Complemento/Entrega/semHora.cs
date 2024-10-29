using CTe.Classes.Informacoes.Complemento.Tipos;
using CTe.Classes.Informacoes.Tipos;

namespace CTe.Classes.Informacoes.Complemento
{
    public class semHora : comHoraBase
    {
        public semHora()
        {
            tpHor = tpHor.SemHoraDefinida;
        }

        public tpHor tpHor { get; set; } 
    }
}