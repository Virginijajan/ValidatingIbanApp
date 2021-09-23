using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validating
{
    public static class ValidatingIbanFile
    {
        public static string ValidateFile(string fileName)
        {
            List < string >ibanList= new List<string>();
            var newFileName = fileName.Remove(fileName.IndexOf(".")) + ".out.txt";
            if (File.Exists(fileName))
            {
                var ibanStrings=File.ReadAllLines(fileName);
                ibanList=ibanStrings.ToList();
            }
            else
            {
                return $"File {fileName} don't exists.";
            }
            
                var ibanListOut = CreateNewIbanList(ibanList);
                File.WriteAllLines(newFileName, ibanListOut);
                return $"New IBAN list {newFileName} is created.";
            
        }


       private static List<string> CreateNewIbanList(List<string> ibanList)
        {
            List<string> ibanListOut = new List<string>();
            string newIbanString;
            foreach (string iban in ibanList)
            {
                if (IbanValidating.IsValid(iban))
                {
                    newIbanString = iban + " " + "true";
                }
                else
                    newIbanString = iban + " " + "false";
                ibanListOut.Add(newIbanString);
            }
            return ibanListOut;
        }
    }
}
