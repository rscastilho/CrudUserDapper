namespace CrudUserDapper.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; }
        public string Password { get; set; }


    }
}
