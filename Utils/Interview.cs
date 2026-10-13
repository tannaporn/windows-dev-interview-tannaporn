using System.Globalization;
using System.Text;
using WindowsDeveloperInterview.Utils;

namespace WindowsDeveloperInterview;

public static class Interview
{
    public static bool IsValidBrackets(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        var res = new List<char>();
        foreach (var item in input)
        {
            if (item is '(' or '[' or '{' or '<')
            {
                res.Add(item);
                continue;
            }

            if (item is not (')' or ']' or '}' or '>'))
                return false;

            if (res.Count == 0)
                return false;

                foreach (var open in res)
                {
                    if ((open == '(' && item == ')') ||
                        (open == '[' && item == ']') ||
                        (open == '{' && item == '}') ||
                        (open == '<' && item == '>'))
                    {
                        res.Remove(open);
                        break;
                    }
                }
        }
        return res.Count == 0;
    }

    public static string[] NaturalSort(IEnumerable<string> items)
    {
        if (items is null)
            throw new InterviewException("Items cannot be null.");

        return items.OrderBy(x => x, NaturalStringComparer.Instance).ToArray();
    }

    public static string[] Autocomplete(string search, IEnumerable<string> items, int maxResult)
    {
        if (search is null)
            throw new InterviewException("Search cannot be null.");
        if (items is null)
            throw new InterviewException("Items cannot be null.");
        if (maxResult < 0)
            throw new InterviewException("Maximum results cannot be negative.");
        if (maxResult == 0 || search.Length == 0)
            return Array.Empty<string>();

        return items
            .Select((item, index) => new
            {
                Item = item ?? throw new InterviewException("Items cannot contain null values."),
                Index = index
            })
            .Select(x => new
            {
                x.Item,
                x.Index,
                Position = x.Item.IndexOf(search, StringComparison.OrdinalIgnoreCase)
            })
            .Where(x => x.Position >= 0)
            .OrderBy(x => x.Position == 0 ? 0 : x.Position + search.Length == x.Item.Length ? 2 : 1)
            .ThenBy(x => x.Index)
            .Take(maxResult)
            .Select(x => x.Item)
            .ToArray();
    }

    private static readonly (int Value, string Symbol)[] RomanTokens =
    {
        (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
        (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
        (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
    };

    public static string ToRoman(int number)
    {
        if (number is < 1 or > 3999)
            throw new InterviewException("Allowed range: 1..3999.");

        var result = new StringBuilder();
        foreach (var (value, symbol) in RomanTokens)
        {
            while (number >= value)
            {
                result.Append(symbol);
                number -= value;
            }
        }

        return result.ToString();
    }

    public static int FromRoman(string roman)
    {
        if (roman is null)
            throw new InterviewException("Roman numeral cannot be null.");
        if (roman.Length == 0) throw new FormatException("Roman numeral cannot be empty.");

        var values = new Dictionary<char, int>
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000
        };

        int sum = 0;
        for (int i = 0; i < roman.Length; i++)
        {
            if (!values.TryGetValue(roman[i], out int current))
                throw new FormatException("Invalid Roman numeral character.");

            if (i + 1 < roman.Length)
            {
                if (!values.TryGetValue(roman[i + 1], out int next))
                    throw new FormatException("Invalid Roman numeral character.");

                sum += current < next ? -current : current;
            }
            else
            {
                sum += current;
            }
        }

        if (sum < 1 || sum > 3999 || ToRoman(sum) != roman)
            throw new FormatException("Not a canonical modern Roman numeral.");

        return sum;
    }

    public static int SortDigitsDescending(int number)
    {
        if (number <= 0) throw new InterviewException("Number must be greater than zero.");

        char[] digits = number.ToString(CultureInfo.InvariantCulture).ToCharArray();
        Array.Sort(digits);
        Array.Reverse(digits);

        if (!int.TryParse(new string(digits), NumberStyles.None,
                          CultureInfo.InvariantCulture, out int result))
            throw new OverflowException("Sorted digits exceed Int32.MaxValue.");

        return result;
    }

    public static int[] Tribonacci(int[] signature, int n)
    {
        if (signature is null)
            throw new InterviewException("Signature cannot be null.");
        if (signature.Length > 3)
            throw new InterviewException("At most three initial values are allowed.");
        if (n < 0) throw new InterviewException("Length cannot be negative.");

        var result = new List<int>(n);
        result.AddRange(signature.Take(n));

        while (result.Count < n)
        {
            int count = result.Count;
            int a = count >= 1 ? result[count - 1] : 0;
            int b = count >= 2 ? result[count - 2] : 0;
            int c = count >= 3 ? result[count - 3] : 0;
            result.Add(checked(a + b + c));
        }

        return result.ToArray();
    }
}


