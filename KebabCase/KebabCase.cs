using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KebabCase {
    public static class KebabCase {
        public static string ToKebabCase(this string value) {
            if (string.IsNullOrEmpty(value)) {
                return null;
            }

            string[] values = Regex.Replace(value, "['\u2019]", string.Empty).ToWords();

            if (!values.Any()) {
                return null;
            }

            return string.Join("-", values).ToLower();
        }
    }
}
