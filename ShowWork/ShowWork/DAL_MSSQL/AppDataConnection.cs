using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

public class AppDataConnection : DataConnection
{
    public AppDataConnection(DataOptions<AppDataConnection> options)
        : base(options.Options)
    {

    }
}