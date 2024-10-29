using System;

namespace DFe.Utils
{
    public static class DataHoraOffSet
    {
        /// <summary>
        /// Retorna uma string no formato AAAA-MM-DD
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParaDataString(this DateTimeOffset data)
        {
            return data == DateTimeOffset.MinValue ? null : data.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Retorna uma string no formato AAAA-MM-DD
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParaDataString(this DateTimeOffset? data)
        {
            if (data == null) return null;

            return data == DateTimeOffset.MinValue ? null : data.Value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Retorna uma string no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParaDataHoraStringUtc(this DateTimeOffset data)
        {
            return data == DateTimeOffset.MinValue ? null : data.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        /// <summary>
        /// Retorna uma string no formato AAAA-MM-DDThh:mm:dd 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParaDataHoraStringSemUtc(this DateTimeOffset data)
        {
            return data == DateTimeOffset.MinValue ? null : data.ToString("yyyy-MM-ddTHH:mm:dd");
        }

        /// <summary>
        /// Retorna uma string no formato AAAA-MM-DDThh:mm:dd 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParaDataHoraStringSemUtc(this DateTimeOffset? data)
        {
            return ParaDataHoraStringSemUtc(data.GetValueOrDefault());
        }

        /// <summary>
        /// Retorna uma string no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParaDataHoraStringUtc(this DateTimeOffset? data)
        {
            return ParaDataHoraStringUtc(data.GetValueOrDefault());
        }

        /// <summary>
        /// Retorna uma string no formato AAAA-MM-DD HH:mm:ss
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParaDataHoraString(this DateTimeOffset data)
        {
            return data.ToString("yyyyMMddHHmmss");
        }
    }
}