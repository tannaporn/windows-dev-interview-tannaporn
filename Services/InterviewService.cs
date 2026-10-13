namespace WindowsDeveloperInterview;

public sealed class InterviewService : IInterviewService
{
    public bool IsValidBrackets(string input)
    {
        return Interview.IsValidBrackets(input);
    }

    public string[] NaturalSort(IEnumerable<string> items)
    {
        return Interview.NaturalSort(items);
    }

    public string[] Autocomplete(string search, IEnumerable<string> items, int maxResult)
    {
        return Interview.Autocomplete(search, items, maxResult);
    }

    public string ToRoman(int number)
    {
        return Interview.ToRoman(number);
    }

    public int FromRoman(string roman)
    {
        return Interview.FromRoman(roman);
    }

    public int SortDigitsDescending(int number)
    {
        return Interview.SortDigitsDescending(number);
    }

    public int[] Tribonacci(int[] signature, int n)
    {
        return Interview.Tribonacci(signature, n);
    }
}
