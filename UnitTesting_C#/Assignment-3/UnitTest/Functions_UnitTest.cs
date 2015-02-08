//! \mainpage String Functions Unit Test
//! \author Linyan Becky Li, Bowen Zhuang
//! \date   November 26, 2014
//! \section intro Program Introduction
//! \details Assignment Requirements: \n
//!         - Modified Assignment-3 source code \n
//!         - Word (or DOxygen) document containing your Unit Test Descriptions – note in real projects, this document would form the foundation of your Test Specification and your Testing Strategy \n 
//!         - Also include a Test Report from Visual Studio showing the results of all of your tests \n
//!         - Place all of this information in one ZIP file (please make sure that both team member’s names are in the ZIP filename) \n

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Functions;

namespace UnitTest
{
    ///
    /// \brief contains 25 automatic unit tests
    /// 
    [TestClass]  // contain this attribute for the unit test to work
    public class Functions_UnitTest
    {

        /**
        \details    Test identifier:       1_1  
        \verbatim
                    Description:                        Calculate the number of digit characters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         123  (normal test)   
                    Expected outputs:                   3   
                    Observed outputs:                   3   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod] //! contain this attribute for the unit test to work
        public void NumberOfDigits_UnitTest_1_1()
        {
            // arrange
            int expected = 3;

            // act 
           int actual =  Functions.StringBreakout.NumberOfDigits("123");

            // assert
            Assert.AreEqual(expected, actual, "Number of digits is incorrect");
        }


        /**
        \details    Test identifier:       1_2  
        \verbatim
                    Description:                        Calculate the number of digit characters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         Abc12453  (normal test)   
                    Expected outputs:                   5   
                    Observed outputs:                   5   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfDigits_UnitTest_1_2()
        {
            // arrange
            int expected = 5;

            // act 
            int actual = Functions.StringBreakout.NumberOfDigits("Abc12453");

            // assert
            Assert.AreEqual(expected, actual, "Number of digits is incorrect");
        }


 

        /**
        \details    Test identifier:       1_3  
        \verbatim
                    Description:                        Calculate the number of digit characters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         1325498777777777777777777 (normal test)   
                    Expected outputs:                   25   
                    Observed outputs:                   25   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfDigits_UnitTest_1_3()
        {
            // arrange
            int expected = 25;

            // act 
            int actual = Functions.StringBreakout.NumberOfDigits("1325498777777777777777777 ");

            // assert
            Assert.AreEqual(expected, actual, "Number of digits is incorrect");
        }


        /**
         \details    Test identifier:       1_4  
         \verbatim
                     Description:                        Calculate the number of digit characters in the passed string   
                     Method of execution:                Automatic  
                     Input data:                         (empty string, boundary test)   
                     Expected outputs:                   0   
                     Observed outputs:                   0   
                     Resulting actions if test fails:	An exception will be thrown by Assert    
          \endverbatim
          */
        [TestMethod]
        public void NumberOfDigits_UnitTest_1_4()
        {
            // arrange
            int expected = 0;

            // act 
            int actual = Functions.StringBreakout.NumberOfDigits(string.Empty);

            // assert
            Assert.AreEqual(expected, actual, "Number of digits is incorrect");
        }


