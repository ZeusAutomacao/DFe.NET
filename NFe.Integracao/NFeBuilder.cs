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
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using NFe.Classes;
using NFe.Classes.Servicos.Autorizacao;
using NFe.Integracao.Enums;

namespace NFe.Integracao
{
    /// <summary>
    /// Classe responsável por desserializar os arquivos xml em classes do Zeus.
    /// Inicialmente a única fonte de dados suportada são arquivos xml. Futuramente pode-se extender a classe para suportar outras fontes.
    /// </summary>
    public class NFeBuilder
    {
        private readonly IList<Classes.NFe> _nFe;

        public NFeBuilder(string pathArquivoXml, TipoXmlNFe tipo)
        {
            _nFe = new List<Classes.NFe>();

            try
            {
                var reader = new StringReader(File.ReadAllText(pathArquivoXml));
                switch (tipo)
                {
                    case TipoXmlNFe.Destinatario:
                    {
                        var desserializador = new XmlSerializer(typeof(nfeProc));
                        _nFe.Add(((nfeProc)desserializador.Deserialize(reader)).NFe);
                    }
                        break;
                    case TipoXmlNFe.Lote:
                    {
                        var desserializador = new XmlSerializer(typeof(enviNFe3));
                        _nFe = ((enviNFe3)desserializador.Deserialize(reader)).NFe;
                    }
                        break;
                    default:
                    {
                        var desserializador = new XmlSerializer(typeof(Classes.NFe));
                        _nFe.Add(((Classes.NFe)desserializador.Deserialize(reader)));
                    }
                        break;
                }
            }
            catch(Exception ex)
            {
                throw new InvalidDataException("Arquivo XML inválido.", ex);
            }
        }

        /// <summary>
        /// Método responsável por montar uma NF-e.
        /// </summary>
        /// <returns>Retorna uma das notas presentes em "this.NFe" removendo a mesma da listagem.</returns>
        public Classes.NFe Build()
        {
            if (_nFe.Count <= 0)
                throw new InvalidOperationException("Não existem informações para a montagem da classe");
            var nfe = _nFe[_nFe.Count - 1];
            _nFe.Remove(nfe);
            return nfe;
        }

        /// <summary>
        /// Monta uma lista de NF-e.
        /// </summary>
        /// <returns>Retorna todas as NF-e´s presentes em "this.NFe"</returns>
        public IList<Classes.NFe> BuildAll()
        {
            if (_nFe.Count <= 0)
                throw new InvalidOperationException("Não existem informações para a montagem da classe");
            return _nFe;
        }
    }
}