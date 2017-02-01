using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class comData : EntregaTipos
    {
        public int tpPer { get; set; }
        public string dProg { get; set; }
    }
}
