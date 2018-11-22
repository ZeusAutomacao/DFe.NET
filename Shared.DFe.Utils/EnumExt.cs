using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DFe.Utils
{
    public static class EnumExt
    {
        /// <summary>
        /// Função de extensão de Enums. 
        /// Obtém um atributo associado ao Enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ObterAtributo<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }

        /// <summary>
        /// Função de extensão de Enums. 
        /// Obtém a descrição definida no atributo [Description("xx")] para o Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Descricao(this Enum value)
        {
            var attribute = value.ObterAtributo<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Função de extensão de Enums. 
        /// Obtém a XmlEnum definida no atributo [XmlEnum("xx")]para o Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string XmlDescricao(this Enum value)
        {
            var attribute = value.ObterAtributo<XmlEnumAttribute>();
            return attribute == null ? value.ToString() : attribute.Name.ToString();
        }
    }
}
