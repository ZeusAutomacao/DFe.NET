using System;
using CTe.Classes.Servicos.Tipos;

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
                case versao.ve400:
                    return "4.00";
                default:
                    throw new InvalidOperationException("Versão de CT-e inválida. Para emissão apenas as versões 2.00, 3.00 e 4.00 são aceitas.");
            }
        }
    }
}