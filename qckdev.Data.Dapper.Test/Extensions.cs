using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace qckdev.Data.Dapper.Test
{
    static class Extensions
    {

        public static TDbContext CreateDbContext<TDbContext>(Func<DbContextOptionsBuilder<TDbContext>, DbContextOptionsBuilder<TDbContext>> builder) where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            var options = builder(optionsBuilder).Options;

            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);
        }

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

    }
}
