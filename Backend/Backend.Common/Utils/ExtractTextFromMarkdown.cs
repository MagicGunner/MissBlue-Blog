using System.Text.RegularExpressions;

namespace Backend.Common.Utils;

public static class MarkdownUtil {
    public static string ExtractTextFromMarkdown(string markdown) {
        if (string.IsNullOrEmpty(markdown)) return string.Empty;

        var text = Regex.Replace(markdown, @"(\!\[.*?\]\(.*?\))|(\[.*?\]\(.*?\))|(```.*?```)|(`.*?`)|(\*\*|__|\*|_|>|#+|-|\d+\.)", "", RegexOptions.Singleline);
        text = Regex.Replace(text, @"\n{2,}", "\n");
        return text;
    }
    
}