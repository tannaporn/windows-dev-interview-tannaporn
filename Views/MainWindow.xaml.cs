using System.Windows;
using System.Windows.Media;
namespace WindowsDeveloperInterview;

public partial class MainWindow : Window
{
    private readonly IInterviewService _service;

    public MainWindow(IInterviewService service)
    {
        _service = service;
        InitializeComponent();
    }

    private static string[] ParseItems(string value) =>
        value.Split(',', StringSplitOptions.TrimEntries);

    private static int ParseInt(string value, string fieldName) =>
        int.TryParse(value, out int result)
            ? result
            : throw new FormatException($"{fieldName} must be a whole number.");

    private void Run(Func<string> action)
    {
        try
        {
            StatusText.Foreground = Brushes.DarkGreen;
            StatusText.Text = action();
        }
        catch (Exception ex)
        {
            string message = ex is InterviewException or FormatException or OverflowException
                ? ex.Message
                : "An unexpected error occurred. Please try again.";

            StatusText.Foreground = Brushes.Firebrick;
            StatusText.Text = $"Error: {message}";
            MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ValidateBrackets_Click(object sender, RoutedEventArgs e) =>
        Run(() => $"Valid brackets: {_service.IsValidBrackets(BracketsInput.Text)}");

    private void NaturalSort_Click(object sender, RoutedEventArgs e) =>
        Run(() => $"Sorted: {string.Join(", ", _service.NaturalSort(ParseItems(NaturalSortInput.Text)))}");

    private void Autocomplete_Click(object sender, RoutedEventArgs e) =>
        Run(() => $"Matches: {string.Join(", ", _service.Autocomplete(
            SearchInput.Text,
            ParseItems(AutocompleteItemsInput.Text),
            ParseInt(MaxResultsInput.Text, "Maximum results")))}");

    private void ToRoman_Click(object sender, RoutedEventArgs e) =>
        Run(() => $"Roman numeral: {_service.ToRoman(ParseInt(NumberInput.Text, "Number"))}");

    private void FromRoman_Click(object sender, RoutedEventArgs e) =>
        Run(() => $"Number: {_service.FromRoman(RomanInput.Text)}");

    private void SortDigits_Click(object sender, RoutedEventArgs e) =>
        Run(() => $"Descending digits: {_service.SortDigitsDescending(ParseInt(DigitsInput.Text, "Number"))}");

    private void Tribonacci_Click(object sender, RoutedEventArgs e) =>
        Run(() => $"Sequence: {string.Join(", ", _service.Tribonacci(
            ParseItems(SignatureInput.Text).Select(value => ParseInt(value, "Signature value")).ToArray(),
            ParseInt(TribonacciLengthInput.Text, "Length")))}");

    private void RunTests_Click(object sender, RoutedEventArgs e) => Run(() =>
    {
        var passed = new[]
        {
            _service.IsValidBrackets("{[()]}") && !_service.IsValidBrackets("([)]"),
            _service.NaturalSort(new[] { "file10", "file2", "file1" }).SequenceEqual(new[] { "file1", "file2", "file10" }),
            _service.Autocomplete("app", new[] { "pineapple", "apple", "snapple" }, 3).SequenceEqual(new[] { "apple", "pineapple", "snapple" }),
            _service.ToRoman(1994) == "MCMXCIV" && _service.FromRoman("MCMXCIV") == 1994,
            _service.SortDigitsDescending(42145) == 54421,
            _service.Tribonacci(new[] { 0, 1, 1 }, 7).SequenceEqual(new[] { 0, 1, 1, 2, 4, 7, 13 })
        };

        return passed.All(result => result)
            ? $"All {passed.Length} sample tests passed."
            : $"{passed.Count(result => result)} of {passed.Length} sample tests passed.";
    });

    private void CloseProgram_Click(object sender, RoutedEventArgs e) => Close();
}
