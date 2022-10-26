using System.Data.SqlClient;
using System.Data;

namespace EstudosAPI.Dapper;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SqlConnection");
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);

    public IDbConnection CreateConnection(string nomeDoServidor, string nomeDaBase)
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);
        builder.ConnectionString = _connectionString;
        builder["Server"] = nomeDoServidor;
        builder["Database"] = nomeDaBase;

        return new SqlConnection(builder.ConnectionString);
    }
}