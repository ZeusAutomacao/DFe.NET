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
using System.IO;
using System.Reflection;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.DistribuicaoDFe;
using CTe.Servicos.Factory;
using CTe.Utils.DistribuicaoDFe;
using CTe.Utils;
using DFe.Utils;


namespace CTe.Servicos.DistribuicaoDFe
{
    public class ServicoCTeDistribuicaoDFe
    {
        /// <summary>
        /// Serviço destinado à distribuição de informações resumidas e documentos fiscais eletrônicos de interesse de um ator, seja este pessoa física ou jurídica.
        /// </summary>
        /// <param name="ufAutor">Código da UF do Autor</param>
        /// <param name="documento">CNPJ/CPF do interessado no DF-e</param>
        /// <param name="ultNSU">Último NSU recebido pelo Interessado</param>
        /// <param name="nSU">Número Sequencial Único</param>
        /// <returns>Retorna um objeto da classe CTeDistDFeInteresse com os documentos de interesse do CNPJ/CPF pesquisado</returns>
        public RetornoCteDistDFeInt CTeDistDFeInteresse(string ufAutor, string documento, string ultNSU = "0", string nSU = "0")
        {
            var versaoServico = ConfiguracaoServico.Instancia.VersaoLayout;

            #region Cria o objeto wdsl para consulta

            var ws = WsdlFactory.CriaWsdlCTeDistDFeInteresse();

            #endregion

            #region Cria o objeto distCTeInt

            var pedDistDFeInt = new distDFeInt
            {
                versao = "1.00",
                tpAmb = ConfiguracaoServico.Instancia.tpAmb,
                cUFAutor = ConfiguracaoServico.Instancia.cUF
            };

            if (documento.Length == 11)
                pedDistDFeInt.CPF = documento;
            if (documento.Length > 11)
                pedDistDFeInt.CNPJ = documento;


            pedDistDFeInt.distNSU = new distNSU { ultNSU = ultNSU.PadLeft(15, '0') };

            if (!nSU.Equals("0"))
            {
                pedDistDFeInt.consNSU = new consNSU { NSU = nSU.PadLeft(15, '0') };
                pedDistDFeInt.distNSU = null;
            }

            #endregion

            #region Valida, Envia os dados e obtém a resposta


            pedDistDFeInt.ValidaSchema();

            var xmlConsulta = pedDistDFeInt.ObterXmlString();

            var dadosConsulta = new XmlDocument();
            dadosConsulta.LoadXml(xmlConsulta);

            string path = DateTime.Now.ParaDataHoraString() + "-ped-DistDFeInt.xml";

            SalvarArquivoXml(path, xmlConsulta);

            XmlNode retorno = ws.Execute(dadosConsulta);

            var retornoXmlString = retorno.OuterXml;

            var retConsulta = new retDistDFeInt().CarregarDeXmlString(retornoXmlString);

            SalvarArquivoXml(DateTime.Now.ParaDataHoraString() + "-distDFeInt.xml", retornoXmlString);

            #region Obtém um retDistDFeInt de cada evento e salva em arquivo

            if (retConsulta.loteDistDFeInt != null)
            {
                for (int i = 0; i < retConsulta.loteDistDFeInt.Length; i++)
                {
                    string conteudo = Compressao.Unzip(retConsulta.loteDistDFeInt[i].XmlNfe);
                    string chCTe = string.Empty;

                    if (conteudo.StartsWith("<cteProc"))
                    {
                        var retConteudo = FuncoesXml.XmlStringParaClasse<CTe.Classes.cteProc>(conteudo);
                        chCTe = retConteudo.protCTe.infProt.chCTe;
                    }
                    else if (conteudo.StartsWith("<procEventoCTe"))
                    {
                        var procEventoNFeConteudo = FuncoesXml.XmlStringParaClasse<Classes.Servicos.DistribuicaoDFe.Schemas.procEventoCTe>(conteudo);
                        chCTe = procEventoNFeConteudo.eventoCTe.infEvento.chCTe;
                    }

                    string[] schema = retConsulta.loteDistDFeInt[i].schema.Split('_');
                    if (chCTe == string.Empty)
                        chCTe = DateTime.Now.ParaDataHoraString() + "_SEMCHAVE";

                    SalvarArquivoXml(chCTe + "-" + schema[0] + ".xml", conteudo);
                }
            }

            #endregion

            return new RetornoCteDistDFeInt(pedDistDFeInt.ObterXmlString(), retConsulta.ObterXmlString(), retornoXmlString, retConsulta);

            #endregion
        }

        private void SalvarArquivoXml(string nomeArquivo, string xmlString)
        {
            if (!ConfiguracaoServico.Instancia.IsSalvarXml) return;
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var dir = string.IsNullOrEmpty(ConfiguracaoServico.Instancia.DiretorioSalvarXml) ? path : ConfiguracaoServico.Instancia.DiretorioSalvarXml;
            var stw = new StreamWriter(dir + @"\" + nomeArquivo);
            stw.WriteLine(xmlString);
            stw.Close();
        }

    }
}
