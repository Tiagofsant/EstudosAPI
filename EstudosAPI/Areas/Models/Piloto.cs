namespace EstudosAPI.Areas.Models;

public class Piloto
{
    public Guid IdPiloto { get; set; }
    public string? NomePiloto { get; set; }
    public string? SobrenomePiloto { get; set; }
    public string? EnderecoPiloto { get; set; }
    public float TelefonePiloto { get; set; }
    public DateTime NascimentoPiloto { get; set; }
    public DateTime DataContratacaoPiloto { get; set; }
    public int IdAviao { get; set; }
    public int IdCompania { get; set; }
}