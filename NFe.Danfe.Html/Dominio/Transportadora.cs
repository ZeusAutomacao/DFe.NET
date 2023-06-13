// ===================================================================
//  Empresa: DSBR - Empresa de Desenvolvimento de Sistemas
//  Projeto: DSBR - Automação Comercial
//  Autores:  Valnei Filho, Vagner Marcelo
//  E-mail: dsbrbrasil@yahoo.com.br
//  Data Criação: 10/04/2020
//  Todos os direitos reservados
// ===================================================================


using System.Collections.Generic;

namespace NFe.Danfe.Html.Dominio
{
    public class Transportadora
    {
        #region Propriedades

        public Endereco Endereco { get; }

        /// <summary>
        ///     Documento CPF ou CNPJ
        /// </summary>
        public string CnpjCpf { get; }

        /// <summary>
        ///     Nome
        /// </summary>
        public string Nome { get; }

        /// <summary>
        ///     Placa
        /// </summary>
        public string Placa { get; }

        /// <summary>
        ///     RNTC
        /// </summary>
        public string CodAntt { get; }


        /// <summary>
        ///     Inscrição Estadual
        /// </summary>
        public string Ie { get; }

        /// <summary>
        ///     Responsabilidade pelo frete
        ///     <para>Emitente|Destinatário</para>
        /// </summary>
        public string FretePorConta { get; }

        /// <summary>
        /// Reboque
        /// </summary>
        public List<Reboque> Reboque { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        /// </summary>
        /// <param name="endereco"></param>
        /// <param name="cnpjCpf"></param>
        /// <param name="nome"></param>
        /// <param name="placa"></param>
        /// <param name="codAntt"></param>
        /// <param name="antt"></param>
        /// <param name="ie"></param>
        /// <param name="fretePorConta">{Emitente|Destinatário}</param>
        public Transportadora(Endereco endereco, string cnpjCpf, string nome, string placa, 
                string codAntt, string ie,
                string fretePorConta)
        {
            Endereco = endereco;
            CnpjCpf = cnpjCpf;
            Nome = nome;
            Placa = placa;
            CodAntt = codAntt;
            Ie = ie;
            FretePorConta = fretePorConta;
        }

        #endregion
    }
}