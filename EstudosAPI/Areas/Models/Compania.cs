namespace EstudosAPI.Areas.Models;

public class Compania
{
    public Guid IdCompania { get; set; }
    public string? NomeCompania { get; set; }
    public string? TipoVooCompania { get; set; }
    public string? HorarioAtendimentoCompania { get; set; }
    public string? TipoOperacaoAviaoCompania { get; set; }
}