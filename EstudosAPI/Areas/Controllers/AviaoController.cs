using Dapper;
using EstudosAPI.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EstudosAPI.Areas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AviaoController : ControllerBase
{
    private readonly IConfiguration _config;
    public AviaoController(IConfiguration config)
    {
        _config = config;
    }

    // SELECIONA TODOS OS AVIOES
    [HttpGet("SelecionarTodosOsAvioes")]
    public async Task<ActionResult<List<Aviao>>> SelecionarTodosAvioes()
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var air = await connection.QueryAsync("SELECT * FROM Aviao");
        return Ok(air);
    }

    //SELECIONAR AVIOES ESPECIFICOS
    [HttpGet("SelecionarAviao")]
    public async Task<ActionResult<Aviao>> SelecionaAvioes(int idAviao)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var air = await connection.QueryFirstAsync("SELECT * FROM Aviao WHERE idAviao = @id", new { Id = idAviao });
        return Ok(air);
    }

    //CRIAR NOVOS AVIOES
    [HttpPost("CadastrarAviao")]
    public async Task<ActionResult<Aviao>> CriaAvioes(Aviao aviao)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync($@"INSERT INTO Aviao(
                                                               marcaAviao, 
                                                               modeloAviao,
                                                               motorizacaoAviao, 
                                                               tipoOperacaoAviao,
                                                               idCompania,
                                                               InscricaoAviao
                                                              )             
                                                        VALUES(
                                                                @MarcaAviao, 
                                                                @ModeloAviao, 
                                                                @MotorizacaoAviao, 
                                                                @TipoOperacaoAviao, 
                                                                @IdCompania
                                                                @InscricaoAviao
                                                               )", aviao);

        return Ok(await SelecionarTodosAvioes(connection));
    }

    //ATUALIZAR AVIOES EXISTENTES
    [HttpPut("AtualizarAviao")]
    public async Task<ActionResult<Aviao>> AtualizaAvioes(Aviao aviao)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync($@"UPDATE Aviao SET 
                                                             marcaAviao = @MarcaAviao, 
                                                             modeloAviao = @ModeloAviao, 
                                                             motorizacaoAviao = @MotorizacaoAviao,
                                                             tipoOperacaoAviao = @TipoOperacaoAviao, 
                                                             idCompania = @IdCompania,
                                                             InscricaoAviao = @InscricaoAviao
                                                       WHERE idAviao = @id", new { Id = aviao.IdAviao });

        return Ok(await SelecionarTodosAvioes(connection));
    }

    // DELETAR UM PILOTO ESPECIFICO
    [HttpDelete("DeletarAviao")]
    public async Task<ActionResult<List<Aviao>>> DeletaAvioes(int idAviao)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync("DELETE FROM Aviao WHERE idAviao = @id", new { Id = idAviao });
        return Ok(await SelecionarTodosAvioes(connection));
    }

    // RETORNA TODOS OS PILOTOS APOS CADA OPERAÇÃO
    private static async Task<IEnumerable<Aviao>> SelecionarTodosAvioes(SqlConnection connection)
    {
        return await connection.QueryAsync<Aviao>("SELECT * FROM Aviao");
    }
}
