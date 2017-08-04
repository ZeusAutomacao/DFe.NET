using System;
using DFe.Entidades;
using DFe.Flags;
using DFe.MDFe.Classes.Flags;

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
