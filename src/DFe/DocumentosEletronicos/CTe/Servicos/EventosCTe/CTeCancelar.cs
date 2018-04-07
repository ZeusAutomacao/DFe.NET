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

using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Flags;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.CTe.Servicos.Factory;
using CteEletronico = DFe.DocumentosEletronicos.CTe.Classes.Informacoes.CTe;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EventosCTe
{
    public class CTeCancelar
    {
        private readonly DFeConfig _config;
        private readonly CertificadoDigital _certificadoDigital;

        public CTeCancelar(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            _config = config;
            _certificadoDigital = certificadoDigital;
        }

        public retEventoCTe Cancelar(string chave, string cnpjEmitente, int sequenciaEvento, string numeroProtocolo, string justificativa)
        {
            var eventoCancelar = ClassesFactory.CriaEvCancCTe(justificativa, numeroProtocolo);

            var retorno = new ServicoController().Executar(chave, cnpjEmitente, sequenciaEvento, eventoCancelar, TipoEvento.Cancelamento, _config, _certificadoDigital);

            return retorno;
        }
    }
}