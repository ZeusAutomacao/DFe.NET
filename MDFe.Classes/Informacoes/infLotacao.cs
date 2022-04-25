using System;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infLotacao
    {
        public infLocalCarrega infLocalCarrega { get; set; }

        public infLocalDescarrega infLocalDescarrega { get; set; }
    }
}