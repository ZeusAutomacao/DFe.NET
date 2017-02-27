using System;
using CTeDLL.Classes.Servicos.Tipos;

namespace CTe.Classes.Ext
{
    public static class extversao
    {
        public static string GetString(this versao versao)
        {
            switch (versao)
            {
                    case versao.ve200:
                    return "2.00";
                    case versao.ve300:
                    return "3.00";
                default:
                    throw new InvalidOperationException("A emissão do CT-e possui apenas a versão 2.00 é 3.00");
            }
        }
    }
}