//! \author Linyan Becky Li, Bowen Zhuang
//! \date   November 26, 2014
//! \section intro Program Introduction
//! \details Assignment Requirements: \n
//!         - Modified Assignment-3 source code \n
//!         - Word (or DOxygen) document containing your Unit Test Descriptions – note in real projects, this document would form the foundation of your Test Specification and your Testing Strategy \n 
//!         - Also include a Test Report from Visual Studio showing the results of all of your tests \n
//!         - Place all of this information in one ZIP file (please make sure that both team member’s names are in the ZIP filename) \n

//This code was borrowed from the VS2008 C# Samples and was modified by S. Clarke
// Factorial.cs
// compile with: /target:library

using System;



// Declares a namespace. You need to package your libraries according
// to their namespace so the .NET runtime can correctly load the classes.
namespace Functions 
{
    ///
    /// \brief contains method that perform factorail calculation
    /// 
    public class Factorial 
    {

        ///
        /// \brief The "Factorial" static method calculates the factorial value for the specified integer passed in
        /// \details <b>Details</b> 
        /// \param theString - <b>string</b> - the integer from which the factorial is calculated
        /// \return - <b>int </b> - the factorial of the passed integer
        /// 
        public static int Calc(int i)
        {
            return ((i <= 1) ? 1 : (i * Calc(i - 1)));
        } 
    }
}

