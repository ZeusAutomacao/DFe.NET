using System;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTe.AppTeste.Entidades
{
    [Serializable]
    public class ConfigWebService
    {
        public Estado UfEmitente { get; set; }
        public TipoAmbiente Ambiente { get; set; }
        public short Serie { get; set; }
        public long Numeracao { get; set; }
        public versao Versao { get; set; }
        public string CaminhoSchemas { get; set; }
        public int TimeOut { get; set; }
    }
}