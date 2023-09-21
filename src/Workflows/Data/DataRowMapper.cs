using System.Data;
using System.Reflection;

namespace Bis.ApplySevenDayProcessAutomation.Workflows.Data;

public class DataRowMapper
{
    public T Map<T>(DataRow source, T target)
    {
        var properties = target.GetType().GetProperties()
            .Where(x => x.CanWrite)
            .Where(x => source.Table.Columns.Contains(GetColumnName(x)));

        foreach (var property in properties)
        {
            try
            {
                property.SetValue(target, GetValue(source, property));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to set value of property {0} for {1}.", property.Name, typeof(T)), ex);
            }
        }
        return target;
    }

    private static object GetValue(DataRow source, PropertyInfo property)
    {
        var columnName = GetColumnName(property);
        var value = source[columnName];

        if (value == DBNull.Value)
        {
            return null;
        }
        if (property.PropertyType.IsEnum)
        {
            return Enum.Parse(property.PropertyType, value.ToString());
        }
        if (property.PropertyType == typeof(bool))
        {
            return Convert.ToBoolean(value);
        }
        if (property.PropertyType == typeof(decimal))
        {
            return Convert.ToDecimal(value);
        }
        return value;
    }

    private static string GetColumnName(PropertyInfo property)
    {
        return property.Name == "Id" ? property.DeclaringType.Name + "Id" : property.Name;
    }
}