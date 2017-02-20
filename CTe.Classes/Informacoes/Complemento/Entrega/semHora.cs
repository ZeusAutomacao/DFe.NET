using CTeDLL.Classes.Informacoes.Complemento.Tipos;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class semHora : comHoraBase
    {
         public tpHor tpHor { get; set; } = tpHor.SemHoraDefinida;
    }
}
