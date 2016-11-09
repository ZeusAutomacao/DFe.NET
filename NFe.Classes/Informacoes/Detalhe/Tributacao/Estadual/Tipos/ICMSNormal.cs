﻿/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos
{
    public abstract class ICMSNormal : ICMSBasico
    {
        private decimal _vBc;
        private decimal _pIcms;
        private decimal _vIcms;

        /// <summary>
        ///     Modalidade de determinação da BC do ICMS
        ///     <para>0 - Margem Valor Agregado (%);</para>
        ///     <para>1 - Pauta (valor);</para>
        ///     <para>2 - Preço Tabelado Máximo (valor);</para>
        ///     <para>3 - Valor da Operação.</para>
        /// </summary>
        public DeterminacaoBaseIcms modBC { get; set; }

        /// <summary>
        ///     Valor da BC do ICMS
        /// </summary>
        public decimal vBC
        {
            get { return _vBc; }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     Alíquota do ICMS
        /// </summary>
        public decimal pICMS
        {
            get { return _pIcms; }
            set { _pIcms = value.Arredondar(4); }
        }

        /// <summary>
        ///     Valor do ICMS
        /// </summary>
        public decimal vICMS
        {
            get { return _vIcms; }
            set { _vIcms = value.Arredondar(2); }
        }
    }
}