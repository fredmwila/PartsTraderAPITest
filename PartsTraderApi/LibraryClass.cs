using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsTraderApi
{
    public class Library
    {
        public static bool PartNumberIsValid(string partNumber)
        {
            try
            {
                int partNumberLength = partNumber.Length;

                string partId = partNumber.Substring(0, 4);
                string dash = partNumber[4].ToString();
                string partCode = partNumber.Substring(5, partNumberLength - 5);

                bool validLength = (partNumberLength >= 9);
                bool validDash = (dash == "-");
                bool validPartId = partId.All(char.IsDigit);
                bool validPartCode = partCode.All(char.IsLetterOrDigit);

                return (validLength && validDash && validPartId && validPartCode);
            }
            catch 
            {
                return false;
            }
        }

    }
}

