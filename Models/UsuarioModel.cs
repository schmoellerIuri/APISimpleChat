namespace APISimples.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public ICollection<ConversaModel>? conversasIniciadas { get; set; }
        public ICollection<ConversaModel>? conversasParticipadas { get; set; }
    }
}
