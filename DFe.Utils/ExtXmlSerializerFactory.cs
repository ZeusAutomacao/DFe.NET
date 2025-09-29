using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DFe.Utils {
    
    public abstract class XmlOrderFreeSerializerFactory {
        // Referência https://stackoverflow.com/a/33508815
        readonly static XmlAttributeOverrides overrides = new XmlAttributeOverrides();
        readonly static object locker = new object();
        readonly static Dictionary<Type, XmlSerializer> serializers = new Dictionary<Type, XmlSerializer>();
        static HashSet<string> overridesAdded = new HashSet<string>();

        static void AddOverrideAttributes(Type type, XmlAttributeOverrides overrides) {

            if (type == null || type == typeof(object) || type.IsPrimitive || type == typeof(string) || type == typeof(DateTime)) {
                return;
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                AddOverrideAttributes(type.GetGenericArguments()[0], overrides);
                return;
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)) {
                AddOverrideAttributes(type.GetGenericArguments()[0], overrides);
                return;
            }

            var mask = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
            foreach (var member in type.GetProperties(mask).Cast<MemberInfo>().Union(type.GetFields(mask))) {
                var otTag = $"{type.Name}_{member.Name}";
                if (overridesAdded.Contains(otTag)) {
                    continue;
                }
                XmlAttributes overrideAttr = null;
                foreach (var attr in member.GetCustomAttributes<XmlElementAttribute>()) {
                    overrideAttr = overrideAttr ?? new XmlAttributes();
                    var overrideElt = new XmlElementAttribute(attr.ElementName, attr.Type) { DataType = attr.DataType, ElementName = attr.ElementName, Form = attr.Form, Namespace = attr.Namespace, Type = attr.Type };
                    if(attr.IsNullable) {
                        // Isso aqui deve ter uma logica personalizada no setter, colocar ali em cima causa erro
                        overrideElt.IsNullable = true;
                    }
                    overrideAttr.XmlElements.Add(overrideElt);
                    if(attr.Type != null) {
                        AddOverrideAttributes(attr.Type, overrides);
                    }
                }
                foreach (var attr in member.GetCustomAttributes<XmlArrayAttribute>()) {
                    overrideAttr = overrideAttr ?? new XmlAttributes();
                    overrideAttr.XmlArray = new XmlArrayAttribute { ElementName = attr.ElementName, Form = attr.Form, IsNullable = attr.IsNullable, Namespace = attr.Namespace };
                }
                foreach (var attr in member.GetCustomAttributes<XmlArrayItemAttribute>()) {
                    overrideAttr = overrideAttr ?? new XmlAttributes();
                    overrideAttr.XmlArrayItems.Add(attr);
                }
                foreach (var attr in member.GetCustomAttributes<XmlAnyElementAttribute>()) {
                    overrideAttr = overrideAttr ?? new XmlAttributes();
                    overrideAttr.XmlAnyElements.Add(new XmlAnyElementAttribute { Name = attr.Name, Namespace = attr.Namespace });
                }
                if (overrideAttr != null) {
                    overridesAdded.Add(otTag);
                    overrides.Add(type, member.Name, overrideAttr);
                }
                var mType = (member is PropertyInfo pi ? pi.PropertyType : member is FieldInfo fi ? fi.FieldType : null);
                AddOverrideAttributes(mType, overrides);
            }
        }

        public static XmlSerializer GetSerializer(Type type) {
            if (type == null)
                throw new ArgumentNullException("type");
            lock (locker) {
                XmlSerializer serializer;
                if (!serializers.TryGetValue(type, out serializer)) {
                    AddOverrideAttributes(type, overrides);
                    serializers[type] = serializer = new XmlSerializer(type, overrides);
                }
                return serializer;
            }
        }
    
    }

    public static class TypeExtensions {
        public static IEnumerable<Type> BaseTypesAndSelf(this Type type) {
            while (type != null) {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}
