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

        [HttpGet("{page}")]
        public IActionResult Get([FromRoute] int page, [FromQuery] BooksOptions booksOptions)
        {
            booksOptions.UpdateSeed(page);
            return Ok(_generatorService.GetFakeBooks(booksOptions, page));
        }
    }
}
