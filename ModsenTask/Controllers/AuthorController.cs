using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenTask.Application.DTOs;
using ModsenTask.Application.Services;

namespace ModsenTask.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController(IAuthorService authorService) : Controller
    {
        private readonly IAuthorService _authorService = authorService;

        [HttpGet]
        public async Task<ActionResult<List<AuthorResponse>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{authorId:Guid}")]
        public async Task<ActionResult<AuthorResponse>> GetAuthorById(Guid authorId)
        {
            var author = await _authorService.GetAuthorByIdAsync(authorId);
            if (author == null) 
                return NotFound();
            return Ok(author);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddAuthor([FromBody] AuthorRequest authorRequest)
        {
            Guid guid = await _authorService.AddAuthorAsync(authorRequest);
            return Ok(guid);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{authorId:Guid}")]
        public async Task<ActionResult> UpdateAuthor(Guid authorId, [FromBody] AuthorRequest authorRequest)
        {
            bool result = await _authorService.UpdateAuthorAsync(authorId, authorRequest);
            if (!result)
                return NotFound();
            return Ok();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{authorId:guid}")]
        public async Task<ActionResult> DeleteAuthor(Guid authorId)
        {
            bool result = await _authorService.DeleteAuthorAsync(authorId);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpGet("books/{authorId:Guid}")]
        public async Task<ActionResult<List<BookResponse>>> GetBooksByAuthorId(Guid authorId)
        {
            var books = await _authorService.GetBooksByAuthorAsync(authorId);
            if (books == null)
                return NotFound();
            return Ok(books);
        }
    }
}
