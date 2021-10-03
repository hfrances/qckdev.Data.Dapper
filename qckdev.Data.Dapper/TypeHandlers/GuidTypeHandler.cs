using Dapper;
using System;
using System.Data;

namespace qckdev.Data.Dapper.TypeHandlers
{

    /// <summary>
    /// Converts to <see cref="Guid"/> for those databases which not supports this type natively.
    /// </summary>
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {

        /// <summary>
        /// Assign the value of a parameter before a command executes.
        /// </summary>
        /// <param name="value">Parameter value.</param>
        /// <returns></returns>
        public override Guid Parse(object value)
        {
            // Dapper may pass a Guid instead of a string
            return value is Guid guid ? guid : new Guid((string)value);
        }

        /// <summary>
        /// Parse a database value back to a <see cref="Guid"/> value.
        /// </summary>
        /// <param name="parameter">The value from the database.</param>
        /// <param name="guid">The typed value.</param>
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

    }
}
