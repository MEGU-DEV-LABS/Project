using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DbWebApplication.Enum;

namespace DbWebApplication.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Faculty enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? enumValue.ToString();
    }
}
