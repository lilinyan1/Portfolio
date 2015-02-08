//! \author Linyan Becky Li, Bowen Zhuang
//! \date   November 26, 2014
//! \section intro Program Introduction
//! \details Assignment Requirements: \n
//!         - Modified Assignment-3 source code \n
//!         - Word (or DOxygen) document containing your Unit Test Descriptions – note in real projects, this document would form the foundation of your Test Specification and your Testing Strategy \n 
//!         - Also include a Test Report from Visual Studio showing the results of all of your tests \n
//!         - Place all of this information in one ZIP file (please make sure that both team member’s names are in the ZIP filename) \n

//This code was borrowed from the VS2008 C# Samples and was modified by S. Clarke

// StringBreakout.cs
// compile with: /target:library
using System; 

// Declare the same namespace as the one in Factorial.cs. This simply 
// allows types to be added to the same namespace.
namespace Functions 
{
    ///
    /// \brief contains methods that break down strings, and perform counting or parsing tasks
    /// 
    public class StringBreakout
    {

        ///
        /// \brief The NumberOfDigits static method counts the number of digit characters in the passed string
        /// \details <b>Details</b> 
        /// \param theString - <b>string</b> - the string where number of digits is counted
        /// \return count - <b>int </b> - the count of digits of the passed string
        /// \return -1 - <b>int </b> - return -1 when passed in null 
        public static int NumberOfDigits(string theString) 
        {
            int count = 0; 
            if (theString != null)
            {
                for (int i = 0; i < theString.Length; i++)
                {
                    if (Char.IsDigit(theString[i]))
                    {
                        count++;
                    }
                }
            }
            else
            {
                count = -1;
            }

            return count;
        }


        ///
        /// \brief The NumberOfAlphas static method counts the number of alphabetic characters in the passed string
        /// \details <b>Details</b> 
        /// \param theString - <b>string</b> - the string where number of digits is counted
        /// \return count - <b>int </b> - the count of alphabet characters of the passed string
        /// \return -1 - <b>int </b> - return -1 when passed in null 
        /// 
        public static int NumberOfAlphas(string theString)
        {
            int count = 0;
            if (theString!= null)
            {
                for (int i = 0; i < theString.Length; i++)
                {
                    if (Char.IsLetter(theString[i]))
                    {
                        count++;
                    }
                }
                // the following statement is insecure
                // numAlphas = count;
            }
            else
            {
                count = -1;
            }

            return count;
        }


        ///
        /// \brief The NumberOfOthers static method counts the number of other characters(non-digit and non-alpha characters) in the passed string 
        /// \details <b>Details</b> 
        /// \param theString - <b>string</b> - the string where number of other charact is counted
        /// \return count - <b>int </b> - the count of other characters of the passed string
        /// \return -1 - <b>int </b> - return -1 when passed in null 
        /// 
        public static int NumberOfOthers(string theString)
        {
            int count = 0;
            if (theString != null)
            {
                for (int i = 0; i < theString.Length; i++)
                {
                    if ((!Char.IsLetter(theString[i])) && (!Char.IsDigit(theString[i])))
                    {
                        count++;
                    }
                }
            }
            else
            {
                count = -1;
            }


            return count;
        }


        ///
        /// \brief Find and extract the digit of the passed string as an integer
        /// \details <b>Details</b> 
        /// \param theString - <b>string</b> - the string from which the integer is extracted
        /// \return digit - <b>int </b> - the extracted integer
        /// \return -1 - <b>int </b> - return -1 when passed in null 
        /// 
        public static int FindAndExtractDigits(string theString)
        {
            var digits = new System.Text.StringBuilder();
            int digit;
            if (theString != null)
            {
                foreach (var ch in theString)
                {
                    if (char.IsDigit(ch) == true)
                    {
                        digits.Append(ch);
                    }

                }
                // only parse the string when the string's length is greater than 0
                if (digits.Length != 0)
                {
                    digit = Int32.Parse(digits.ToString());
                }
                else
                {
                    digit = 0;
                }
            }
            else
            {
                digit = -1;
            }
           
            return digit;
        }
    }
}

