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
    public class Destinatario
    {
        #region Propriedades

        /// <summary>
        ///     Nome
        /// </summary>
        public string Nome { get; }

        /// <summary>
        ///     /
        /// </summary>
        public string CnpjCpf { get; }

        /// <summary>
        ///     Inscrição estadual
        /// </summary>
        public string Ie { get; }

        public Endereco Endereco { get; }

        #endregion

        #region Construtor

        /// <summary>
        ///     NFe
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="cnpjCpf">CPF ou CNPJ</param>
        /// <param name="ie">Inscrição estadual</param>
        /// <param name="endereco">Endereço</param>
        public Destinatario(string nome, string cnpjCpf, string ie, Endereco endereco)
        {
            Nome = nome;
            CnpjCpf = cnpjCpf;
            Ie = ie;
            Endereco = endereco;
        }

        /// <summary>
        ///     NFCe
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="cnpjCpf">CPF ou CNPJ</param>
        /// <param name="ie">Inscrição estadual</param>
        /// <param name="endereco">Endereço</param>
        public Destinatario(string nome, string cnpjCpf, Endereco endereco)
        {
            Nome = nome;
            CnpjCpf = cnpjCpf;
            Endereco = endereco;
        }

        #endregion
    }
}