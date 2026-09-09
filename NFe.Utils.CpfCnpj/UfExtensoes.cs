/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

using System;
using DFe.Classes.Entidades;

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Conversao de sigla de UF para o enum <see cref="Estado" />. Util no caminho do emitente
    ///     (<c>enderEmit.UF</c> e enum, enquanto <c>enderDest.UF</c> e string).
    /// </summary>
    public static class UfExtensoes
    {
        /// <summary>
        ///     Converte uma sigla de UF ("MG", "sp") no enum <see cref="Estado" />.
        /// </summary>
        /// <exception cref="ArgumentException">Sigla desconhecida.</exception>
        public static Estado ParaEstado(string sigla)
        {
            Estado estado;
            if (TentarParaEstado(sigla, out estado))
                return estado;
            throw new ArgumentException(string.Format("Sigla de UF desconhecida: '{0}'.", sigla), nameof(sigla));
        }

        /// <summary>
        ///     Tenta converter uma sigla de UF no enum <see cref="Estado" />.
        /// </summary>
        public static bool TentarParaEstado(string sigla, out Estado estado)
        {
            estado = default(Estado);
            if (string.IsNullOrWhiteSpace(sigla))
                return false;
            return Enum.TryParse(sigla.Trim().ToUpperInvariant(), true, out estado) && Enum.IsDefined(typeof(Estado), estado);
        }
    }
}
