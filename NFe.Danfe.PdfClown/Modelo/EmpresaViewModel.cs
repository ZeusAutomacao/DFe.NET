using System.Text;
using NFe.Danfe.PdfClown.Tools;

namespace NFe.Danfe.PdfClown.Modelo
{
    public class EmpresaViewModel
    {
        /// <summary>
        /// <para>Razão Social ou Nome</para>
        /// <para>Tag xNome</para>
        /// </summary>
        public string RazaoSocial { get; set; }

        /// <summary>
        /// <para>Nome fantasia</para>
        /// <para>Tag xFant</para>
        /// </summary>
        public string NomeFantasia { get; set; }

        /// <summary>
        /// <para>Logradouro</para>
        /// <para>Tag xLgr</para>
        /// </summary>
        public string EnderecoLogadrouro { get; set; }

        /// <summary>
        /// <para>Complemento</para>
        /// <para>Tag xCpl</para>
        /// </summary>
        public string EnderecoComplemento { get; set; }

        /// <summary>
        /// <para>Número</para>
        /// <para>Tag nro</para>
        /// </summary>
        public string EnderecoNumero { get; set; }

        /// <summary>
        /// <para>Código do CEP</para>
        /// <para>Tag CEP</para>
        /// </summary>
        public string EnderecoCep { get; set; }

        /// <summary>
        /// <para>Bairro</para>
        /// <para>Tag xBairro</para>
        /// </summary>
        public string EnderecoBairro { get; set; }

        /// <summary>
        /// <para>Sigla da UF</para>
        /// <para>Tag UF</para>
        /// </summary>
        public string EnderecoUf { get; set; }

        /// <summary>
        /// <para>Nome do município</para>
        /// <para>Tag xMun</para>
        /// </summary>
        public string Municipio { get; set; }

        /// <summary>
        /// <para>Telefone</para>
        /// <para>Tag fone</para>
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// <para>CNPJ ou CPF</para>
        /// <para>Tag CNPJ ou CPF</para>
        /// </summary>
        public string CnpjCpf { get; set; }

        /// <summary>
        /// <para>Inscrição Estadual</para>
        /// <para>Tag IE</para>
        /// </summary>
        public string Ie { get; set; }

        /// <summary>
        /// <para>IE do Substituto Tributário</para>
        /// <para>Tag IEST</para>
        /// </summary>
        public string IeSt { get; set; }

        /// <summary>
        /// <para>Inscrição Municipal</para>
        /// <para>Tag IM</para>
        /// </summary>
        public string IM { get; set; }

        /// <summary>
        /// <para>Email</para>
        /// <para>Tag email</para>
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Código de Regime Tributário
        /// </summary>
        public string CRT { get; set; }

        /// <summary>
        /// Linha 1 do Endereço
        /// </summary>
        public string EnderecoLinha1
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(EnderecoLogadrouro);
                if (!string.IsNullOrWhiteSpace(EnderecoNumero)) sb.Append(", ").Append(EnderecoNumero);
                if (!string.IsNullOrWhiteSpace(EnderecoComplemento)) sb.Append(" - ").Append(EnderecoComplemento);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Linha 1 do Endereço
        /// </summary>
        public string EnderecoLinha2 => $"{EnderecoBairro} - CEP: {Formatador.FormatarCEP(EnderecoCep)}";


        /// <summary>
        /// Linha 3 do Endereço
        /// </summary>
        public string EnderecoLinha3
        {
            get
            {
                StringBuilder sb = new StringBuilder()
                    .Append(Municipio).Append(" - ").Append(EnderecoUf);

                if (!string.IsNullOrWhiteSpace(Telefone))
                    sb.Append(" Fone: ").Append(Formatador.FormatarTelefone(Telefone));

                return sb.ToString();
            }
        }

    }
}
