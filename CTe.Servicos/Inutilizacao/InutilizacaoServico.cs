/********************************************************************************/
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
using System;
using CTe.Utils.Extencoes;
using CTeDLL.Classes.Servicos.Inutilizacao;
using CTeDLL.Servicos.Factory;
using CTeDLL.Utils.Inutilizacao;
using DFe.Classes.Flags;

namespace CTeDLL.Servicos.Inutilizacao
{
    public class ConfigInutiliza
    { 
        public ConfigInutiliza(string cnpj, short serie, long numeroInicial, long numeroFinal, int ano,
            string justificativa, ModeloDocumento modeloDocumento = ModeloDocumento.CTe)
        {
            Cnpj = cnpj;
            Serie = serie;
            NumeroInicial = numeroInicial;
            NumeroFinal = numeroFinal;
            Ano = ano;
            Justificativa = justificativa;
            ModeloDocumento = modeloDocumento;
        }

        public int Ano { get; }
        public string Cnpj { get; }
        public short Serie { get; }
        public long NumeroInicial { get; }
        public long NumeroFinal { get; }
        public string Justificativa { get; }
        public ModeloDocumento ModeloDocumento { get; }
    }

    public class InutilizacaoServico
    {
        private readonly ConfigInutiliza _configInutiliza;

        public InutilizacaoServico(ConfigInutiliza configInutiliza)
        {
            Validacoes(configInutiliza);

            _configInutiliza = configInutiliza;
        }

        public retInutCTe Inutilizar()
        {
            var inutCte = ClassesFactory.CriaInutCTe(_configInutiliza);
            inutCte.Assinar();
            inutCte.ValidarShcema();
            inutCte.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlCteInutilizacao();
            var retornoXml = webService.cteInutilizacaoCT(inutCte.CriaRequestWs());

            var retorno = retInutCTe.LoadXml(retornoXml.OuterXml, inutCte);
            retorno.SalvarXmlEmDisco();

            return retorno;
        }

        private static void Validacoes(ConfigInutiliza configInutiliza)
        {
            if (configInutiliza == null) throw new ArgumentNullException("Preciso de uma configuração de inutilização");

            if (string.IsNullOrEmpty(configInutiliza.Cnpj))
                throw new InvalidOperationException("Para inutilizar a númeração eu preciso do cnpj do emitente");

            if (configInutiliza.Serie <= 0)
                throw new InvalidOperationException("Preciso que a série seja maior que 0");

            if (configInutiliza.NumeroInicial <= 0)
                throw new InvalidOperationException("Preciso que o número inicial seja maior que 0");

            if (configInutiliza.NumeroFinal <= 0)
                throw new InvalidOperationException("Preciso que o número final seja maior que 0");

            if (configInutiliza.NumeroInicial > configInutiliza.NumeroFinal)
                throw new InvalidOperationException("Preciso que o número inicial seja maior que o número final");
        }
    }
}