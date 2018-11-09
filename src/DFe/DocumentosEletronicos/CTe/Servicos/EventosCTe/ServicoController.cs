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
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Classes.Flags;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Evento;
using DFe.DocumentosEletronicos.CTe.Servicos.Eventos;
using DFe.DocumentosEletronicos.CTe.Servicos.EventosCTe.Contratos;
using DFe.DocumentosEletronicos.CTe.Servicos.Factory;
using DFe.DocumentosEletronicos.Flags;
using DFe.Ext;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EventosCTe
{
    public class ServicoController : IServicoController
    {
        public retEventoCTe Executar(string chave, string cnpjEmitente, int sequenciaEvento, EventoContainer container, TipoEvento tipoEvento, DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var evento = FactoryEvento.CriaEvento(chave, cnpjEmitente, tipoEvento, sequenciaEvento, container, config);


            switch (config.VersaoServico)
            {
                case VersaoServico.Versao200:
                    evento.infEvento.ProxydhEvento = evento.infEvento.dhEvento.ParaDataHoraStringSemUtc();
                    break;
                case VersaoServico.Versao300:
                    evento.infEvento.ProxydhEvento = evento.infEvento.dhEvento.ParaDataHoraStringUtc();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            evento.Assina(certificadoDigital, config);
            evento.ValidarSchema(config);
            evento.SalvarXmlEmDisco(config, tipoEvento);

            var webService = WsdlFactory.CriaWsdlCteEvento(config, certificadoDigital);
            var retEventoCTe = webService.Autorizar(evento.CriaXmlRequestWs());

            retEventoCTe.LoadXml(evento);
            retEventoCTe.SalvarXmlEmDisco(config, tipoEvento);

            return retEventoCTe;
        }
    }
}