        /**
        \details    Test identifier:       1_5  
        \verbatim
                    Description:                        Calculate the number of digit characters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         null  (exception test)   
                    Expected outputs:                   -1   
                    Observed outputs:                   -1   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfDigits_UnitTest_1_5()
        {
            // arrange
            int expected = -1;

            // act 
            int actual = Functions.StringBreakout.NumberOfDigits(null);

            // assert
            Assert.AreEqual(expected, actual, "Number of digits is incorrect");
        }


        /**
        \details    Test identifier:                    2_1   
        \verbatim
                    Description:                        Calculate the number of alphabet letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         abcde (normal test)   
                    Expected outputs:                   5   
                    Observed outputs:                   5   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfAlphas_UnitTest_2_1()
        {
            // arrange
            int expected = 5;

            // act 
            int actual = Functions.StringBreakout.NumberOfAlphas("abcde");

            // assert
            Assert.AreEqual(expected, actual, "Number of alphabet letters is incorrect");
        }


        /**
        \details    Test identifier:                    2_2   
        \verbatim
                    Description:                        Calculate the number of alphabet letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         123abc (normal test)   
                    Expected outputs:                   3   
                    Observed outputs:                   3   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfAlphas_UnitTest_2_2()
        {
            // arrange
            int expected = 3;

            // act 
            int actual = Functions.StringBreakout.NumberOfAlphas("123abc");

            // assert
            Assert.AreEqual(expected, actual, "Number of alphabet letters is incorrect");
        }

        /**
      \details    Test identifier:                    2_3   
      \verbatim
                  Description:                        Calculate the number of alphabet letters in the passed string   
                  Method of execution:                Automatic  
                  Input data:                          *3*a9bc0 (normal test)   
                  Expected outputs:                   3   
                  Observed outputs:                   3   
                  Resulting actions if test fails:	An exception will be thrown by Assert    
       \endverbatim
       */
        [TestMethod]
        public void NumberOfAlphas_UnitTest_2_3()
        {
            // arrange
            int expected = 3;

            // act 
            int actual = Functions.StringBreakout.NumberOfAlphas(" *3*a9bc0");

            // assert
            Assert.AreEqual(expected, actual, "Number of alphabet letters is incorrect");
        }

        /**
        \details    Test identifier:                    2_4   
        \verbatim
                    Description:                        Calculate the number of alphabet letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         (empty string, boundary test)   
                    Expected outputs:                   0   
                    Observed outputs:                   0   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfAlphas_UnitTest_2_4()
        {
            // arrange
            int expected = 0;

            // act 
            int actual = Functions.StringBreakout.NumberOfAlphas("");

            // assert
            Assert.AreEqual(expected, actual, "Number of alphabet letters is incorrect");
        }


        /**
        \details    Test identifier:                    2_5   
        \verbatim
                    Description:                        Calculate the number of alphabet letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         null  (exception test)   
                    Expected outputs:                   -1   
                    Observed outputs:                   -1   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfAlphas_UnitTest_2_5()
        {
            // arrange
            int expected = -1;

            // act 
            int actual = Functions.StringBreakout.NumberOfAlphas(null);

            // assert
            Assert.AreEqual(expected, actual, "Number of alphabet letters is incorrect");
        }





        /**
        \details    Test identifier:                    3_1   
        \verbatim
                    Description:                        Calculate the number of other letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                          1234asdf,.(normal test)   
                    Expected outputs:                   3   
                    Observed outputs:                   3   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfOthers_UnitTest_3_1()
        {
            // arrange
            int expected = 3;

            // act 
            int actual = Functions.StringBreakout.NumberOfOthers(" 1234asdf,.");

            // assert
            Assert.AreEqual(expected, actual, "Number of other letters is incorrect");
        }




        /**
        \details    Test identifier:                    3_2   
        \verbatim
                    Description:                        Calculate the number of other letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         1243567(normal test)   
                    Expected outputs:                   0   
                    Observed outputs:                   0   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfOthers_UnitTest_3_2()
        {
            // arrange
            int expected = 0;

            // act 
            int actual = Functions.StringBreakout.NumberOfOthers("1243567");

            // assert
            Assert.AreEqual(expected, actual, "The number of other letters is incorrect");
        }


        /**
        \details    Test identifier:                    3_3   
        \verbatim
                    Description:                        Calculate the number of other letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         @#$%^&*()_+++_)(*&^%$#(normal test)   
                    Expected outputs:                   22   
                    Observed outputs:                   22   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfOthers_UnitTest_3_3()
        {
            string testStr = "@#$%^&*()_+++_)(*&^%$#";
            // arrange
            int expected = testStr.Length;

            // act 
            int actual = Functions.StringBreakout.NumberOfOthers(testStr);

            // assert
            Assert.AreEqual(expected, actual, "The number of other letters is incorrect");
        }


        /**
        \details    Test identifier:                    3_4   
        \verbatim
                    Description:                        Calculate the number of other letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         (empty string, boundary test)   
                    Expected outputs:                   0   
                    Observed outputs:                   0   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfOthers_UnitTest_3_4()
        {
            // arrange
            int expected = 0;

            // act 
            int actual = Functions.StringBreakout.NumberOfOthers(string.Empty);

            // assert
            Assert.AreEqual(expected, actual, "The number of other letters is incorrect");
        }


        /**
        \details    Test identifier:                    3_5   
        \verbatim
                    Description:                        Calculate the number of other letters in the passed string   
                    Method of execution:                Automatic  
                    Input data:                         null(exception test)   
                    Expected outputs:                   6   
                    Observed outputs:                   6   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void NumberOfOthers_UnitTest_3_5()
        {
            // arrange
            int expected = -1;

            // act 
            int actual = Functions.StringBreakout.NumberOfOthers(null);

            // assert
            Assert.AreEqual(expected, actual, "Number of other letters is incorrect");
        }
        /**
        \details    Test identifier:                    4_1   
        \verbatim
                    Description:                        Unit test for FindAndExtractDigits(), FindAndExtractDigits() should extract digits from passed in string   
                    Method of execution:                Automatic  
                    Input data:                         11674 (normal test)   
                    Expected outputs:                   11674   
                    Observed outputs:                   11674   
                    Resulting actions if test fails:	An exception will be thrown by Assert    
         \endverbatim
         */
        [TestMethod]
        public void FindAndExtractDigits_UnitTest_4_1()
        {
            // arrange
            int expected = 11674;

            // act 
            int actual = Functions.StringBreakout.FindAndExtractDigits("11674");

            // assert
            Assert.AreEqual(expected, actual, "The extracted number is incorrect");
        }


