using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionEntregaCTe
    {
        [XmlElement(ElementName = "semData")]
        public FusionSemDataCTe SemData { get; set; }

        [XmlElement(ElementName = "comData")]
        public FusionComDataCTe ComData { get; set; }

        [XmlElement(ElementName = "noPeriodo")]
        public FusionNoPeriodoCTe NoPeriodo { get; set; }

        [XmlElement(ElementName = "semHora")]
        public FusionSemHoraCTe SemHora { get; set; }

        [XmlElement(ElementName = "comHora")]
        public FusionComHoraCTe ComHora { get; set; }

        [XmlElement(ElementName = "noInter")]
        public FusionIntervaloHora IntervaloHora { get; set; }
    }

    [Serializable]
    public class FusionSemDataCTe
    {
        [XmlElement(ElementName = "tpPer")]
        public FusionTipoPeriodoDataCTe TipoPeriodoData { get; set; }

        public FusionSemDataCTe()
        {
            TipoPeriodoData = FusionTipoPeriodoDataCTe.SemDataDefinida;
        }
    }

    [Serializable]
    public class FusionComDataCTe
    {
        [XmlElement(ElementName = "tpPer")]
        public FusionTipoPeriodoDataCTe TipoPeriodoData { get; set; }

        [XmlElement(ElementName = "dProg")]
        public string DataProgramada { get; set; }
    }

    [Serializable]
    public class FusionNoPeriodoCTe
    {
        [XmlElement(ElementName = "tpPer")]
        public FusionTipoPeriodoDataCTe TipoPeriodoData { get; set; }

        [XmlElement(ElementName = "dIni")]
        public string DataInicial { get; set; }

        [XmlElement(ElementName = "dFim")]
        public string DataFinal { get; set; }

        public FusionNoPeriodoCTe()
        {
            TipoPeriodoData = FusionTipoPeriodoDataCTe.NoPeriodo;
        }
    }


    [Serializable]
    public class FusionSemHoraCTe
    {
        [XmlElement(ElementName = "tpHor")]
        public FusionTipoPeriodoHoraCTe TipoPeriodoHora { get; set; }

        public FusionSemHoraCTe()
        {
            TipoPeriodoHora = FusionTipoPeriodoHoraCTe.SemHoraDefinida;
        }
    }

    [Serializable]
    public class FusionComHoraCTe
    {
        [XmlElement(ElementName = "tpHor")]
        public FusionTipoPeriodoHoraCTe TipoPeriodoHora { get; set; }

        [XmlElement(ElementName = "hProg")]
        public string HoraProgramada { get; set; }
    }

    [Serializable]
    public class FusionIntervaloHora
    {
        [XmlElement(ElementName = "tpHor")]
        public FusionTipoPeriodoHoraCTe TipoPeriodoHora { get; set; }

        [XmlElement(ElementName = "hIni")]
        public string HoraInicial { get; set; }

        [XmlElement(ElementName = "hFim")]
        public string HoraFinal { get; set; }

        public FusionIntervaloHora()
        {
            TipoPeriodoHora = FusionTipoPeriodoHoraCTe.NoIntervaloDeTempo;
        }
    }
}