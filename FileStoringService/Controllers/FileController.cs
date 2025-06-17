using FileStoringService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileStoringService.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly FileStorageService _fileStorageService;

        public FileController(FileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(/*[FromForm] IFormFile file*/)
        {
            //var result = await _fileStorageService.SaveFileAsync(file);
            return Ok("result");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var content = await _fileStorageService.GetFileContentAsync(id);
            if (content == null) return NotFound();

            return File(content, "text/plain", "report.txt");
        }
    }

}
