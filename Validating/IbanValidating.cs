using System;
using System.Collections.Generic;

namespace Validating
{
    public static class IbanValidating
    {
        public static bool IsValid(string iban)
        {
            iban = iban.Trim();
            if (iban.Length != 20)
                return false;
            var rearrangedIban= iban.Substring(4) + "2129" + iban.Substring(2, 2);
            if (decimal.TryParse(rearrangedIban, out decimal number))
            {
                int remainder = (int)(number % 97);
                if (remainder == 1)
                    return true;
                else return false;
            }
            else return false;
        }
    }
}
