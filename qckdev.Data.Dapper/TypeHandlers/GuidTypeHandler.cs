using Dapper;
using System;
using System.Data;

namespace qckdev.Data.Dapper.TypeHandler
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            // Dapper may pass a Guid instead of a string
            return value is Guid guid ? guid : new Guid((string)value);
        }
    }
}
