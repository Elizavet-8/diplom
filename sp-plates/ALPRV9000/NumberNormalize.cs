using System.Text;
using System.Text.RegularExpressions;

namespace ALPRV9000
{
    class NumberNormalize
    {
        public static string getNormalizeNumber(string number)
        {
            char rus_O = 'О';
            char eng_O = 'O';
            string rus_chars = "АВЕКМНОРСТУХ";
            string eng_chars = "ABEKMHOPCTYX";
            string pattern = @"[" + rus_chars + "0][0-9" + rus_O + "][0-9" + rus_O + "][0-9" + rus_O + "][" + rus_chars + "0][" + rus_chars + "0]";
            MatchCollection match = Regex.Matches(number, pattern, RegexOptions.IgnoreCase);

            string pattern2 = @"[" + eng_chars + "0][0-9" + eng_O + "][0-9" + eng_O + "][0-9" + eng_O + "][" + eng_chars + "0][" + eng_chars + "0]";
            MatchCollection match2 = Regex.Matches(number, pattern2, RegexOptions.IgnoreCase);
            StringBuilder result = new StringBuilder();
            if (match.Count > 0)
            {
                result.Append(match[0].Value);
            }

            if (match2.Count > 0)
            {
                result.Append(match2[0].Value);

                for (int i = 0; i < eng_chars.Length; i++)
                {
                    result = result.Replace(eng_chars[i], rus_chars[i]);
                }
            }

            if (result.Length == 6)
            {
                if (result[0] == '0')
                    result[0] = rus_O;

                if (result[1] == rus_O)
                    result[1] = '0';

                if (result[2] == rus_O)
                    result[2] = '0';

                if (result[3] == rus_O)
                    result[3] = '0';

                if (result[4] == '0')
                    result[4] = rus_O;

                if (result[5] == '0')
                    result[5] = rus_O;
            }

            return result.ToString();
        }
    }
}
