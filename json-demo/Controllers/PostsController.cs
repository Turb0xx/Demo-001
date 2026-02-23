
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;

namespace json_demo.Controllers
{
    [Route("api/rest")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly HttpClient _httpClient; // antes del constructor varible global
        public PostsController()
        {
            _httpClient = new HttpClient();//inicializar el cliente
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        //GET: api/rest
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var response = await _httpClient.GetAsync("posts");
            var posts = await response.Content.ReadAsStringAsync();
            return Ok(posts);
        }

        // PUT: api/posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Posts postActualizado)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(postActualizado);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"posts/{id}", content);
            return NoContent();
        }

        // POST: api/posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Posts nuevoPost)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(nuevoPost);
            var content = new StringContent(json, System.Text.Encoding.UTF8, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

            var response = await _httpClient.PostAsync("posts", content);
            var postCreado = await response.Content.ReadAsStringAsync();

            return Created($"api/posts/{nuevoPost.id}", postCreado);
        }

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _httpClient.DeleteAsync($"posts/{id}");
            return NoContent();
        }

    }


}
