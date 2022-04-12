using System;
using DFe.Classes.Flags;

namespace DFe.Classes.Extensoes
{
    public static class ExtVersaoServico
    {
        public static string GetVersaoString(this VersaoServico versaoServico)
        {
            switch (versaoServico)
            {
                case VersaoServico.Versao100:
                    return "1.00";
                case VersaoServico.Versao200:
                    return "2.00";
                case VersaoServico.Versao300:
                    return "3.00";
                case VersaoServico.Versao310:
                    return "3.10";
                case VersaoServico.Versao400:
                    return "4.00";
                default:
                    throw new ArgumentOutOfRangeException("versaoServico", versaoServico, null);
            }
        }
    }
}