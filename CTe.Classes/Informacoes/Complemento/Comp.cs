using DFe.Classes;

namespace CTe.Classes.Informacoes.Complemento
{
    public class Comp
    {
        private string _xNome;
        private decimal _vComp;

        public string xNome { get { return _xNome; } set { _xNome = value; } }
        public decimal vComp
        {
            get { return _vComp.Arredondar(2); }
            set { _vComp = value.Arredondar(2); }
        }
    }
}