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

using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Servicos.Factory;

namespace CTe.Servicos.Eventos
{
    public class EventoDesacordo
    {
        private readonly int _sequenciaEvento;
        private readonly string _cnpj;
        private readonly string _chave;
        private readonly string _indicadorDesacordo;
        private readonly string _observacao;

        public eventoCTe EventoEnviado { get; private set; }
        public retEventoCTe RetornoSefaz { get; private set; }

        public EventoDesacordo(int sequenciaEvento, string chave, string cnpj, string indicadorDesacordo, string observacao)
        {
            _chave = chave;
            _cnpj = cnpj;
            _sequenciaEvento = sequenciaEvento;
            _indicadorDesacordo = indicadorDesacordo;
            _observacao = observacao;
        }

        /// <summary>
        /// Gera o evento de desacordo de CTe
        /// </summary>
        /// <param name="configuracaoServico"></param>
        /// <param name="orgaoEmissor">Sempre considera a UF que gerou o xml. Então a empresa pode estar configurada para uma UF X e gerar o desacordo de um xml gerado na UF Y, sendo o evento, portanto, enviado para UF Y</param>
        /// <returns></returns>
        public retEventoCTe Discordar(ConfiguracaoServico configuracaoServico = null, DFe.Classes.Entidades.Estado? orgaoEmissor = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var eventoDiscordar = ClassesFactory.CriaEvPrestDesacordo(_indicadorDesacordo, _observacao);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configServico, orgaoEmissor);
            RetornoSefaz = new ServicoController().Executar(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configServico, orgaoEmissor);
            return RetornoSefaz;
        }

        /// <summary>
        /// Gera o evento de desacordo de CTe de forma assíncrona
        /// </summary>
        /// <param name="configuracaoServico"></param>
        /// <param name="orgaoEmissor">Sempre considera a UF que gerou o xml. Então a empresa pode estar configurada para uma UF X e gerar o desacordo de um xml gerado na UF Y, sendo o evento, portanto, enviado para UF Y</param>
        /// <returns></returns>
        public async Task<retEventoCTe> DiscordarAsync(ConfiguracaoServico configuracaoServico = null, DFe.Classes.Entidades.Estado? orgaoEmissor = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var eventoDiscordar = ClassesFactory.CriaEvPrestDesacordo(_indicadorDesacordo, _observacao);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configServico, orgaoEmissor);
            RetornoSefaz = await new ServicoController().ExecutarAsync(CTeTipoEvento.Desacordo, _sequenciaEvento, _chave, _cnpj, eventoDiscordar, configServico, orgaoEmissor);
            return RetornoSefaz;
        }
    }
}