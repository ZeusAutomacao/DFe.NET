using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class comHora : EntregaTipos
    {
        public int tpHor { get; set; }
        public string hProg { get; set; }
    }
}
