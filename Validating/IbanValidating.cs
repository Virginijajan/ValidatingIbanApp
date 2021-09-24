using System;
using System.Collections.Generic;

namespace Validating
{
    public static class IbanValidating
    {
        public static Dictionary<string, int> LengthPerCountry;
        public static Dictionary<char, int> LetterToNumbers;

        static IbanValidating()
        {
            LetterToNumbers = new Dictionary<char, int>();
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();           
            for(int i=0; i<26; i++)
            {               
                LetterToNumbers.Add(alphabet[i], 10+i);
            }
            LengthPerCountry = new Dictionary<string, int>();
            LengthPerCountry["AL"] = 28;
            LengthPerCountry["AD"] = 24;
            LengthPerCountry["AT"] = 20;
            LengthPerCountry["AZ"] = 28;
            LengthPerCountry["BH"] = 22;
            LengthPerCountry["BY"] = 28;
            LengthPerCountry["BE"] = 16;
            LengthPerCountry["BA"] = 20;
            LengthPerCountry["BR"] = 29;
            LengthPerCountry["BG"] = 22;
            LengthPerCountry["CR"] = 22;
            LengthPerCountry["HR"] = 21;
            LengthPerCountry["CY"] = 28;
            LengthPerCountry["CZ"] = 24;
            LengthPerCountry["DK"] = 18;
            LengthPerCountry["DO"] = 28;
            LengthPerCountry["EG"] = 29;
            LengthPerCountry["SV"] = 28;
            LengthPerCountry["EE"] = 20;
            LengthPerCountry["FO"] = 18;
            LengthPerCountry["FI"] = 18;
            LengthPerCountry["FR"] = 27;
            LengthPerCountry["LT"] = 20;
            LengthPerCountry["GE"] = 22;
            LengthPerCountry["DE"] = 22;
            LengthPerCountry["GR"] = 27;
            LengthPerCountry["HU"] = 28;
            LengthPerCountry["IS"] = 26;
            LengthPerCountry["IE"] = 22;
            LengthPerCountry["IT"] = 27;
            LengthPerCountry["LV"] = 21;
            LengthPerCountry["LU"] = 20;
            LengthPerCountry["MT"] = 31;
            LengthPerCountry["MC"] = 27;
            LengthPerCountry["MD"] = 24;
            LengthPerCountry["NL"] = 18;
            LengthPerCountry["NO"] = 15;
            LengthPerCountry["PL"] = 28;
            LengthPerCountry["PT"] = 25;
            LengthPerCountry["RO"] = 24;
            LengthPerCountry["RS"] = 22;
            LengthPerCountry["SK"] = 24;
            LengthPerCountry["SI"] = 19;
            LengthPerCountry["ES"] = 24;
            LengthPerCountry["ES"] = 24;
            LengthPerCountry["SE"] = 24;
            LengthPerCountry["CH"] = 21;
            LengthPerCountry["GB"] = 22;
            LengthPerCountry["VA"] = 22;
        }       
        public static bool IsValid(string iban)
        {
            iban = iban.Trim();
            if (iban.Length < 5)
                return false;
            if (iban.Length != GetIbanLength(iban.Substring(0, 2)))
                return false;
           
            var rearrangedIbanString = GetRearrangedNumberString(iban);

            if (decimal.TryParse(rearrangedIbanString, out decimal number))
            {
                int remainder = (int)(number % 97);
                if (remainder == 1)
                    return true;
                else return false;
            }
            else return false;
        }
        private static int GetIbanLength(string countryCode)
        {
            if (LengthPerCountry.TryGetValue(countryCode, out int length))
                return length;
            return -1;
        }
        private static string GetRearrangedNumberString(string iban)
        {           
            string numbersString="";                     
            string rearrangedIban = $"{iban.Substring(4)}{iban.Substring(0, 4)}";                         

            foreach(var c in rearrangedIban)
            {
                if (int.TryParse(c.ToString(), out int number))
                    numbersString += number.ToString();

                else if (LetterToNumbers.TryGetValue(c, out int n))
                    numbersString += n.ToString();
                else return "";
            }
            return numbersString;
        }
    }
}
