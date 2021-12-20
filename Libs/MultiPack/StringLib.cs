using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPack
{
    public static class StringLib
    {
        public static string Truncate(this string value, int maxLength)
        {
            if ((string.IsNullOrEmpty(value)) || (maxLength <= 0)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string Left(this string value, int length)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(value))
            {
                //Set valid empty string as string could be null
                value = string.Empty;
            }
            else if (value.Length > length)
            {
                //Make the string no longer than the max length
                value = value.Substring(0, length);
            }

            //Return the string
            return value;
        }

        public static string Right(this string value, int length)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(value))
            {
                //Set valid empty string as string could be null
                value = string.Empty;
            }
            else if (value.Length > length)
            {
                //Make the string no longer than the max length
                value = value.Substring(value.Length - length, length);
            }

            //Return the string
            return value;
        }

        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("The string to find may not be empty", "value");
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }

        public static string GetTextContainingIndexDelimitedByStrings(string source, int index, string delimiterBegin, string delimiterEnd)
        {
            string result = string.Empty;

            int indexBegin = source.LastIndexOf(delimiterBegin, index, StringComparison.InvariantCultureIgnoreCase);
            int indexEnd = source.IndexOf(delimiterEnd, index, StringComparison.InvariantCultureIgnoreCase);

            result = source.Substring(indexBegin, indexEnd - indexBegin);

            return result;
        }

        public static string GetTextAboveIndexDelimitedByStrings(string source, int index, string delimiterBegin, string delimiterEnd)
        {
            string result = string.Empty;

            int indexBegin = source.LastIndexOf(delimiterBegin, index, StringComparison.InvariantCultureIgnoreCase);
            int indexEnd = source.IndexOf(delimiterEnd, indexBegin, StringComparison.InvariantCultureIgnoreCase);

            result = source.Substring(indexBegin, indexEnd - indexBegin);

            return result;
        }

        public static string GetTextBelowIndexDelimitedByStrings(string source, int index, string delimiterBegin, string delimiterEnd)
        {
            string result = string.Empty;

            int indexBegin = source.IndexOf(delimiterBegin, index, StringComparison.InvariantCultureIgnoreCase);
            int indexEnd = source.IndexOf(delimiterEnd, indexBegin, StringComparison.InvariantCultureIgnoreCase);

            result = source.Substring(indexBegin, indexEnd - indexBegin);

            return result;
        }

        //Gets the value from KeyValue Pair like Key="value" from a text
        public static string GetValueFromKeyValuePair(string source, string key)
        {
            string result = string.Empty;

            try
            {
                int indexBegin = source.IndexOf(key);
                int indexEnd = source.IndexOf("\"", source.IndexOf("\"", indexBegin) + 1);
                var pair = source.Substring(indexBegin, indexEnd - indexBegin);

                var chunks = pair.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (chunks.Count() > 1)
                {
                    result = chunks[1].Replace("\"", string.Empty).Trim();
                }
            }
            catch { }

            return result;
        }

        public static string Repeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }
    }
}
