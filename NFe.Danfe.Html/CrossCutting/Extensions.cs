using System;
using System.Reflection;
using System.Text.RegularExpressions;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;

namespace NFe.Danfe.Html.CrossCutting
{
    internal  static class Extensions
    {
        public static string orig(this ICMSBasico icms)
        {
            return GetOrigIcms(icms, "orig");
        }

        public static string Csosn(this ICMSBasico icms)
        {
            return GetCsosn(icms, "CSOSN");
        }

        public static string Cst(this ICMSBasico icms)
        {
            return GetCstIcms(icms, "CST");
        }

        public static decimal pICMS(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pICMS");
        }

        public static decimal vBC(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBC");
        }

        public static decimal vBCST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBCST");
        }

        public static decimal vICMSDeson(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMSDeson");
        }

        public static decimal vICMS(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMS");
        }

        public static decimal vBCFCP(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBCFCP");
        }

        public static decimal pFCP(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pFCP");
        }

        public static decimal vFCP(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vFCP");
        }

        public static decimal vBCFCPST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBCFCPST");
        }

        public static decimal pFCPST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pFCPST");
        }

        public static decimal vFCPST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vFCPST");
        }

        public static decimal vBCFCPSTRet(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBCFCPSTRet");
        }

        public static decimal pFCPSTRet(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pFCPSTRet");
        }

        public static decimal vFCPSTRet(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vFCPSTRet");
        }

        public static decimal pST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pST");
        }

        public static string modBC(this ICMSBasico icms)
        {
            return GetModBC(icms, "modBC");
        }

        public static string modBCST(this ICMSBasico icms)
        {
            return GetModBCst(icms, "modBCST");
        }


        public static decimal pMVAST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pMVAST");
        }

        public static decimal pRedBC(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pRedBC");
        }

        public static decimal pRedBCST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pRedBCST");
        }

        public static decimal pICMSST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pICMSST");
        }

        public static decimal vICMSST(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMSST");
        }

        public static decimal vBCSTRet(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBCSTRet");
        }

        public static decimal vICMSSTRet(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMSSTRet");
        }

        public static string motDesICMS(this ICMSBasico icms)
        {
            return GetMotDesnIcms(icms, "motDesICMS");
        }

        public static decimal pBCOp(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pBCOp");
        }

        public static string UFST(this ICMSBasico icms)
        {
            return GetPropStringValue(icms, "UFST");
        }

        public static decimal pCredSN(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pCredSN");
        }

        public static decimal vCredICMSSN(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vCredICMSSN");
        }

        public static decimal vICMSOp(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMSOp");
        }

        public static decimal pDif(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "pDif");
        }

        public static decimal vICMSDif(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMSDif");
        }

        public static decimal vBCSTDest(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vBCSTDest");
        }

        public static decimal vICMSSTDest(this ICMSBasico icms)
        {
            return GetPropDecimalValue(icms, "vICMSSTDest");
        }


        public static string Cst(this IPIBasico ipi)
        {
            return GetCstIpi(ipi, "CST");
        }

        public static decimal vBC(this IPIBasico ipi)
        {
            return GetPropDecimalValue(ipi, "vBC");
        }

        public static decimal pIPI(this IPIBasico ipi)
        {
            return GetPropDecimalValue(ipi, "pIPI");
        }

        public static decimal vIPI(this IPIBasico ipi)
        {
            return GetPropDecimalValue(ipi, "vIPI");
        }

        public static decimal qUnid(this IPIBasico ipi)
        {
            return GetPropDecimalValue(ipi, "qUnid");
        }

        public static decimal vUnid(this IPIBasico ipi)
        {
            return GetPropDecimalValue(ipi, "vUnid");
        }


        public static string Cst(this PISBasico pis)
        {
            return GetCstPis(pis, "CST");
        }

        public static decimal vPIS(this PISBasico pis)
        {
            return GetPropDecimalValue(pis, "vPIS");
        }

        public static decimal vBC(this PISBasico pis)
        {
            return GetPropDecimalValue(pis, "vBC");
        }

        public static decimal pPpis(this PISBasico pis)
        {
            return GetPropDecimalValue(pis, "pPIS");
        }

        public static decimal qBCProd(this PISBasico pis)
        {
            return GetPropDecimalValue(pis, "qBCProd");
        }

        public static decimal vAliqProd(this PISBasico pis)
        {
            return GetPropDecimalValue(pis, "vAliqProd");
        }


        public static decimal vCOFINS(this COFINSBasico cofins)
        {
            return GetPropDecimalValue(cofins, "vCOFINS");
        }

        public static string Cst(this COFINSBasico cofins)
        {
            return GetCstCofins(cofins, "CST");
        }

        public static decimal vBC(this COFINSBasico cofins)
        {
            return GetPropDecimalValue(cofins, "vBC");
        }

        public static decimal pCOFINS(this COFINSBasico cofins)
        {
            return GetPropDecimalValue(cofins, "pCOFINS");
        }

        public static decimal qBCProd(this COFINSBasico cofins)
        {
            return GetPropDecimalValue(cofins, "qBCProd");
        }

        public static decimal vAliqProd(this COFINSBasico cofins)
        {
            return GetPropDecimalValue(cofins, "vAliqProd");
        }


        private static string GetCstCofins(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((CSTCOFINS)property.GetValue(instance, null)).ToString() : "";
                return w != "" ? Regex.Match(w, "[0-9]+").Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetCstIpi(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((CSTIPI)property.GetValue(instance, null)).ToString() : "";
                return w != "" ? Regex.Match(w, "[0-9]+").Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetCstPis(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((CSTPIS)property.GetValue(instance, null)).ToString() : "";
                return w != "" ? Regex.Match(w, "[0-9]+").Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetCstIcms(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((Csticms)property.GetValue(instance, null)).ToString() : "";
                return w != "" ? Regex.Match(w, "[0-9]+").Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetModBCst(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((int)(DeterminacaoBaseIcmsSt)property.GetValue(instance, null)).ToString() : "";
                return w;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetModBC(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((int)(DeterminacaoBaseIcms)property.GetValue(instance, null)).ToString() : "";
                return w;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetMotDesnIcms(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((MotivoDesoneracaoIcms)property.GetValue(instance, null)).ToString() : "";
                return w != "" ? Regex.Match(w, "[0-9]+").Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetCsosn(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((Csosnicms)property.GetValue(instance, null)).ToString() : "";
                return w != "" ? Regex.Match(w, "[0-9]+").Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static string GetOrigIcms(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                var w = property != null ? ((int)(OrigemMercadoria)property.GetValue(instance, null)).ToString() : "";
                return w;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static decimal GetPropDecimalValue(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                if (property != null) return (decimal?)property.GetValue(instance, null) ?? 0M;
                return 0M;
            }
            catch (Exception)
            {
                return 0M;
            }
        }

        private static string GetPropStringValue(object instance, string propName)
        {
            try
            {
                var property = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                if (property != null) return (string)property.GetValue(instance, null) ?? "";
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
