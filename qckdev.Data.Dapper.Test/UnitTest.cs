using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace qckdev.Data.Dapper.Test
{
    [TestClass]
    public sealed class UnitTest
    {

        static UnitTest()
        {
            SqlMapper.AddTypeHandler(new TypeHandlers.GuidTypeHandler());
            SqlMapper.AddTypeHandler(new TypeHandlers.TrimStringTypeHandler());
            SqlMapperHelper.SetMapper<Entities.TestFake>();
        }


        [TestMethod]
        public void TestMethodSqliteTest()
        {
            const string connectionString = "Data Source=:memory:";
            using var conn = new SqliteConnection(connectionString);
            dynamic rdo;

            conn.Open();
            rdo = conn.QueryFirst("select SQLITE_VERSION() AS Version");
            Assert.IsNotNull(rdo);
        }

        [TestMethod]
        public void TestMethodQueryTest()
        {
            const string connectionString = "Data Source=:memory:";
            using var conn = new SqliteConnection(connectionString);
            using var context = Extensions.CreateDbContext<TestDbContext>(
                builder => builder.UseSqlite(conn)
            );
            bool created;
            int rdo;

            context.Database.OpenConnection();

            try
            {
                var entities = new Entities.Test[] {
                    new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 1" },
                    new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 2" }
                };

                // Create schema.
                created = context.Database.EnsureCreated();
                Assert.IsTrue(created);

                // Initialize data.
                context.Tests.AddRange(entities);
                rdo = context.SaveChanges();
                Assert.AreEqual(entities.Length, rdo);

                // Check if data is created properly.
                var fromContext = context.Tests;
                CollectionAssert.AreEqual(
                    entities.Select(x => x.ToDynamic()).ToArray(),
                    fromContext.Select(x => x.ToDynamic()).ToArray(),
                    "Failed checking if data has been created properly"
                );

                // Check dapper functionality.
                var fromDapper = conn.Query<Entities.Test>("SELECT * FROM Tests");
                CollectionAssert.AreEqual(
                    entities.Select(x => x.ToDynamic()).ToArray(),
                    fromDapper.Select(x => x.ToDynamic()).ToArray(),
                    "Failed checking dapper functionality"
                );

                // Check dapper functionality with column mapping.
                var fromDapperFake = conn.Query<Entities.TestFake>("SELECT * FROM Tests");
                CollectionAssert.AreEqual(
                    entities.Select(x => x.ToDynamic()).ToArray(),
                    fromDapperFake.Select(x => x.ToDynamic()).ToArray(),
                    "Failed checking daper functionality with column mapping"
                );
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        [TestMethod]
        public async Task TestMethodQueryTestAsync()
        {
            const string connectionString = "Data Source=:memory:";
            using var conn = new SqliteConnection(connectionString);
            using var context = Extensions.CreateDbContext<TestDbContext>(
                builder => builder.UseSqlite(conn)
            );
            bool created;
            int rdo;

            await context.Database.OpenConnectionAsync();

            try
            {
                var entities = new Entities.Test[] {
                    new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 1" },
                    new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 2" }
                };

                // Create schema.
                created = await context.Database.EnsureCreatedAsync();
                Assert.IsTrue(created);

                // Initialize data.
                await context.Tests.AddRangeAsync(entities);
                rdo = await context.SaveChangesAsync();
                Assert.AreEqual(entities.Length, rdo);

                // Check if data is created properly.
                var fromContext = await context.Tests.ToArrayAsync();
                CollectionAssert.AreEqual(
                    entities.Select(x => x.ToDynamic()).ToArray(),
                    fromContext.Select(x => x.ToDynamic()).ToArray(),
                    "Failed checking if data has been created properly"
                );

                // Check dapper functionality.
                var fromDapper = await conn.QueryAsync<Entities.Test>("SELECT * FROM Tests");
                CollectionAssert.AreEqual(
                    entities.Select(x => x.ToDynamic()).ToArray(),
                    fromDapper.Select(x => x.ToDynamic()).ToArray(),
                    "Failed checking dapper functionality"
                );

                // Check dapper functionality with column mapping.
                var fromDapperFake = await conn.QueryAsync<Entities.TestFake>("SELECT * FROM Tests");
                CollectionAssert.AreEqual(
                    entities.Select(x => x.ToDynamic()).ToArray(),
                    fromDapperFake.Select(x => x.ToDynamic()).ToArray(),
                    "Failed checking daper functionality with column mapping"
                );
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }

        [TestMethod]
        public async Task TrimEndStringTypeHandlerTestAsync()
        {
            const string connectionString = "Data Source=:memory:";
            using var conn = new SqliteConnection(connectionString);
            using var context = Extensions.CreateDbContext<TestDbContext>(
                builder => builder.UseSqlite(conn)
            );
            bool created;
            int rdo;

            await context.Database.OpenConnectionAsync();

            try
            {
                var entity = new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 1", Spaced = "Value with spaces   " };

                // Create schema.
                created = await context.Database.EnsureCreatedAsync();
                Assert.IsTrue(created);

                // Initialize data.
                await context.Tests.AddAsync(entity);
                rdo = await context.SaveChangesAsync();
                Assert.AreEqual(1, rdo);

                // Check if data is created properly.
                var fromContext = await context.Tests.SingleOrDefaultAsync();
                Assert.AreEqual(entity, fromContext,
                    "Failed checking if data has been created properly"
                );

                // Check dapper functionality.
                var fromDapper = await conn.QuerySingleAsync<Entities.Test>("SELECT * FROM Tests WHERE TestId = @id", new { id = entity.TestId });
                Assert.AreEqual(
                    entity.Spaced.TrimEnd(), fromDapper.Spaced,
                    "Failed checking dapper functionality"
                );

                // Check dapper functionality with column mapping.
                var fromDapperFake = await conn.QuerySingleAsync<Entities.TestFake>("SELECT * FROM Tests WHERE TestID = @id", new { id = entity.TestId });
                Assert.AreEqual(
                    entity.Spaced.TrimEnd(), fromDapperFake.SpacedFake,
                    "Failed checking daper functionality with column mapping"
                );
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }

    }
}
