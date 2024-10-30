using System;

namespace NFe.Classes.Informacoes.Transporte
{
    public class transporta
    {
        private const string ErroCpfCnpjPreenchidos = "Somente preencher um dos campos: CNPJ ou CPF, para um objeto do tipo transporta!";
        private string cnpj;
        private string cpf;

        /// <summary>
        ///     X04 - CNPJ do Transportador
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
        ///     X05 - CPF do Transportador
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

        /// <summary>
        ///     X06 - Razão Social ou nome
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        ///     X07 - Inscrição Estadual do Transportador
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        ///     X08 - Endereço Completo
        /// </summary>
        public string xEnder { get; set; }

        /// <summary>
        ///     X09 - Nome do município
        /// </summary>
        public string xMun { get; set; }

        /// <summary>
        ///     X10 - Sigla da UF
        /// </summary>
        public string UF { get; set; }
    }
}