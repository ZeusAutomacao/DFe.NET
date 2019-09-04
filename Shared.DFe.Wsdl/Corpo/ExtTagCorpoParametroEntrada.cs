using System;
using DFe.Classes.Entidades;

namespace CTe.CTeOSDocumento.Wsdl.Corpo
{
    public static class ExtTagCorpoParametroEntrada
    {
        public static string GetParametroDeEntradaWsdl(this Estado estado, bool compactar)
        {
            switch (estado)
            {
                case Estado.AC:
                case Estado.AL:
                case Estado.AP:
                case Estado.AM:
                case Estado.BA:
                case Estado.CE:
                case Estado.DF:
                case Estado.ES:
                case Estado.MA:
                case Estado.MT:
                case Estado.MS:
                case Estado.MG:
                case Estado.PA:
                case Estado.PB:
                case Estado.PR:
                case Estado.PE:
                case Estado.PI:
                case Estado.RJ:
                case Estado.RN:
                case Estado.RS:
                case Estado.RO:
                case Estado.RR:
                case Estado.SC:
                case Estado.SP:
                case Estado.SE:
                case Estado.TO:
                case Estado.AN:
                case Estado.EX:
                case Estado.GO:
                    return compactar ? "nfeDadosMsgZip" : "nfeDadosMsg";
                default:
                    throw new ArgumentOutOfRangeException(nameof(estado), estado, null);
            }
        }
    }
}