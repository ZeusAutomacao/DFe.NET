using DFe.Classes.Entidades;

namespace CTeDLL.Classes.Informacoes.Identificacao
{
    public class enderToma
    {
        public string xLgr { get; set; }

        public string nro { get; set; }

        public string xCpl { get; set; }

        public string xBairro { get; set; }

        public string cMun { get; set; }

        public string xMun { get; set; }

        public string CEP { get; set; }

        public Estado UF { get; set; }

        public int? cPais { get; set; }

        public bool cPaisSpecified { get { return cPais.HasValue; } }

        public string xPais { get; set; }
    }
}
