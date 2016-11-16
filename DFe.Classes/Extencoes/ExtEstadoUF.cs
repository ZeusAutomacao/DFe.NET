using System;
using System.Linq;
using System.Net.NetworkInformation;
using DFe.Classes.Entidades;

namespace DFe.Classes.Extencoes
{
    public static class ExtEstadoUF
    {
        public static EstadoUF SiglaParaEstado(this EstadoUF estadoUf, string siglaUf)
        {
            var enumValues = Enum.GetValues(typeof(EstadoUF)).Cast<EstadoUF>().FirstOrDefault(estado => estado.GetSiglaUfString() == siglaUf);

            return enumValues;
        }

        public static string GetSiglaUfString(this EstadoUF estadoUf)
        {
            return estadoUf.ToString();
        }

        public static string GetCodigoIbgeEmString(this EstadoUF estadoUf)
        {
            var codigo = (byte) estadoUf;
            return codigo.ToString();
        }
    }
}