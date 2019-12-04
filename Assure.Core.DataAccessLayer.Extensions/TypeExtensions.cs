using Dapper.Contrib.Extensions;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class TypeExtensions
    {
        public static string GetDapperTableName(this Type type)
        {
            var info = type;
            var tableAttrName =
                info.GetCustomAttribute<TableAttribute>(false)?.Name
                ?? (info.GetCustomAttributes(false).FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            string name;
            if (tableAttrName != null)
            {
                name = tableAttrName;
            }
            else
            {
                name = type.Name + "s";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }

            return name;
        }
    }
}
