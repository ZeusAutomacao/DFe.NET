using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals.aereos
{
    public class tarifa
    {
        private decimal _vTar;
        public CL CL { get; set; }

        public string cTar { get; set; }

        public decimal vTar
        {
            get { return _vTar.Arredondar(2); }
            set { _vTar = value.Arredondar(2); }
        }
    }
}