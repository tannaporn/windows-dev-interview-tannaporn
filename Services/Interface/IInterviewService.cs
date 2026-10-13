namespace WindowsDeveloperInterview;

public interface IInterviewService
{
    bool IsValidBrackets(string input);
    string[] NaturalSort(IEnumerable<string> items);
    string[] Autocomplete(string search, IEnumerable<string> items, int maxResult);
    string ToRoman(int number);
    int FromRoman(string roman);
    int SortDigitsDescending(int number);
    int[] Tribonacci(int[] signature, int n);
}
