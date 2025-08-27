namespace ApiBlog.Model
{
    public class CommentsModel
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public int PostId {  get; set; }

    }
    public class CommentRequestModel
    {
        public string Conteudo { get; set; }

    }
}
