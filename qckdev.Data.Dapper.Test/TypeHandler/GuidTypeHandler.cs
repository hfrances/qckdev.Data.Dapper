using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace qckdev.Data.Dapper.Test.TypeHandler
{
    public class GuidTypeHandler : global::Dapper.SqlMapper.TypeHandler<Guid>
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
