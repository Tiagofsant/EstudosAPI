using Dapper;
using EstudosAPI.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EstudosAPI.Areas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniaController : ControllerBase
{
    private readonly IConfiguration _config;
    public CompaniaController(IConfiguration config)
    {
        _config = config;
    }

    // SELECIONA TODOS AS COMPANIAS
    [HttpGet("SelecionarTodasAsCompanias")]
    public async Task<ActionResult<List<Compania>>> SelecionarTodosCompania()
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var comp = await connection.QueryAsync("SELECT * FROM Compania");
        return Ok(comp);
    }

    //SELECIONAR COMPANIAS ESPECIFICAS
    [HttpGet("SelecionarCompania")]
    public async Task<ActionResult<Compania>> SelecionaCompania(int idCompania)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var comp = await connection.QueryFirstAsync("SELECT * FROM Compania WHERE idCompania = @id", new { Id = idCompania });
        return Ok(comp);
    }

    //CRIAR NOVOS COMPANIAS
    [HttpPost("CadastrarCompania")]
    public async Task<ActionResult<Compania>> CriaCompania(Compania compania)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync($@"INSERT INTO Compania 
                                                                 (
                                                                    nomeCompania, 
                                                                    tipoVooCompania, 
                                                                    horarioAtendimentoCompania, 
                                                                    tipoOperacaoAviaoCompania
                                                                 ) 
                                                          VALUES (
                                                                    @NomeCompania, 
                                                                    @TipoVooCompania, 
                                                                    @HorarioAtendimentoCompania, 
                                                                    @TipoOperacaoAviaoCompania
                                                                 )", compania);

        return Ok(await SelecionarTodosCompania(connection));
    }

    //ATUALIZAR COMPANIAS EXISTENTES
    [HttpPut("AtualizarCompania")]
    public async Task<ActionResult<Compania>> AtualizaCompania(Compania compania)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync($@"UPDATE Compania SET 
                                                                nomeCompania = @Nome, 
                                                                tipoVooCompania = @TipoVoo,
                                                                horarioAtendimentoCompania = @HorarioAtendimento, 
                                                                tipoOperacaoAviaoCompania = @TipoOperacao 
                                                          WHERE idCompania = @IdCompania", compania);

        return Ok(await SelecionarTodosCompania(connection));
    }

    // DELETAR UMA COMPANIA ESPECIFICA
    [HttpDelete("DeletarCompania")]
    public async Task<ActionResult<List<Compania>>> DeletaCompania(int idCompania)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync("DELETE FROM Compania WHERE idCompania = @id", new { Id = idCompania });
        return Ok(await SelecionarTodosCompania(connection));
    }

    // RETORNA TODOS OS PILOTOS APOS CADA OPERAÇÃO
    private static async Task<IEnumerable<Compania>> SelecionarTodosCompania(SqlConnection connection)
    {
        return await connection.QueryAsync<Compania>("SELECT * FROM Compania");
    }
}

