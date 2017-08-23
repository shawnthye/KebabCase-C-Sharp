using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KebabCase {
    static class UnicodeWords {
        /** Used to compose unicode character classes. */
        private const string RsAstralRange = "\\ud800-\\udfff";
        private const string RsComboMarksRange = "\\u0300-\\u036f";
        private const string ReComboHalfMarksRange = "\\ufe20-\\ufe2f";
        private const string RsComboSymbolsRange = "\\u20d0-\\u20ff";
        private const string RsComboRange = RsComboMarksRange + ReComboHalfMarksRange + RsComboSymbolsRange;
        private const string RsDingbatRange = "\\u2700-\\u27bf";
        private const string RsLowerRange = "a-z\\xdf-\\xf6\\xf8-\\xff";
        private const string RsMathOpRange = "\\xac\\xb1\\xd7\\xf7";
        private const string RsNonCharRange = "\\x00-\\x2f\\x3a-\\x40\\x5b-\\x60\\x7b-\\xbf";
        private const string RsPunctuationRange = "\\u2000-\\u206f";
        private const string RsSpaceRange = " \\t\\x0b\\f\\xa0\\ufeff\\n\\r\\u2028\\u2029\\u1680\\u180e\\u2000\\u2001\\u2002\\u2003\\u2004\\u2005\\u2006\\u2007\\u2008\\u2009\\u200a\\u202f\\u205f\\u3000";
        private const string RsUpperRange = "A-Z\\xc0-\\xd6\\xd8-\\xde";
        private const string RsVarRange = "\\ufe0e\\ufe0f";
        private const string RsBreakRange = RsMathOpRange + RsNonCharRange + RsPunctuationRange + RsSpaceRange;

        /** Used to compose unicode capture groups. */
        private const string RsApos = "['\u2019]";
        private static readonly string RsBreak = $"[{RsBreakRange}]";
        private static readonly string RsCombo = $"[{RsComboRange}]";
        private const string RsDigits = "\\d+";
        private static readonly string RsDingbat = $"[{RsDingbatRange}]";
        private static readonly string RsLower = $"[{RsLowerRange}]";
        private static readonly string RsMisc = $"[^{RsAstralRange}{RsBreakRange}{RsDigits}{RsDingbatRange}{RsLowerRange}{RsUpperRange}]";
        private const string RsFitz = "\\ud83c[\\udffb-\\udfff]";
        private static readonly string RsModifier = $"(?:{RsCombo}|{RsFitz})";
        private static readonly string RsNonAstral = $"[^{RsAstralRange}]";
        private const string RsRegional = "(?:\\ud83c[\\udde6-\\uddff]){2}";
        private const string RsSurrPair = "[\\ud800-\\udbff][\\udc00-\\udfff]";
        private static readonly string RsUpper = $"[{RsUpperRange}]";
        private const string RsZWJ = "\\u200d";

        /** Used to compose unicode regexes. */
        private static readonly string RsMiscLower = $"(?:{RsLower}|{RsMisc})";
        private static readonly string RsMiscUpper = $"(?:{RsUpper}|{RsMisc})";
        private static readonly string RsOptContrLower = $"(?:{RsApos}(?:d|ll|m|re|s|t|ve))?";
        private static readonly string RsOptContrUpper = $"(?:{RsApos}(?:D|LL|M|RE|S|T|VE))?";
        private static readonly string ReOptMod = $"{RsModifier}?";
        private static readonly string RsOptVar = $"[{RsVarRange}]?";
        private static readonly string RsOptJoin = $"(?:{RsZWJ}(?:{string.Join("|", new string[] { RsNonAstral, RsRegional, RsSurrPair })}){RsOptVar + ReOptMod})*";
        private const string RsOrdLower = "\\d*(?:(?:1st|2nd|3rd|(?![123])\\dth)\\b)";
        private const string RsOrdUpper = "\\d*(?:(?:1ST|2ND|3RD|(?![123])\\dTH)\\b)";
        private static readonly string RsSeq = RsOptVar + ReOptMod + RsOptJoin;
        private static readonly string RsEmoji = $"(?:{string.Join("|", new string[] { RsDingbat, RsRegional, RsSurrPair })}){RsSeq}";

        private static readonly Regex Pattern = new Regex(string.Join("|", new string[] {
                $"{RsUpper}?{RsLower}+{RsOptContrLower}(?=${string.Join("|",new string[]{ RsBreak, RsUpper, "$" })})",
                $"{RsMiscUpper}+{RsOptContrUpper}(?={string.Join("|",RsBreak, RsUpper + RsMiscLower, "$")})",
                $"{RsUpper}?{RsMiscLower}+{RsOptContrLower}",
                $"{RsUpper}+{RsOptContrUpper}",
                RsOrdUpper,
                RsOrdLower,
                RsDigits,
                RsEmoji
            }));
        /**
         * Splits a Unicode `string` into an array of its words.
         *
         * @private
         * @param {string} The string to inspect.
         * @returns {Array} Returns the words of `string`.         
         */
        public static string[] ToUnicodeWords(this string input) {
            var matches = from match in Pattern.Matches(input).Cast<Match>()
                          select match.Value;

            return matches.ToArray();
        }
    }
}
