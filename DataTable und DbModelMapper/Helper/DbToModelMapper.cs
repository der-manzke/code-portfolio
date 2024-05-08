using System.Data;
using System.Reflection;

namespace CodePortfolio.Helper
{
    public class DbToModelMapper {
        /// <summary>
        /// Maps List<System.Data.DataTable> onto any given model
        /// </summary>
        /// <param name="dbReturn">list of db table entries. Type of List<Sytem.Data.DataTable></param>
        /// <param name="classOfObject">Type of model to match values onto.</param>
        /// <returns>List of instances of the given model classOfObject</returns>
        public static List<object?> MapDbTableToModel(System.Data.DataTable dbReturn, Type classOfObject)
        {
            List<object?> listToReturn = new List<object?>();
            PropertyInfo[] properties = classOfObject.GetProperties();
            Dictionary<string, PropertyInfo> propertyMap = properties.ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            foreach (DataRow dataRow in dbReturn.Rows)
            {
                object? objectToMap = Activator.CreateInstance(classOfObject);

                foreach (DataColumn dataColumn in dbReturn.Columns)
                {
                    string colName = ToCamelCase(dataColumn.ColumnName);

                    if (propertyMap.TryGetValue(colName, out PropertyInfo? prop) && prop.CanWrite)
                    {
                        var valueToSet = dataRow[dataColumn];
                        if (dataRow[dataColumn] == DBNull.Value)
                            valueToSet = null;

                        prop.SetValue(objectToMap, valueToSet);
                    }
                }
                listToReturn.Add(objectToMap);
            }
            return listToReturn;
        }

        /// <summary>
        /// Transforms string to CamelCase
        /// </summary>
        /// <param name="str">String to transform.</param>
        /// <returns>CamelCase string</returns>
        private static string ToCamelCase(string str)
        {
            var words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
            var leadWord = words[0].ToLower();
            var tailWords = words.Skip(1)
                .Select(word => char.ToUpper(word[0]) + word.Substring(1))
                .ToArray();
            return $"{leadWord}{string.Join(string.Empty, tailWords)}";
        }
    }
}