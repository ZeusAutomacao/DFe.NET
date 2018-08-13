using System;
using System.Reflection;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public static class Extensions
    {
        public static OrigemMercadoria GetIcmsOrig(this ICMSBasico icms)
        {
            return GetPropOrigemMercadoriaValue(icms, "orig");
        }

        public static Csosnicms GetIcmsCsosn(this ICMSBasico icms)
        {
            return GetPropCsosnicmsValue(icms, "CSOSN");
        }

        public static Csticms GetIcmsCst(this ICMSBasico icms)
        {
            return GetPropCsticmsValue(icms, "CST");
        }

        public static decimal GetIcmsPercent(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pICMS");
        }

        public static decimal GetIcmsBcValue(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBC");
        }

        public static decimal GetIcmsBcStValue(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBCST");
        }

        public static decimal GetIcmsDesonValue(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMSDeson");
        }        

        public static decimal GetIcmsValue(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMS");
        }

        public static decimal GetIpiPercent(this IPIBasico ipi)
        {
            return GetPropDecimalValue(ipi, "pIPI");
        }

        public static decimal GetIpiValue(this IPIBasico ipi)
        {
            return GetPropDecimalValue(ipi, "vIPI");
        }

        public static decimal GetPisValue(this PISBasico pis)
        {
            return GetPropDecimalValue(pis, "vPIS");
        }

        public static decimal GetCofinsValue(this COFINSBasico cofins)
        {
            return GetPropDecimalValue(cofins, "vCOFINS");
        }
        
        private static OrigemMercadoria GetPropOrigemMercadoriaValue(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                return (OrigemMercadoria) (property != null ? (OrigemMercadoria?) property.GetValue(instance, null) : OrigemMercadoria.OmNacional);
            }
            catch (Exception)
            {
                return OrigemMercadoria.OmNacional;
            }
        }

        private static Csticms GetPropCsticmsValue(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                return (Csticms) (property != null ? (Csticms?) property.GetValue(instance, null) : Csticms.Cst90);
            }
            catch (Exception)
            {
                return Csticms.Cst90;
            }
        }

        private static Csosnicms GetPropCsosnicmsValue(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                return (Csosnicms) (property != null ? (Csosnicms?) property.GetValue(instance, null) : Csosnicms.Csosn900);
            }
            catch (Exception)
            {
                return Csosnicms.Csosn900;
            }
        }

        private static decimal GetPropDecimalValue(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    return (decimal?)property.GetValue(instance, null) ?? 0M;
                }

                return 0M;
            }
            catch (Exception)
            {
                return 0M;
            }
        }
    }
}
