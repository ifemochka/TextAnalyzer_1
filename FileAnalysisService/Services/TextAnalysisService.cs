using FileAnalysisService.Models;
using System.Text.RegularExpressions;

namespace FileAnalysisService.Services;

public class TextAnalysisService
{
    public FileStatistics Analyze(string text)
    {
        return new FileStatistics
        {
            ParagraphCount = text.Split('\n').Length,
            WordCount = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length,
            CharacterCount = text.Length
        };
    }

    public ComparisonResult Compare(string text1, string text2)
    {
        bool isIdentical = text1 == text2;
        double similarity = CalculateJaccardSimilarity(text1, text2);

        return new ComparisonResult
        {
            IsIdentical = isIdentical,
            SimilarityPercentage = similarity * 100
        };
    }

    private double CalculateJaccardSimilarity(string text1, string text2)
    {
        var words1 = new HashSet<string>(Regex.Split(text1.ToLower(), @"\W+"));
        var words2 = new HashSet<string>(Regex.Split(text2.ToLower(), @"\W+"));

        var intersection = new HashSet<string>(words1);
        intersection.IntersectWith(words2);

        var union = new HashSet<string>(words1);
        union.UnionWith(words2);

        return union.Count == 0 ? 0 : (double)intersection.Count / union.Count;
    }
}
