using DapperBase = Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;

namespace qckdev.Data.Dapper
{
    public static class SqlMapperHelper
    {

        public static void SetMapper<T>()
        {
            var type = typeof(T);

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
