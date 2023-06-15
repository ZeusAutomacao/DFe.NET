// ===================================================================
//  Empresa: DSBR - Empresa de Desenvolvimento de Sistemas
//  Projeto: DSBR - Automação Comercial
//  Autores:  Valnei Filho, Vagner Marcelo
//  E-mail: dsbrbrasil@yahoo.com.br
//  Data Criação: 10/04/2020
//  Todos os direitos reservados
// ===================================================================


#region

using System;

#endregion

namespace NFe.Danfe.Html.Dominio
{
    public class Fatura
    {
        #region Propriedades

        public string NumFatura { get; }

        public DateTime DataVencimento { get; }

        public decimal Valor { get; }

        #endregion

        #region Construtor

        /// <summary>
        /// </summary>
        /// <param name="numFatura">Numero da fatura</param>
        /// <param name="dataVencimento">Data do vencimento</param>
        /// <param name="valor">Valor</param>
        public Fatura(string numFatura, DateTime dataVencimento, decimal valor)
        {
            NumFatura = numFatura;
            DataVencimento = dataVencimento;
            Valor = valor;
        }

        #endregion
    }
}