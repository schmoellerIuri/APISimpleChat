namespace APISimples.Models
{
    public class ConversaModel
    {
        public int id { get; set; }

        public int idUser1 { get; set; }

        public int idUser2 { get; set;}

        public UsuarioModel? user1 { get; set; }

        public UsuarioModel? user2 { get; set; }

        public ICollection<MensagemModel>? mensagens { get; set; }

    }
}
