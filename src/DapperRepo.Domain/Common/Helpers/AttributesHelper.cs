using DapperRepo.Domain.Common.Attributes;
using DapperRepo.Domain.Common.Attributes.Interfaces;
using System.Reflection;

namespace DapperRepo.Domain.Common.Helpers;

public static class AttributesHelper
{
    public static string? GetTableName(Type type) => GetNamedAttribute<TableAttribute>(type);

    public static string? GetColumnName(Type type) => GetNamedAttribute<ColumnAttribute>(type);


    public static string? GetKeyName(Type type)
    {
        var property = GetPropertiesWithAttribute<KeyAttribute>(type).FirstOrDefault();

        if (property == null)
        {
            return string.Empty;
        }

        return property.GetCustomAttribute<KeyAttribute>(false)?.Name;
    }


    private static string? GetNamedAttribute<T>(Type type) where T : Attribute, INamedAttribute
    {
        return type.GetCustomAttribute<T>(false)?.Name;
    }



    private static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type type)
    {
        return type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(T)));
    }
}
