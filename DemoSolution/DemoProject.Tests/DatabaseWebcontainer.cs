using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace DemoProject.Tests;

[TestClass]
public class DatabaseWebcontainer
{
    // this is called for each test, since each test
    // instantiates a new class instance
    private readonly PostgreSqlContainer container =
        new PostgreSqlBuilder()
        .Build();
    private string connectionString = string.Empty;

    [TestMethod]
    public async Task Database_Can_Run_Query()
    {
        await container.StartAsync();
        connectionString = container.GetConnectionString();

        await using NpgsqlConnection connection = new(connectionString);
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT '1';";
        var result = command.ExecuteScalar();
        

        //const int expected = 1;
        //var actual = await connection..QueryFirstAsync<int>("SELECT 1");
        Assert.AreEqual("1", result);
    }
}
