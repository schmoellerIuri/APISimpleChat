namespace APISimples.Models
{
    public class MensagemModel
    {
        public int id { get; set; }

        public int conversaId { get; set; }

        public int remetenteId { get; set; }

        public string? conteudo { get; set; }

        public ConversaModel? conversa { get; set; }

        public UsuarioModel? remetente { get; set; }

    }
}
