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
    public class Endereco
    {
        #region Propriedades

        public string NomeEnd { get; }

        public string Bairro { get; }

        public string Cidade { get; }

        public string Num { get; }

        public string Cep { get; }

        public string Uf { get; }

        public string Tel { get; }

        public string Estado { get; }

        #endregion

        #region Construtor

        /// <summary>
        /// </summary>
        /// <param name="nomeEnd">Endereço</param>
        /// <param name="bairro">Bairro</param>
        /// <param name="cidade">Cidade</param>
        /// <param name="num">Numero</param>
        /// <param name="cep">CEP</param>
        /// <param name="uf">UF</param>
        /// <param name="tel">Telefone</param>
        /// <param name="estado">Estado</param>
        public Endereco(string nomeEnd, string bairro, string cidade, string num, string cep, string uf, string tel, string estado)
        {
            NomeEnd = nomeEnd;
            Bairro = bairro;
            Cidade = cidade;
            Num = num;
            Cep = cep;
            Uf = uf;
            Tel = tel;
            Estado = estado;
        }

        #endregion
    }
}