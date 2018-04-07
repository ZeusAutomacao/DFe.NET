﻿/********************************************************************************/
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

using System;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Evento;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Evento.Flags;
using DFe.Ext;

namespace DFe.DocumentosEletronicos.MDFe.Servicos.EventosMDFe
{
    public static class FactoryEvento
    {
        public static eventoMDFe CriaEvento(string chave, string cnpjEmitente, tpEvento tipoEvento, byte sequenciaEvento, MDFeEventoContainer evento, DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var eventoMDFe = new eventoMDFe
            {
                versao = dfeConfig.VersaoServico,
                infEvento = new infEventoEnv
                {
                    Id = "ID" + (long)tipoEvento + chave + sequenciaEvento.ToString("D2"),
                    tpAmb = dfeConfig.TipoAmbiente,
                    CNPJ = cnpjEmitente,
                    cOrgao = dfeConfig.EstadoUf,
                    chMDFe = chave,
                    detEvento = new detEvento
                    {
                        VersaoServico = dfeConfig.VersaoServico,
                        EventoContainer = evento
                    },
                    dhEvento = DateTime.Now,
                    nSeqEvento = sequenciaEvento,
                    tpEvento = tipoEvento
                }
            };

            switch (dfeConfig.VersaoServico)
            {
                case VersaoServico.Versao100:
                    eventoMDFe.infEvento.ProxydhEvento = eventoMDFe.infEvento.dhEvento.ParaDataHoraStringSemUtc();
                    break;
                case VersaoServico.Versao300:
                    eventoMDFe.infEvento.ProxydhEvento = eventoMDFe.infEvento.dhEvento.ParaDataHoraStringUtc();
                    break;
            }

            eventoMDFe.Assinar(certificadoDigital);

            return eventoMDFe;
        }
    }
}