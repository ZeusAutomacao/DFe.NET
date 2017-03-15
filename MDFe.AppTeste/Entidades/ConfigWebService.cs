using System;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using MDFe.Classes.Servicos.Flags;

namespace MDFe.AppTeste.Entidades
{
    [Serializable]
    public class ConfigWebService
    {
        public Estado UfEmitente { get; set; }
        public TipoAmbiente Ambiente { get; set; }
        public short Serie { get; set; }
        public long Numeracao { get; set; }
        public VersaoServico VersaoMDFeRecepcao { get; set; }
        public VersaoServico VersaoMDFeRetRecepcao { get; set; }
        public VersaoServico VersaoMDFeRecepcaoEvento { get; set; }
        public VersaoServico VersaoMDFeConsulta { get; set; }
        public VersaoServico VersaoMDFeStatusServico { get; set; }
        public VersaoServico VersaoMDFeConsNaoEnc { get; set; }
        public string CaminhoSchemas { get; set; }
        public int TimeOut { get; set; }
    }
}
