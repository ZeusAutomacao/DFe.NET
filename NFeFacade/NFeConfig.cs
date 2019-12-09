using DFe.Classes.Entidades;
using DFeFacadeBase;

namespace NFeFacade
{
    public class NFeConfig : DFeBase
    {
        public override Estado ObterEstado()
        {
            return Estado.GO;
        }
    }
}