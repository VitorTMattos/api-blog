using ApiBlog.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace ApiBlog.Repositories
{
    public class PostsRepository
    {
        private readonly string _connectionString;

        public PostsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionStringTeste");
        }

        public async Task<IEnumerable<AllPostsModel>> GetAllPosts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"SELECT p.id, 
                                p.titulo, 
                                p.conteudo, 
                                COUNT(c.id) [QntComentarios] 
                                FROM Posts p
                                LEFT JOIN Comentarios c ON p.id = c.id_post
                                GROUP BY p.id, p.titulo, p.conteudo
                                ORDER BY P.id";

                    var posts = await connection.QueryAsync<AllPostsModel>(sql);

                    return posts;
                }

            }
            catch (SqlException e)
            {
                throw new Exception($"Ocorreu um erro inesperado ao acessar o banco de dados: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Ocorreu um erro inesperado: {e.Message}");
            }
        }

        public async Task<PostModel> GetPostById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sqlPost = @"SELECT p.id, 
                                p.titulo, 
                                p.conteudo, 
                                COUNT(c.id) [QntComentarios] 
                                FROM Posts p
                                LEFT JOIN Comentarios c ON p.id = c.id_post
                                WHERE p.id = @PostId
                                GROUP BY p.id, p.titulo, p.conteudo
                                ORDER BY P.id";

                    var post = await connection.QuerySingleOrDefaultAsync<PostModel>(sqlPost, new { PostId = id});

                    if (post != null)
                    {
                        if (post.QntComentarios > 0)
                        {
                            var sqlComentarios = @"SELECT id, conteudo FROM Comentarios WHERE id_post = @PostId";

                            var comentarios = await connection.QueryAsync<CommentsModel>(sqlComentarios, new {PostId = id});

                            post.Comentarios = comentarios;
                        }
                    }
                    return post;
                }

            }
            catch (SqlException e)
            {
                throw new Exception($"Ocorreu um erro inesperado ao acessar o banco de dados: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Ocorreu um erro inesperado: {e.Message}");
            }



        }





        public async Task<Int32> CreatePost(PostRequestModel post)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"INSERT INTO Posts (titulo, conteudo, status_post) OUTPUT inserted.id VALUES(@titulo, @conteudo, 1)";

                    var insert = await connection.QuerySingleOrDefaultAsync(sql, new { titulo = post.Titulo, conteudo = post.Titulo });

                    return insert.id;
                }


            }
            catch (SqlException e)
            {
                throw new Exception($"Ocorreu um erro inesperado ao acessar o banco de dados: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Ocorreu um erro inesperado: {e.Message}");
            }
        }

        public async Task<Int32> CreateComment(CommentRequestModel comment, int postId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"INSERT INTO Comentarios (conteudo, status_comentario, id_post) OUTPUT inserted.id VALUES(@conteudo, 1, @PostId)";

                    var insert = await connection.QuerySingleOrDefaultAsync(sql, new { conteudo = comment.Conteudo, PostId = postId });

                    return insert.id;
                }


            }
            catch (SqlException e)
            {
                throw new Exception($"Ocorreu um erro inesperado ao acessar o banco de dados: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Ocorreu um erro inesperado: {e.Message}");
            }
        }
    }
}
