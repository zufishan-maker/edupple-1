using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex _splitNameRegex = new Regex(@"[\W_]+");
        private static readonly Regex _properWordRegex = new Regex(@"([A-Z][a-z]*)|([0-9]+)");

        public static async Task<string> ToJson<T>(this T model)
        {
            return JsonConvert.SerializeObject(model, Settings);
        }
        private static JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
        };
        public static string Format(object value, string format, IFormatProvider formatProvider = null)
        {
            if (value == null)
            {
                return string.Empty;
            }

            IFormattable formattable = value as IFormattable;

            if (formattable != null)
            {
                return formattable.ToString(format, formatProvider);
            }

            throw new ArgumentException("value");
        }
        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }
        public static string Truncate(this string text, int keep, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (string.IsNullOrEmpty(ellipsis))
                ellipsis = string.Empty;

            string buffer = NormalizeLineEndings(text);
            if (buffer.Length <= keep)
                return buffer;

            if (buffer.Length <= keep + ellipsis.Length || keep < ellipsis.Length)
                return buffer.Substring(0, keep);

            return string.Concat(buffer.Substring(0, keep - ellipsis.Length), ellipsis);
        }


        public static bool IsEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email)) return false;
                return new System.Net.Mail.MailAddress(email) == null ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }


        public static bool IsNullOrWhiteSpace(this string item)
        {
            if (item == null)
                return true;

            for (int i = 0; i < item.Length; i++)
                if (!char.IsWhiteSpace(item[i]))
                    return false;

            return true;
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Does string contain both uppercase and lowercase characters?
        /// </summary>
        /// <param name="s">The value.</param>
        /// <returns>True if contain mixed case.</returns>
        public static bool IsMixedCase(this string s)
        {
            if (s.IsNullOrEmpty())
                return false;

            var containsUpper = s.Any(Char.IsUpper);
            var containsLower = s.Any(Char.IsLower);

            return containsLower && containsUpper;
        }


        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string output = ToPascalCase(value);
            if (output.Length > 2)
                return char.ToLower(output[0]) + output.Substring(1);

            return output.ToLower();
        }

        public static string ToPascalCase(this string value)
        {
            return value.ToPascalCase(_splitNameRegex);
        }


        public static string ToPascalCase(this string value, Regex splitRegex)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var mixedCase = value.IsMixedCase();
            var names = splitRegex.Split(value);
            var output = new StringBuilder();

            if (names.Length > 1)
            {
                foreach (string name in names)
                {
                    if (name.Length > 1)
                    {
                        output.Append(char.ToUpper(name[0]));
                        output.Append(mixedCase ? name.Substring(1) : name.Substring(1).ToLower());
                    }
                    else
                    {
                        output.Append(name);
                    }
                }
            }
            else if (value.Length > 1)
            {
                output.Append(char.ToUpper(value[0]));
                output.Append(mixedCase ? value.Substring(1) : value.Substring(1).ToLower());
            }
            else
            {
                output.Append(value.ToUpper());
            }

            return output.ToString();
        }

        public static string ToTitle(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            value = ToPascalCase(value);

            var words = _properWordRegex.Matches(value);
            var spacedName = new StringBuilder();
            foreach (Match word in words)
            {
                if (spacedName.Length > 0)
                    spacedName.Append(' ');

                spacedName.Append(word.Value);
            }

            return spacedName.ToString();
        }



        public static string RemoveInvisible(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return s
                .Replace("\r\n", " ")
                .Replace('\n', ' ')
                .Replace('\t', ' ');
        }

        public static string NormalizeLineEndings(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text
                .Replace("\r\n", "\n")
                .Replace("\n", Environment.NewLine);
        }



        public static StringBuilder AppendLine(this StringBuilder sb, string format, params object[] args)
        {
            sb.AppendFormat(format, args);
            sb.AppendLine();
            return sb;
        }

        public static StringBuilder AppendIf(this StringBuilder sb, string text, Func<string, bool> condition = null)
        {
            var c = condition ?? (s => !string.IsNullOrWhiteSpace(s));

            if (c(text))
                sb.Append(text);

            return sb;
        }


        public static StringBuilder AppendIf(this StringBuilder sb, string text, bool condition)
        {
            if (condition)
                sb.Append(text);

            return sb;
        }


        public static StringBuilder AppendLineIf(this StringBuilder sb, string text, Func<string, bool> condition = null)
        {
            var c = condition ?? (s => !string.IsNullOrWhiteSpace(s));

            if (c(text))
                sb.AppendLine(text);

            return sb;
        }


        public static string ComputeHash(this string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            byte[] hashBytes;

            using (var sha = new SHA512Managed())
                hashBytes = sha.ComputeHash(bytes);

            var hash = Convert.ToBase64String(hashBytes);

            return hash;
        }
    }
}
