namespace EstudosAPI.Areas.Models;

public class Aviao
{
    public Guid IdAviao { get; set; }
    public string? MarcaAviao { get; set; }
    public string? ModeloAviao { get; set; }
    public string? MotorizacaoAviao { get; set; }
    public string? TipoOperacaoAviao { get; set; }
    public int IdCompania { get; set; }
    public string? InscricaoAviao { get; set; }
}