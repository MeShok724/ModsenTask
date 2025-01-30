using Microsoft.AspNetCore.Mvc;
using ModsenTask.Application.DTOs;
using ModsenTask.Application.Services;
using ModsenTask.Core.Entities;

namespace ModsenTask.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController(IBookService bookService) : Controller
    {
        private readonly IBookService _bookService = bookService;

        [HttpGet]
        public async Task<ActionResult<List<BookResponse>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<BookResponse>> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return book is not null ? Ok(book) : NotFound();
        }
        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookResponse>> GetBookByIsbn(string isbn)
        {
            var book = await _bookService.GetBookByIsbnAsync(isbn);
            return book is not null ? Ok(book) : NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> AddBook([FromBody] BookRequest book)
        {
            if (book is null)
                return BadRequest("Некорректные данные");

            Guid guid = await _bookService.AddBookAsync(book);
            return guid;
        }
        [HttpPut]
        public async Task<ActionResult> UpdateBook([FromBody] Book book)
        {
            if (book is null)
                return BadRequest("Некорректные данные");

            await _bookService.UpdateBookAsync(book);
            return Ok();
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            return result ? Ok() : NotFound();
        }
        [HttpPost("{bookId:Guid}/lend/{userId:Guid}")]
        public async Task<ActionResult> LendBook(Guid bookId, Guid userId, [FromQuery] DateTime returnDate)
        {
            var result = await _bookService.LendBookAsync(bookId, userId, returnDate);
            return result ? Ok() : BadRequest("Невозможно выдать книгу");
        }

        //[HttpPut("{bookId:guid}/image")]
        //public async Task<ActionResult> UpdateBookImage(Guid bookId, [FromForm] IFormFile image)
        //{
        //    if (image is null || image.Length == 0)
        //        return BadRequest("Некорректное изображение");

        //    using var ms = new MemoryStream();
        //    await image.CopyToAsync(ms);
        //    var imageData = ms.ToArray();

        //    var result = await _bookService.UpdateBookImageAsync(bookId, imageData);
        //    return result ? Ok() : NotFound("Книга не найдена");
        //}
    }
}
