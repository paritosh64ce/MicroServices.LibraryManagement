using Api.Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService service;
        private readonly ILogger<BookController> logger;
        public BookController(IBookService service, ILogger<BookController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetBook(int? id = null)
        {
            if (id == null)
            {
                logger.LogWarning("All book information provided");
                var books = await service.GetBooks();
                return Ok(books);
            }
            else
            {
                var book = await service.GetBook((int)id);
                if (book != null)
                {
                    logger.LogTrace($"Book with id:{id} searched.");
                    return new OkObjectResult(book);
                }
                else
                {
                    logger.LogError($"Book with id:{id} not found.");
                    return new NotFoundObjectResult($"Book having id - {id} not found.");
                }
            }
        }

        [HttpPost("subscribe/{id}")]
        public async Task<IActionResult> SubscribeBook(int id)
        {
            await service.SubscribeBook(id);
            logger.LogInformation($"Book with id:{id} has been subscribed.");
            return new OkResult();
        }

    }
}
