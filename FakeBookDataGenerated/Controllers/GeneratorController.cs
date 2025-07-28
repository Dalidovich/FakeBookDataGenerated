using FakeBookDataGenerated.Enum;
using FakeBookDataGenerated.Extension;
using FakeBookDataGenerated.Model;
using FakeBookDataGenerated.Service;
using Microsoft.AspNetCore.Mvc;

namespace FakeBookDataGenerated.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneratorController : ControllerBase
    {
        private readonly GeneratorService _generatorService;

        public GeneratorController(GeneratorService generatorService)
        {
            _generatorService = generatorService;
        }

        [HttpGet("books/{page}")]
        public IActionResult GetBooks([FromRoute] int page, [FromQuery] BooksOptions booksOptions)
        {
            booksOptions.UpdateSeed(page);
            return Ok(_generatorService.GetFakeBooks(booksOptions, page));
        }

        [HttpGet("comments/{id}")]
        public IActionResult GetComments([FromRoute] int id, [FromQuery] int count, [FromQuery] Language language)
        {
            return Ok(_generatorService.GetFakeComments(id, count, language));
        }
    }
}
