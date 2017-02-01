using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Emitente
{
    public class emit
    {
        public enderEmit enderEmit;

        private string _CNPJ;
        private string _IE;
        private string _xNome;
        private string _xFant;

        public string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }
        public string IE { get { return _IE; } set { _IE = value; } }
        public string xNome { get { return _xNome; } set { _xNome = value; } }
        public string xFant { get { return _xFant; } set { _xFant = value; } }
        
        /// <summary>
        ///     C21 - Código de Regime Tributário
        /// </summary>
        public CRT CRT { get; set; }

    }
}
