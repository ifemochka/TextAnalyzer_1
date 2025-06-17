using Microsoft.AspNetCore.Mvc;
using FileAnalysisService.Services;
using FileAnalysisService.Models;

namespace FileAnalysisService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly TextAnalysisService _analysisService;

    public AnalysisController(TextAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }

    [HttpPost("analyze")]
    public ActionResult<FileStatistics> AnalyzeText([FromBody] string text)
    {
        return Ok(_analysisService.Analyze(text));
    }

    [HttpPost("compare")]
    public ActionResult<ComparisonResult> CompareTexts([FromBody] CompareRequest request)
    {
        return Ok(_analysisService.Compare(request.Text1, request.Text2));
    }
}

public class CompareRequest
{
    public string Text1 { get; set; }
    public string Text2 { get; set; }
}
