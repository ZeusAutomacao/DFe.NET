using System;
using System.Reflection;
using System.Text.RegularExpressions;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public static class Extensions
    {
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
