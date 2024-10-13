namespace CrudUserDapper.DTO
{
    public class CriarUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; }
        public string Password { get; set; }
    }
}
