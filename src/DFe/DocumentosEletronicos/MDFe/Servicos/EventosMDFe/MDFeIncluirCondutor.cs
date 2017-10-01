/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
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

using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Evento.Flags;
using DFe.DocumentosEletronicos.MDFe.Servicos.Factory;

namespace DFe.DocumentosEletronicos.MDFe.Servicos.EventosMDFe
{
    public class MDFeIncluirCondutor
    {
        public CertificadoDigital CertificadoDigital { get; }
        private readonly DFeConfig _dfeConfig;

        public MDFeIncluirCondutor(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            CertificadoDigital = certificadoDigital;
            _dfeConfig = dfeConfig;
        }

        public retEventoMDFe MDFeEventoIncluirCondutor(string chave, string cnpj, byte sequenciaEvento, string nome, string cpf)
        {
            var incluirCodutor = ClassesFactory.CriaEvIncCondutorMDFe(nome, cpf);

            var retorno = new ServicoController(_dfeConfig, CertificadoDigital).Executar(chave, cnpj, sequenciaEvento, incluirCodutor, tpEvento.InclusaoDeCondutor);

            return retorno;
        }
    }
}