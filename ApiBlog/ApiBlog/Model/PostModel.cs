namespace ApiBlog.Model
{
    public class PostRequestModel
    {
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        
    }

    public class AllPostsModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public int QntComentarios { get; set; }
    }

    public class PostModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public int QntComentarios { get; set; }
        public IEnumerable<CommentsModel> Comentarios { get; set; }
    }
}