        /**
        \details    Test identifier:                    4_2   
        \verbatim
                    Description:                        Unit test for FindAndExtractDigits(), FindAndExtractDigits() should extract digits from passed in string   
                    Method of execution:                Automatic  
                    Input data:                         116asdf (normal test)   
                    Expected outputs:                   116   
                    Observed outputs:                   116   
                    Resulting actions if test fails:	An exception will be thrown by Assert     
         \endverbatim
         */
        [TestMethod]
        public void FindAndExtractDigits_UnitTest_4_2()
        {
            // arrange
            int expected = 116;

            // act 
            int actual = Functions.StringBreakout.FindAndExtractDigits("116asdf");

            // assert
            Assert.AreEqual(expected, actual, "The extracted number is incorrect");
        }

        /**
        \details    Test identifier:                    4_3   
        \verbatim
                    Description:                        Unit test for FindAndExtractDigits(), FindAndExtractDigits() should extract digits from passed in string   
                    Method of execution:                Automatic  
                    Input data:                         116asdf (normal test)   
                    Expected outputs:                   116   
                    Observed outputs:                   116   
                    Resulting actions if test fails:	An exception will be thrown by Assert     
         \endverbatim
         */
        [TestMethod]
        public void FindAndExtractDigits_UnitTest_4_3()
        {
            // arrange
            int expected = 116;

            // act 
            int actual = Functions.StringBreakout.FindAndExtractDigits("a1s1df6");

            // assert
            Assert.AreEqual(expected, actual, "The extracted number is incorrect");
        }
        /**
       \details    Test identifier:                    4_4   
       \verbatim
                   Description:                        Unit test for FindAndExtractDigits(), FindAndExtractDigits() should extract digits from passed in string   
                   Method of execution:                Automatic  
                   Input data:                         (empty string, boundary test)   
                   Expected outputs:                   0   
                   Observed outputs:                   0   
                   Resulting actions if test fails:	An exception will be thrown by Assert     
        \endverbatim
        */
        [TestMethod]
        public void FindAndExtractDigits_UnitTest_4_4()
        {
            // arrange
            int expected = 0;

            // act 
            int actual = Functions.StringBreakout.FindAndExtractDigits("");

            // assert
            Assert.AreEqual(expected, actual, "The extracted number is incorrect");
        }

