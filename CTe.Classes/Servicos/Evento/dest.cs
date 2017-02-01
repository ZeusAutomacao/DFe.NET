using System;
using DFe.Classes;

namespace CTeDLL.Classes.Servicos.Evento
{
    public class dest
    {
        private const string ErroCpfCnpjIdEstrangeiroPreenchidos = "Somente preencher um dos campos: CNPJ, CPF ou idEstrangeiro, para um objeto do tipo dest!";
        private string _cnpj;
        private string _cpf;
        private string _idEstrangeiro;
        private decimal _vNf;
        private decimal _vIcms;
        private decimal _vSt;

        /// <summary>
        ///     P27 - Sigla da UF do destinatário.
        ///     Informar “EX” no caso de operação com o exterior.
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        ///     P28 - Somente preencher um dos campos: CNPJ, CPF ou idEstrangeiro
        /// </summary>
        public string CNPJ
        {
            get { return _cnpj; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(CPF) & string.IsNullOrEmpty(idEstrangeiro))
                    _cnpj = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjIdEstrangeiroPreenchidos);
                }
            }
        }

        /// <summary>
        ///     P29 - Somente preencher um dos campos: CNPJ, CPF ou idEstrangeiro
        /// </summary>
        public string CPF
        {
            get { return _cpf; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(CNPJ) & string.IsNullOrEmpty(idEstrangeiro))
                    _cpf = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjIdEstrangeiroPreenchidos);
                }
            }
        }

        /// <summary>
        ///     P30 - Somente preencher um dos campos: CNPJ, CPF ou idEstrangeiro
        /// </summary>
        public string idEstrangeiro
        {
            get { return _idEstrangeiro; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(CNPJ) & string.IsNullOrEmpty(CPF))
                    _idEstrangeiro = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjIdEstrangeiroPreenchidos);
                }
            }
        }

        /// <summary>
        ///     P31 - Informar a IE do destinatário somente quando o contribuinte destinatário possuir uma inscrição estadual.
        ///     Omitir a tag no caso de destinatário “ISENTO”, ou destinatário não possuir IE.
        /// </summary>
        public string IE { get; set; }

        /*Atenção: 
        NT2014_001_v1.00_Evento_EPEC e NT2014.003_v1.00 mostram que os campos abaixo pertencem a detEvento, no entanto o Schema XML do serviço AN está informado que os campos estão em detEvento/dest 
        */

        /// <summary>
        ///     P32 - Valor total da NF-e
        /// </summary>
        public decimal vNF
        {
            get { return _vNf; }
            set { _vNf = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     P33 - Valor total do ICMS
        /// </summary>
        public decimal vICMS
        {
            get { return _vIcms; }
            set { _vIcms = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     P34 - Valor total do ICMS de Substituição Tributária
        /// </summary>
        public decimal vST
        {
            get { return _vSt; }
            set { _vSt = Valor.Arredondar(value, 2); }
        }

        public bool ShouldSerializeIE()
        {
            return !string.IsNullOrEmpty(IE) && IE.ToUpper() != "ISENTO";
        }
    }
}