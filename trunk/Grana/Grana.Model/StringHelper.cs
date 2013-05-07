using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Grana.Model
{
    public class StringHelper
    {
        public static string AddSpaceIfCapital(string input)
        {
            StringBuilder builder = new StringBuilder();
            bool isFirst = true;

            foreach (char c in input)
            {
                if (char.IsUpper(c) && !isFirst)
                    builder.Append(" ");

                builder.Append(c);

                isFirst = false;

            }

            return builder.ToString();
        }

        public static String RemoveDiacritics(String s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        } 
    }
}
