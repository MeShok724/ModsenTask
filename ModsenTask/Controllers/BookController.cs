using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenTask.API.Exceptions;
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
            if (book == null)
                throw new NotFoundException("Книга не найдена");
            return Ok(book);
        }
        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookResponse>> GetBookByIsbn(string isbn)
        {
            var book = await _bookService.GetBookByIsbnAsync(isbn);
            if (book == null)
                throw new NotFoundException("Книга не найдена");
            return Ok(book);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddBook([FromBody] BookRequest book)
        {
            var result = await _bookService.AddBookAsync(book);
            if (!result.Item1)
                throw new BadRequestException("Некорректные данные");
            return Ok(result.Item2);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{bookId:Guid}")]
        public async Task<ActionResult> UpdateBook(Guid bookId, [FromBody] BookRequest bookRequest)
        {
            bool result = await _bookService.UpdateBookAsync(bookId, bookRequest);
            if (!result)
                throw new BadRequestException("Невозможно обновить книгу");
            return Ok();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                throw new BadRequestException("Невозможно удалить книгу");
            return Ok();
        }

        [Authorize(Policy = "AuthenticatedUsersOnly")]
        [HttpPost("{bookId:Guid}/lend/{userId:Guid}")]
        public async Task<ActionResult> LendBook(Guid bookId, Guid userId, [FromQuery] DateTime returnDate)
        {
            var result = await _bookService.LendBookAsync(bookId, userId, returnDate);
            if (!result)
                throw new BadRequestException("Невозможно выдать книгу");
            return Ok();
        }

        //[HttpPut("{bookId:guid}/image")]
        //[Consumes("multipart/form-data")]
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
