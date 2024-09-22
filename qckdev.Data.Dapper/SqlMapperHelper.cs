using DapperBase = Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.Reflection;

namespace qckdev.Data.Dapper
{

    /// <summary>
    /// Provides additional methos for Dapper mapping.
    /// </summary>
    public static class SqlMapperHelper
    {

        /// <summary>
        /// Mapea los tipos dentro del ensamblado indicado para que Dapper utilice los atributos <see cref="TableAttribute"/> y <see cref="ColumnAttribute"/>
        /// como nombres de tabla y columnas en las consultas SQL que genera.
        /// <br/>
        /// Busca en el ensamblado del tipo indicado.
        /// <br/>
        /// Importante: sólo mapea las clases con el atributo <see cref="TableAttribute"/>.
        /// </summary>
        public static void SetAllMaps<TAnchor>()
        {
            SetAllMaps(typeof(TAnchor));
        }

        /// <summary>
        /// Mapea los tipos dentro del ensamblado indicado para que Dapper utilice los atributos <see cref="TableAttribute"/> y <see cref="ColumnAttribute"/>
        /// como nombres de tabla y columnas en las consultas SQL que genera.
        /// <br/>
        /// Busca en el ensamblado del tipo indicado.
        /// <br/>
        /// Importante: sólo mapea las clases con el atributo <see cref="TableAttribute"/>.
        /// </summary>
        public static void SetAllMaps(Type anchorType)
        {
            var assembly = Assembly.GetAssembly(anchorType);
            SetAllMaps(assembly);
        }

        /// <summary>
        /// Mapea los tipos dentro del ensamblado indicado para que Dapper utilice los atributos <see cref="TableAttribute"/> y <see cref="ColumnAttribute"/>
        /// como nombres de tabla y columnas en las consultas SQL que genera.
        /// <br/>
        /// Importante: sólo mapea las clases con el atributo <see cref="TableAttribute"/>.
        /// </summary>
        public static void SetAllMaps(params Assembly[] assemblies)
        {
            foreach (var ass in assemblies)
            {
                foreach (Type item in ass.GetTypes())
                {
                    if (item.GetCustomAttributes<TableAttribute>().Any())
                    {
                        SetMapper(item);
                    }
                }
            }
        }

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
