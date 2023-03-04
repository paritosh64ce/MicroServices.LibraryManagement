using Api.Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService service;
        public BookController(IBookService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await service.GetBook(id);
            if (book != null)
            {
                return new OkObjectResult(book);
            }
            else
            {
                return new NotFoundObjectResult($"Book having id - {id} not found.");
            }
        }

        [HttpPost("subscribe/{id}")]
        public async Task<IActionResult> SubscribeBook(int id)
        {
            await service.SubscribeBook(id);
            return new OkResult();
        }

    }
}
