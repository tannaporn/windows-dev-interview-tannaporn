using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsDeveloperInterview.Utils
{
    public class NaturalStringComparer : IComparer<string>
    {
        public static NaturalStringComparer Instance { get; } = new();

        public int Compare(string? x, string? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            int i = 0, j = 0;
            while (i < x.Length && j < y.Length)
            {
                bool digitX = char.IsAsciiDigit(x[i]);
                bool digitY = char.IsAsciiDigit(y[j]);
                if (digitX && digitY)
                {
                    int startX = i, startY = j;
                    while (i < x.Length && char.IsAsciiDigit(x[i])) i++;
                    while (j < y.Length && char.IsAsciiDigit(y[j])) j++;
                    var a = x.AsSpan(startX, i - startX).TrimStart('0');
                    var b = y.AsSpan(startY, j - startY).TrimStart('0');
                    int compare = a.Length.CompareTo(b.Length);
                    if (compare == 0) compare = a.CompareTo(b, StringComparison.Ordinal);
                    if (compare != 0) return compare;
                    continue;
                }
                if (digitX != digitY) return digitX ? -1 : 1;

                int charCompare = char.ToUpperInvariant(x[i]).CompareTo(char.ToUpperInvariant(y[j]));
                if (charCompare != 0) return charCompare;
                i++;
                j++;
            }
            return (x.Length - i).CompareTo(y.Length - j);
        }
    }

}
