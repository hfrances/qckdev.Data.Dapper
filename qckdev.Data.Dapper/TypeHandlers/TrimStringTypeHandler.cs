using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace qckdev.Data.Dapper.TypeHandlers
{
    /// <summary>
    /// Applies a <see cref="string.TrimEnd"/> to string values.
    /// </summary>
    public class TrimStringTypeHandler : SqlMapper.TypeHandler<string>
    {

        /// <summary>
        /// Applies <see cref="string.TrimEnd"/> to a database value back to a typed value.
        /// </summary>
        public override string Parse(object value)
        {
            return (value as string)?.TrimEnd();
        }

        /// <summary>
        /// Keeps the value of a parameter before a command executes.
        /// </summary>
        public override void SetValue(IDbDataParameter parameter, string value)
        {
            // No es necesario aplicar Trim al escribir en SQL, sólo al leer.
            parameter.Value = value;
        }

    }
}
