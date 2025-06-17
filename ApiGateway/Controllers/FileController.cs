using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FileController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("upload-analyze")]
    public async Task<IActionResult> UploadAndAnalyze(/*[FromForm] IFormFile file*/)
    {
        IFormFile file = null;
        if (file == null || file.Length == 0)
            return BadRequest("Файл отсутствует");

        var storingClient = _httpClientFactory.CreateClient("FileStoring");

        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(file.OpenReadStream());
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

        content.Add(fileContent, "file", file.FileName);

        var storingResponse = await storingClient.PostAsync("/api/files", content);
        if (!storingResponse.IsSuccessStatusCode)
            return StatusCode((int)storingResponse.StatusCode, "Ошибка хранения файла");

        var fileId = await storingResponse.Content.ReadAsStringAsync();

        var analysisClient = _httpClientFactory.CreateClient("FileAnalysis");

        var analysisRequest = new
        {
            FileId = fileId
        };

        var analysisResponse = await analysisClient.PostAsJsonAsync("/api/analyze", analysisRequest);
        if (!analysisResponse.IsSuccessStatusCode)
            return StatusCode((int)analysisResponse.StatusCode, "Ошибка анализа файла");

        var resultJson = await analysisResponse.Content.ReadAsStringAsync();
        return Ok(resultJson);
    }
}
