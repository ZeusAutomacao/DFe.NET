using System;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class prodPred
    {
        public tpCarga tpCarga { get; set; }

        public string xProd { get; set; }

        public string cEAN { get; set; }

        public string NCM { get; set; }

        public infLotacao infLotacao { get; set; }
    }
}