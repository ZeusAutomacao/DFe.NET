using System;
using System.Runtime.InteropServices;

namespace CTeDLL.Classes.Informacoes
{

    [Guid("0e829c36-cf37-4647-a367-6b90a6752916")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("CTeDLL.Classes")]
    [ComVisible(true)]
    public class autXML
    {
        private const string ErroCpfCnpjPreenchidos = "Somente preencher um dos campos: CNPJ ou CPF, para um objeto do tipo autXML!";
        private string cnpj;
        private string cpf;

        /// <summary>
        ///     GA02 - CNPJ Autorizado
        /// </summary>
        public string CNPJ
        {
            get { return cnpj; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(cpf))
                    cnpj = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        ///     GA03 - CPF Autorizado
        /// </summary>
        public string CPF
        {
            get { return cpf; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(cnpj))
                    cpf = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }
    }
}