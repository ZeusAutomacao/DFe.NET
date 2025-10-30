using System;

namespace NFe.Classes.Servicos.AdmCsc
{
    [Serializable()]
    public class dadosCsc
    {
        /// <summary>
        /// AP07 / AR08 - Número identificador do CSC
        /// </summary>
        public string idCsc { get; set; }

        /// <summary>
        /// AP08 / AR09 - Código alfanumérico do CSC
        /// </summary>
        public string codigoCsc { get; set; }
    }
}