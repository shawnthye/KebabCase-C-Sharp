using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KebabCase {
    static class Words {
        private static readonly Regex AsciiWords = new Regex("[^\x00-\x2f\x3a-\x40\x5b-\x60\x7b-\x7f]+");
        private static readonly Regex UnicodeWord = new Regex("[a-z][A-Z]|[A-Z]{2,}[a-z]|[0-9][a-zA-Z]|[a-zA-Z][0-9]|[^a-zA-Z0-9 ]");

        public static string[] ToWords(this string input) {
            return UnicodeWord.IsMatch(input) ? input.ToUnicodeWords() : AsciiWords.Matches(input).Cast<Match>().Select(m => m.Value).ToArray();
        }
    }
}
