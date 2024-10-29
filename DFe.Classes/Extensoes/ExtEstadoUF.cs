using System;
using System.Linq;
using DFe.Classes.Entidades;

namespace DFe.Classes.Extensoes
{
    public static class ExtEstadoUF
    {
        public static Estado SiglaParaEstado(this Estado estado, string siglaUf)
        {
            var enumValues = Enum.GetValues(typeof(Estado)).Cast<Estado>().FirstOrDefault(e => e.GetSiglaUfString() == siglaUf);

            return enumValues;
        }

        public static Estado CodigoIbgeParaEstado(this Estado estado, string codigoIbge)
        {
            var enumValues = Enum.GetValues(typeof(Estado)).Cast<Estado>().FirstOrDefault(est => est.GetCodigoIbgeEmString() == codigoIbge);

            return enumValues;
        }

        public static string GetSiglaUfString(this Estado estado)
        {
            return estado.ToString();
        }

        public static string GetCodigoIbgeEmString(this Estado estado)
        {
            var codigo = (byte) estado;
            return codigo.ToString();
        }

        public static byte GetCodigoIbgeEmByte(this Estado estado)
        {
            var codigo = (byte) estado;

            return codigo;
        }
    }
}