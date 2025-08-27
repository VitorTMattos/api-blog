using ApiBlog.Model;
using ApiBlog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace ApiBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsRepository _postsRepository;

        public PostsController(PostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var posts = await _postsRepository.GetAllPosts();

                if (posts != null)
                {
                    return Ok(posts);
                }
                else
                {
                    return BadRequest();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _postsRepository.GetPostById(id);

                if (post != null)
                {
                    return Ok(post);
                }
                else
                {
                    return BadRequest();
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

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequestModel post)
        {
            try
            {
                var id = await _postsRepository.CreatePost(post);

                if (!id.ToString().IsNullOrEmpty())
                {
                    return Ok(new { mensagem = "Post criado com sucesso!", PostId = id });
                }
                else
                {
                    return BadRequest();
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

        [HttpPost("{id:int}/comments")]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequestModel comment, int id)
        {
            try
            {
                
                var idComment = await _postsRepository.CreateComment(comment, id);

                if (!id.ToString().IsNullOrEmpty())
                {
                    return Ok(new { mensagem = "Comentario criado com sucesso!", ComentarioId = idComment });
                }
                else
                {
                    return BadRequest();
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
