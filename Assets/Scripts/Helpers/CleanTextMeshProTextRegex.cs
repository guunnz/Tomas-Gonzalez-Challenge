using System;
using System.Text.RegularExpressions;

public class CleanTextMeshProTextRegex
{
    public static string GetCleanString(string input)
    {
        var result = string.Empty;
        Regex regex = new Regex(">([^>\n]+)$");

        foreach (Match match in regex.Matches(input))
        {
            if (match.Success)
            {
                result += match.Groups[1].Value + Environment.NewLine;
            }
        }

        return result.Trim();
    }
}