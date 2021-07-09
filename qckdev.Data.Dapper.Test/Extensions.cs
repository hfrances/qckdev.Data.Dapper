using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace qckdev.Data.Dapper.Test
{
    static class Extensions
    {

        public static dynamic ToDynamic(this Entities.Test entity)
        {
            return new
            {
                entity.TestId,
                entity.Name,
                entity.Factor
            };
        }

        public static dynamic ToDynamic(this Entities.TestFake entity)
        {
            return new
            {
                TestId = entity.TestIdFake,
                Name = entity.NameFake,
                Factor = entity.FactorFake
            };
        }

        public static dynamic ToDynamic<T>(this T value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var property in TypeDescriptor.GetProperties(value.GetType()).OfType<PropertyDescriptor>())
            {
                expando.Add(property.Name, property.GetValue(value));
            }
            return expando as ExpandoObject;
        }

    }
}
