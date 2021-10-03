using DapperBase = Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;

namespace qckdev.Data.Dapper
{

    /// <summary>
    /// Provides additional methos for Dapper mapping.
    /// </summary>
    public static class SqlMapperHelper
    {

        /// <summary>
        ///  Set custom mapping for a specific type.
        /// </summary>
        /// <typeparam name="T">Entity type to map.</typeparam>
        public static void SetMapper<T>()
        {
            SetMapper(typeof(T));
        }

        /// <summary>
        ///  Set custom mapping for a specific type.
        /// </summary>
        /// <param name="type">Entity type to map.</param>
        public static void SetMapper(Type type)
        {
            DapperBase.SqlMapper.SetTypeMap(
                type,
                new DapperBase.CustomPropertyTypeMap(
                    type,
                    (t, columnName) =>
                        t.GetProperties().FirstOrDefault(prop =>
                            prop.GetCustomAttributes(inherit: false)
                                .OfType<ColumnAttribute>()
                                .Any(attr => attr.Name == columnName))
                )
            );
        }

    }
}
