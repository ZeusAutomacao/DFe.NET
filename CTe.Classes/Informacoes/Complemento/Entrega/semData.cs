using CTe.Classes.Informacoes.Complemento.Tipos;
using CTe.Classes.Informacoes.Tipos;

namespace CTe.Classes.Informacoes.Complemento
{
    public class semData : comDataBase
    {
        public semData()
        {
            tpPer = tpPer.SemDataDefinida;
        }

        public tpPer tpPer { get; set; } 
    }
}