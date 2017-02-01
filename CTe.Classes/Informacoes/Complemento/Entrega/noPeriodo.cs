using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class noPeriodo : EntregaTipos
    {
        public int tpPer { get; set; }
        public string dIni { get; set; }
        public string dFim { get; set; }
    }
}
