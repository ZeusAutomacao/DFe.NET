using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class noInter : EntregaTipos
    {
        public int tpHor { get; set; }
        public string hIni { get; set; }
        public string hFim { get; set; }
    }
}
