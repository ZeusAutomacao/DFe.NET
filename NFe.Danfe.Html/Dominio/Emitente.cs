// ===================================================================
//  Empresa: DSBR - Empresa de Desenvolvimento de Sistemas
//  Projeto: DSBR - Automação Comercial
//  Autores:  Valnei Filho, Vagner Marcelo
//  E-mail: dsbrbrasil@yahoo.com.br
//  Data Criação: 10/04/2020
//  Todos os direitos reservados
// ===================================================================


namespace NFe.Danfe.Html.Dominio
{
    public class Emitente
    {
        #region Propriedades

        /// <summary>
        ///     Nome
        /// </summary>
        public string Nome { get; }

        /// <summary>
        ///     Inscrição estadual
        /// </summary>
        public string Ie { get; }

        /// <summary>
        ///     Documento CPF ou CNPJ
        /// </summary>
        public string CnpjCpf { get; }

        /// <summary>
        ///     Logo empresa Base64
        /// </summary>
        public string Logo { get; }

        public Endereco Endereco { get; }

        #endregion

        #region Construtor

        /// <summary>
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="ie">Inscrição estadual</param>
        /// <param name="cnpjCpf">CPF ou CNPJ</param>
        /// <param name="logo">Imagem em base64</param>
        /// <param name="endereco">Endereço</param>
        public Emitente(string nome, string ie, string cnpjCpf, string logo, Endereco endereco)
        {
            Nome = nome;
            Ie = ie;
            CnpjCpf = cnpjCpf;
            Logo = logo;
            Endereco = endereco;
        }

        #endregion
    }
}