        /**
        \details    Test identifier:                    4_5   
        \verbatim
                    Description:                        Unit test for FindAndExtractDigits(), FindAndExtractDigits() should extract digits from passed in string   
                    Method of execution:                Automatic  
                    Input data:                         (null, exception test)    
                    Expected outputs:                   -1   
                    Observed outputs:                   -1   
                    Resulting actions if test fails:	An exception will be thrown by Assert     
         \endverbatim
         */
        [TestMethod]
        public void FindAndExtractDigits_UnitTest_4_5()
        {
            // arrange
            int expected = -1;

            // act 
            int  actual=Functions.StringBreakout.FindAndExtractDigits(null);

            // assert
            Assert.AreEqual(expected, actual, "The extracted number is incorrect");
        } 
       

 
        /**
       \details    Test identifier:                    5_1   
       \verbatim
                   Description:                        Unit test for Factorial(), The "Factorial" static method calculates the factorial value for the specified integer passed in   
                   Method of execution:                Automatic  
                   Input data:                         10 (normal test)   
                   Expected outputs:                   120   
                   Observed outputs:                   120   
                   Resulting actions if test fails:	An exception will be thrown by Assert     
        \endverbatim
        */
        [TestMethod]
        public void Calc_UnitTest_5_1()
        {
            // arrange
            int expected = 120;

            // act 
            int actual = Functions.Factorial.Calc(5);

            // assert
            Assert.AreEqual(expected, actual, "The calculation result of Factorial() is incorrect");
        }

        /**
        \details    Test identifier:                    5_2   
        \verbatim
                    Description:                        Unit test for Factorial(), The "Factorial" static method calculates the factorial value for the specified integer passed in   
                    Method of execution:                Automatic  
                    Input data:                         -1 (Normal test)   
                    Expected outputs:                   6227020800   
                    Observed outputs:                   6227020800   
                    Resulting actions if test fails:	An exception will be thrown by Assert     
         \endverbatim
         */
        [TestMethod]
        public void Calc_UnitTest_5_2()
        {
            // arrange
            Int64 expected = 6227020800;

            // act 
            Int64 actual = Functions.Factorial.Calc(13);

            // assert
            Assert.AreEqual(expected, actual, "The calculation result of Factorial() is incorrect");
        }

        /**
        \details    Test identifier:                    5_3   
        \verbatim
                    Description:                        Unit test for Factorial(), The "Factorial" static method calculates the factorial value for the specified integer passed in   
                    Method of execution:                Automatic  
                    Input data:                         -1200 (Normal test)   
                    Expected outputs:                   1   
                    Observed outputs:                   1   
                    Resulting actions if test fails:	An exception will be thrown by Assert     
         \endverbatim
         */
        [TestMethod]
        public void Calc_UnitTest_5_3()
        {
            // arrange
            int expected = 1;

            // act 
            int actual = Functions.Factorial.Calc(-1200);

            // assert
            Assert.AreEqual(expected, actual, "The calculation result of Factorial() is incorrect");
        }

        /**
        \details    Test identifier:                    5_4   
        \verbatim
                    Description:                        Unit test for Factorial(), The "Factorial" static method calculates the factorial value for the specified integer passed in   
                    Method of execution:                Automatic  
                    Input data:                         1 (Boundary test)   
                    Expected outputs:                   1   
                    Observed outputs:                   1   
                    Resulting actions if test fails:	An exception will be thrown by Assert     
         \endverbatim
         */
        [TestMethod]
        public void Calc_UnitTest_5_4()
        {
            // arrange
            int expected = 1;

            // act 
            int actual = Functions.Factorial.Calc(1);

            // assert
            Assert.AreEqual(expected, actual, "The calculation result of Factorial() is incorrect");
        }


        /**
        \details    Test identifier:                    5_5   
        \verbatim
                    Description:                        Unit test for Factorial(), The "Factorial" static method calculates the factorial value for the specified integer passed in   
                    Method of execution:                Automatic  
                    Input data:                         0 (Boundary test)   
                    Expected outputs:                   1   
                    Observed outputs:                   1   
                    Resulting actions if test fails:	An exception will be thrown by Assert     
         \endverbatim
         */
        [TestMethod]
        public void Calc_UnitTest_5_5()
        {
            // arrange
            int expected = 1;

            // act 
            int actual = Functions.Factorial.Calc(0);

            // assert
            Assert.AreEqual(expected, actual, "The calculation result of Factorial() is incorrect");
        }

        


    }
}
