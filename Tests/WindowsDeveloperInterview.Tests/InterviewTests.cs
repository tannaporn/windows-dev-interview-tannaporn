using WindowsDeveloperInterview;
using Xunit;

namespace Tests;

public class InterviewTests
{
    private readonly IInterviewService _iInterviewService = new InterviewService();

    [Theory]
    [InlineData("()", true)]
    [InlineData("([])", true)]
    [InlineData("({[]})", true)]
    [InlineData("([)]", false)]
    [InlineData("((", false)]
    [InlineData("())", false)]
    [InlineData("}{", false)]
    [InlineData("", true)]
    public void Brackets(string input, bool expected) =>
        Assert.Equal(expected, _iInterviewService.IsValidBrackets(input));

    [Fact]
    public void NaturalSortExamples()
    {
        Assert.Equal(new[] { "SG20", "TH2", "TH19" },
            _iInterviewService.NaturalSort(new[] { "TH19", "SG20", "TH2" }));
        Assert.Equal(new[] { "TH1", "TH3Netflix", "TH7", "TH10" },
            _iInterviewService.NaturalSort(new[] { "TH10", "TH3Netflix", "TH1", "TH7" }));
    }

    [Fact]
    public void AutocompleteExamples()
    {
        var items = new[] { "Mother", "Think", "Worthy", "Apple", "Android" };
        Assert.Equal(new[] { "Think", "Mother" }, Interview.Autocomplete("th", items, 2));
        Assert.Equal(new[] { "Think", "Mother", "Worthy" }, Interview.Autocomplete("th", items, 3));
        Assert.Empty(Interview.Autocomplete("th", items, 0));
    }

    [Fact]
    public void AutocompleteRejectsNegativeMaximum() =>
        Assert.Throws<InterviewException>(() => Interview.Autocomplete("th", Array.Empty<string>(), -1));

    [Theory]
    [InlineData(1989, "MCMLXXXIX")]
    [InlineData(2000, "MM")]
    [InlineData(68, "LXVIII")]
    [InlineData(109, "CIX")]
    [InlineData(4, "IV")]
    [InlineData(9, "IX")]
    [InlineData(900, "CM")]
    public void RomanRoundTrip(int value, string expected)
    {
        Assert.Equal(expected, _iInterviewService.ToRoman(value));
        Assert.Equal(value, _iInterviewService.FromRoman(expected));
    }

    [Theory]
    [InlineData("IIII")]
    [InlineData("IC")]
    [InlineData("VX")]
    public void RomanInvalid(string input) =>
        Assert.Throws<FormatException>(() => _iInterviewService.FromRoman(input));

    [Theory]
    [InlineData(3008, 8300)]
    [InlineData(1989, 9981)]
    [InlineData(2679, 9762)]
    [InlineData(9163, 9631)]
    public void SortDigits(int input, int expected) =>
        Assert.Equal(expected, _iInterviewService.SortDigitsDescending(input));

    [Fact]
    public void TribonacciExamples()
    {
        Assert.Equal(new[] { 1, 3, 5, 9, 17 }, _iInterviewService.Tribonacci(new[] { 1, 3, 5 }, 5));
        Assert.Equal(new[] { 2, 2, 2 }, _iInterviewService.Tribonacci(new[] { 2, 2, 2 }, 3));
        Assert.Equal(new[] { 10, 10, 10, 30 }, _iInterviewService.Tribonacci(new[] { 10, 10, 10 }, 4));
        Assert.Empty(_iInterviewService.Tribonacci(Array.Empty<int>(), 0));
        Assert.Equal(new[] { 5, 2, 0, 7 }, _iInterviewService.Tribonacci(new[] { 5, 2, 0 }, 4));
        Assert.Equal(new[] { 1, 1, 2, 4 }, _iInterviewService.Tribonacci(new[] { 1 }, 4));
    }

    [Fact]
    public void TribonacciRejectsNegativeLength() =>
        Assert.Throws<InterviewException>(() => Interview.Tribonacci(new[] { 1, 1, 1 }, -1));
}
