﻿using System.Xml.Serialization;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCTeNormal.infModals;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Tipos;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.InfCTeNormal
{
    public class infModalOs
    {
        [XmlAttribute]
        public versaoModal versaoModal { get; set; }

        [XmlElement("rodoOS", typeof(rodoOS), Namespace = "http://www.portalfiscal.inf.br/cte")]
        public ContainerModal ContainerModal { get; set; }
    }
}