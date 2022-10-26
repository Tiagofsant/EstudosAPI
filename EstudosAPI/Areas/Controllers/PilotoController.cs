using AutoWrapper.Wrappers;
using Dapper;
using EstudosAPI.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EstudosAPI.Areas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PilotoController : ControllerBase
{
    private readonly IConfiguration _config;
    public PilotoController(IConfiguration config)
    {
        _config = config;
    }

    // SELECIONAR TODOS OS PILOTOS
    [HttpGet("SelecionarTodosOsPilotos")]
    public async Task<ApiResponse> SelecionarTodosPilotos()
    {
        try
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var pilot = await connection.QueryAsync("SELECT * FROM Piloto");

            return new ApiResponse($"Requisição feita como sucesso", pilot);
        }
        catch
        {
            throw new ApiException($"Impossível carregar os dados, verifique sua conexão com o o banco de dados e tente novamente.");
        }
    }

    //SELECIONAR PILOTOS ESPECIFICOS
    [HttpGet("SelecionarPilotosEspecicos")]
    public async Task<ApiResponse> SelecionaPilotos(int idPiloto)
    {
        try
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var pilot = await connection.QueryFirstAsync("SELECT * FROM Piloto WHERE idPiloto = @id", new { Id = idPiloto });

            return new ApiResponse($"Sucesso na Requisição", pilot);
        }
        catch
        {
            throw new ApiException("Erro: Id não encotrado ou inexistente");
        }
    }

    //CADASTRAR NOVOS PILOTOS
    [HttpPost("CadastrarPilotos")]
    public async Task<ApiResponse> CadPiloto(Piloto piloto)
    {
        try
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync($@" INSERT INTO Piloto 
                                                                  ( nomePiloto, 
                                                                    sobrenomePiloto, 
                                                                    enderecoPiloto, 
                                                                    telefonePiloto, 
                                                                    nascimentoPiloto, 
                                                                    dataContratacaoPiloto, 
                                                                    idAviao, 
                                                                    idCompania
                                                                   )
                                                            VALUES (
                                                                    @NomePiloto, 
                                                                    @SobrenomePiloto, 
                                                                    @EnderecoPiloto, 
                                                                    @TelefonePiloto, 
                                                                    @NascimentoPiloto, 
                                                                    @DataContratacaoPiloto, 
                                                                    @IdAviao, 
                                                                    @IdCompania)", piloto
                                           );

            return new ApiResponse("Piloto cadastrado com sucesso!");
        }
        catch
        {
            throw new ApiException("Erro de validação");
        }
    }

    //ATUALIZAR PILOTOS EXISTENTES
    [HttpPut("AtualizarPilotos")]
    public async Task<ApiResponse> AtualizaPilotos(Piloto piloto)
    {
        try
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync($@"
                                                UPDATE Piloto SET 
                                                                nomePiloto = @NomePiloto, 
                                                                sobrenomePiloto = @SobrenomePiloto, 
                                                                enderecoPiloto = @EnderecoPiloto, 
                                                                telefonePiloto = @TelefonePiloto, 
                                                                nascimentoPiloto = @NascimentoPiloto, 
                                                                dataContratacaoPiloto = @DataContratacaoPiloto, 
                                                                idAviao = @IdAviao, 
                                                                idCompania = @IdCompania 
                                                          WHERE idPiloto = @IdPiloto"
                                          );

            return new ApiResponse($"Atualização feita com sucesso para o id @idPiloto");
        }
        catch
        {
            return new ApiResponse($"Erro ao atualizar o cadastro");
            throw new ApiProblemDetailsException(ModelState);
        }
    }

    // DELETAR UM PILOTO ESPECIFICO
    [HttpDelete("DeletarPiloto")]

    public async Task<ApiResponse> Delete(int idPiloto)
    {
        try
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM Piloto WHERE idPiloto = @id", new { Id = idPiloto });
            return new ApiResponse("Exclusão feita com sucesso");
        }
        catch
        {
            return new ApiResponse("Não foi possivel deletar o Cadastro n° @idPiloto, cadastro inexistente");
            throw new ApiProblemDetailsException(ModelState);
        }
    }

    // RETORNA TODOS OS PILOTOS APOS CADA OPERAÇÃO
    private static async Task<IEnumerable<Piloto>> SelecionarTodosPilotos(SqlConnection connection)
    {
        return await connection.QueryAsync<Piloto>("SELECT * FROM Piloto");
    }
}
