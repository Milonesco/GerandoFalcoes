public class UsuarioDto
{
    public int IdUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? Imagem { get; set; }

    public int TipoUsuarioId { get; set; }
    public string TipoUsuarioDescricao { get; set; }
}
