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
    public class UnitTest
    {

        static UnitTest()
        {
            SqlMapper.AddTypeHandler(new TypeHandler.GuidTypeHandler());
            SqlMapperHelper.SetMapper<Entities.TestFake>();
        }


        [TestMethod]
        public void TestMethodSqlite()
        {
            const string connectionString = "Data Source=:memory:";

            using (var con = new SqliteConnection(connectionString))
            {
                dynamic rdo;

                con.Open();
                rdo = con.QueryFirst("select SQLITE_VERSION() AS Version");
                Assert.IsNotNull(rdo);
            }
        }

        [TestMethod]
        public void TestMethodQuery()
        {
            const string connectionString = "Data Source=:memory:";

            using (var conn = new SqliteConnection(connectionString))
            {
                using (var context = CreateDbContext<TestDbContext>(conn))
                {
                    bool created;
                    int rdo;

                    context.Database.OpenConnection();

                    try
                    {
                        var entities = new Entities.Test[] {
                            new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 1" },
                            new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 2" }
                        };

                        created = context.Database.EnsureCreated();
                        Assert.IsTrue(created);

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
            }
        }

        [TestMethod]
        public async Task TestMethodQueryAsync()
        {
            const string connectionString = "Data Source=:memory:";

            using (var conn = new SqliteConnection(connectionString))
            {
                using (var context = CreateDbContext<TestDbContext>(conn))
                {
                    bool created;
                    int rdo;

                    await context.Database.OpenConnectionAsync();

                    try
                    {
                        var entities = new Entities.Test[] {
                            new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 1" },
                            new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 2" }
                        };

                        created = await context.Database.EnsureCreatedAsync();
                        Assert.IsTrue(created);

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
            }
        }


        private static TDbContext CreateDbContext<TDbContext>(SqliteConnection connection) where TDbContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TDbContext>();
            var options = builder.UseSqlite(connection).Options;

            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);
        }

    }
}
