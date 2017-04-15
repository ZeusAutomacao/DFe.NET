using System;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using MDFe.Utils.Flags;

namespace MDFe.AppTeste.Entidades
{
    [Serializable]
    public class ConfigWebService
    {
        public Estado UfEmitente { get; set; }
        public VersaoServico VersaoLayout { get; set; }
        public TipoAmbiente Ambiente { get; set; }
        public short Serie { get; set; }
        public long Numeracao { get; set; }
        public string CaminhoSchemas { get; set; }
        public int TimeOut { get; set; }
    }
}
