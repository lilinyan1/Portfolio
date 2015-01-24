/**
* \file	FileIO.cs
* \author Ab-code: Becky Linyan Li, Bowen Zhuang, Sekou Gassama, Tuan Ly
* \date 11-21-2014
* \brief contain the logics of the methods of the FileIO class
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;            /// streamreader/writer
using System.Diagnostics;
using AllEmployees;

namespace Supporting
{
    ///
    /// \class FileIO
    /// \brief This class contains the methods necessary to open (for reading or writing) and close Employee DBase files and other files
    /// \details try-catch statements are used to detect exceptions, in which cases, user will be notified
    ///
    public class FileIO
    {

        // private
        // TIPs: static won't work here
        const string dBaseFileName = "ems.dBase.csv";  ///< \breif the name of database file
        const string dBaseFileExtension = "*.csv";     ///< \breif the name of database file extension
        const string dBaseFolderName = "DBase";        ///< \breif the name of database folder
        const string className = "FileIO";
        Logging logger = new Logging();                                               ///


        ///
        /// \brief Creates specified file and folder if they don't exist
        /// \details <b>Details</b> 
        /// \param fileName - <b>string </b> -the name of the file
        /// \param folderName - <b>string </b> -the name of the folder, folder should be under the same directory the executable locates
        /// \param fileFilter - <b>string </b> -file's extention, e.g. ".csv" for dBase; ".log" for log
        /// \return true <b>bool </b> - if file and folder are created successfully
        /// \return false <b>bool </b> - if file and folder are not created 
        ///
        public bool CreateFile(string fileName, string folderName, string fileFilter)
        {

            //create folder if "DBase" doesn't exist
            bool isFileExist = false;

            string folderPath = Directory.GetCurrentDirectory() + "\\" + folderName + "\\";
            try
            {
                // create the requred folder if it doesn't exist
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

                //open and scan folder if it exist
                FileInfo[] fileInfo = directoryInfo.GetFiles(fileFilter, SearchOption.AllDirectories);

                foreach (FileInfo fileInfoItem in fileInfo)
                {
                    if (fileInfoItem.Name == fileName)
                    {
                        isFileExist = true;
                    }
                }

                //create the file if it doesn't exist in the DBase folder
                if (!isFileExist)
                {
                    // Create a file to write to. 
                    StreamWriter sw = File.CreateText(folderPath + fileName);
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false ;
            }

            return true;
        }


        ///
        /// \brief Creates specified file and folder if they don't exist
        /// \details <b>Details</b>  Must create file by using CreateFile() before using GetStreamReader()
        /// \param fileName - <b>string </b> - specify the name of the file of which the stream reader is returned
        /// \param folderName - <b>string </b> -the name of the folder where file is put, folder should be under the same directory the executable locates
        /// \param fileFilter - <b>string </b> -the file's extention, e.g. ".csv" for dBase; ".log" for log
        /// \return sr <b>StreamReader </b> - when the stream reader of the specified is successfuly achieved
        /// \return null if the stream reader is not created
        /// 
        private StreamReader GetStreamReader(string fileName, string folderName, string fileFilter)
        {

            StreamReader sr=null;
            //create folder if "DBase" doesn't exist

            string folderPath = Directory.GetCurrentDirectory() + "\\" + folderName + "\\";
            try
            {

                sr = new StreamReader(folderPath + fileName);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return sr;
        }


        ///
        /// \brief Creates specified file and folder if they don't exist
        /// \details <b>Details</b>  Must create file by using CreateFile() before using GetStreamWriter()
        /// \param fileName - <b>string </b> - specify the name of the file of which the stream writer is returned
        /// \param folderName - <b>string </b> -the name of the folder where file is put, folder should be under the same directory the executable locates
        /// \param fileFilter - <b>string </b> -the file's extention, e.g. ".csv" for dBase; ".log" for log
        /// \return sr <b>StreamReader </b> - when the stream writer of the specified is successfuly achieved
        /// \return null if the stream writer is not achieved
        /// 
        public StreamWriter GetStreamWriter(string fileName, string folderName, string fileFilter)
        {
            StreamWriter sw = null;
            string folderPath = Directory.GetCurrentDirectory() + "\\" + folderName + "\\";
            try
            {
                
                if (folderName=="log")
                {
                    sw = new StreamWriter(folderPath + fileName,true);
                }
                else
                {
                    sw = new StreamWriter(folderPath + fileName,true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return sw;
        }


        ///
        /// \brief Read the database file, parse it, and store different types of employees into passed in the passed in data container
        /// \details <b>Details</b> 
        /// \param employees - <b>Dictionary<string, Employee></b> - the data container where data is stored
        /// \return true - <b>bool </b> - when the database file was successfully written to the database file
        /// \return false - <b>bool </b> - when error occured, the database file was not successfully updated
        ///
        public bool ReadDatabase(ref Dictionary<string, Employee> employees)
        {
            string line;
            UInt32 validCount = 0;
            UInt32 invalidCount = 0;
            UInt32 totalCount = 0;
            const string methodName = "ReadDatabase";
            bool ret = false;

            try
            {
                if (employees != null)
                {
                    employees.Clear();
                    CreateFile(dBaseFileName, dBaseFolderName, dBaseFileExtension);
                    StreamReader sr = GetStreamReader(dBaseFileName, dBaseFolderName, dBaseFileExtension);
                    if (sr != null)
                    {
                        using (sr)
                        {
                            // keep reading line untial the end of the file
                            while (sr.Peek() >= 0)
                            {
                                //read a new line
                                line = sr.ReadLine();

                                if (line == "")
                                {
                                   
                                    continue;
                                }


                                // start parse
                                if (line[0] == ';')
                                {
                                    // isComment, ignore
                                }
                                else
                                {
                                    totalCount++;

                                    // is employee record
                                    // Split string on |
                                    string[] words = line.Split('|');

                                    // put line to employee
                                    if ((words[0] == "F") && (words.Length >= 8))
                                    {
                                        // a fulltime employee record
                                        FulltimeEmployee fEmployee = new FulltimeEmployee();

                                        fEmployee.SetemployeeType(words[0][0]);
                                        fEmployee.SetlastName(words[1]);
                                        fEmployee.SetfirstName(words[2]);
                                        fEmployee.SetsocialInsuranceNumber(words[3]);
                                        fEmployee.SetdateOfBirth(words[4]);
                                        fEmployee.SetdateOfHire(words[5]);
                                        fEmployee.SetdateOfTermination(words[6]);
                                        if (words[7] != "")
                                        {
                                            fEmployee.Setsalary(float.Parse(words[7]));
                                        }

                                        bool validRet = false;
                                        try
                                        {
                                            validRet = fEmployee.Validate();
                                            logger.Log("UIMenu", methodName, "Validate " + fEmployee.GetfirstName() + "-" + fEmployee.GetlastName() + " SIN: " + fEmployee.GetsocialInsuranceNumber(), true);
                                        }
                                        catch(Exception e)
                                        { 
                                            logger.Log("UIMenu", methodName, "Validate " + fEmployee.GetfirstName() + "-" + fEmployee.GetlastName() + " SIN: " + fEmployee.GetsocialInsuranceNumber(), false);
                                            Console.WriteLine("Employee with the sin: " + fEmployee.GetsocialInsuranceNumber() + " is not valid - " + e.Message);
                                        }
                                        //if (true)
                                        if (validRet)
                                        {
                                            validCount++;
                                            try
                                            {
                                                // add record to dictionary if it is valide
                                                employees.Add(fEmployee.GetsocialInsuranceNumber(), fEmployee);

                                                //2010-09-01 07:02:00 [FileIO.ParseRecord] Employee - Clarke,Sean (333 333 333)INVALID
                                                logger.Log(className, methodName, "Employee - " + fEmployee.GetlastName() + "," + fEmployee.GetfirstName()
                                                    + " (" + fEmployee.GetsocialInsuranceNumber() + ")", true);
                                            }
                                            catch
                                            {
                                                // catch dictionary.add() exception
                                                logger.Log(className, methodName, "Duplicate or empty SIN for Employee - " + fEmployee.GetlastName() + "," + fEmployee.GetfirstName()
                                                    + " (" + fEmployee.GetsocialInsuranceNumber() + ")", false);
                                            }

                                        }
                                        else
                                        {
                                            // log invalid record
                                            invalidCount++;
                                            logger.Log(className, methodName, "Employee - " + fEmployee.GetlastName() + "," + fEmployee.GetfirstName()
                                                + " (" + fEmployee.GetsocialInsuranceNumber() + ")", false);
                                        }

                                    }
                                    else if ((words[0] == "P") && (words.Length >= 8))
                                    {
                                        // a part time employee record
                                        ParttimeEmployee pEmployee = new ParttimeEmployee();

                                        pEmployee.SetemployeeType(words[0][0]);
                                        pEmployee.SetlastName(words[1]);
                                        pEmployee.SetfirstName(words[2]);
                                        pEmployee.SetsocialInsuranceNumber(words[3]);
                                        pEmployee.SetdateOfBirth(words[4]);
                                        pEmployee.SetdateOfHire(words[5]);
                                        pEmployee.SetdateOfTermination(words[6]);
                                        pEmployee.SethourlyRate(float.Parse(words[7]));

                                        bool validRet = false;
                                        try
                                        {
                                            validRet = pEmployee.Validate();
                                            logger.Log("UIMenu", methodName, "Validate " + pEmployee.GetfirstName() + "-" + pEmployee.GetlastName() + " SIN: " + pEmployee.GetsocialInsuranceNumber(), true);
                                        }
                                        catch(Exception e)
                                        {
                                            logger.Log("UIMenu", methodName, "Validate " + pEmployee.GetfirstName() + "-" + pEmployee.GetlastName() + " SIN: " + pEmployee.GetsocialInsuranceNumber(), false);
                                            Console.WriteLine("Employee with the sin: " + pEmployee.GetsocialInsuranceNumber() + " is not valid - " + e.Message);
                                        }

                                        //if (true)
                                        if (validRet)
                                        {
                                            validCount++;

                                            try
                                            {
                                                // add record to dictionary if it is valide
                                                employees.Add(pEmployee.GetsocialInsuranceNumber(), pEmployee);
                                                logger.Log(className, methodName, "Employee - " + pEmployee.GetlastName() + "," + pEmployee.GetfirstName()
                                                    + " (" + pEmployee.GetsocialInsuranceNumber() + ") ", true);
                                            }
                                            catch
                                            {
                                                // catch dictionary.add() exception
                                                logger.Log(className, methodName, "Duplicate or empty SIN for Employee - " + pEmployee.GetlastName() + "," + pEmployee.GetfirstName()
                                                    + " (" + pEmployee.GetsocialInsuranceNumber() + ")", false);
                                            }

                                        }
                                        else
                                        {
                                            invalidCount++;
                                            logger.Log(className, methodName, "Employee - " + pEmployee.GetlastName() + "," + pEmployee.GetfirstName()
                                                + " (" + pEmployee.GetsocialInsuranceNumber() + ") ", false);
                                        }
                                    }
                                    else if ((words[0] == "C") && (words.Length >= 8))
                                    {
                                        // a contract employee record
                                        ContractEmployee cEmployee = new ContractEmployee();

                                        cEmployee.SetemployeeType(words[0][0]);
                                        cEmployee.SetlastName(words[1]);
                                        cEmployee.SetfirstName(words[2]);
                                        cEmployee.SetsocialInsuranceNumber(words[3]);
                                        cEmployee.SetdateOfBirth(words[4]);
                                        cEmployee.SetcontractStartDate(words[5]);
                                        cEmployee.SetcontractStopDate(words[6]);
                                        cEmployee.SetfixedContractAmount(float.Parse(words[7]));

                                        bool validRet = false;
                                        try
                                        {
                                            validRet = cEmployee.Validate();
                                            logger.Log("UIMenu", methodName, "Validate " + cEmployee.GetfirstName() + "-" + cEmployee.GetlastName() + " SIN: " + cEmployee.GetsocialInsuranceNumber(), true);
                                            
                                        }
                                        catch (Exception e)
                                        {
                                            logger.Log("UIMenu", methodName, "Validate " + cEmployee.GetfirstName() + "-" + cEmployee.GetlastName() + " SIN: " + cEmployee.GetsocialInsuranceNumber(), false);
                                            Console.WriteLine("Employee with the sin: " + cEmployee.GetsocialInsuranceNumber() + " is not valid - " + e.Message);
                                        }

                                        //if (true)
                                        if (validRet)
                                        {
                                            validCount++;

                                            try
                                            {
                                                // add record to dictionary if it is valide
                                                employees.Add(cEmployee.GetsocialInsuranceNumber(), cEmployee);
                                                logger.Log(className, methodName, "Employee - " + cEmployee.GetlastName() + "," + cEmployee.GetfirstName()
                                                    + " (" + cEmployee.GetsocialInsuranceNumber() + ") ", true);
                                            }
                                            catch
                                            {
                                                // catch dictionary.add() exception
                                                logger.Log(className, methodName, "Duplicate or empty SIN for Employee - " + cEmployee.GetlastName() + "," + cEmployee.GetfirstName()
                                                    + " (" + cEmployee.GetsocialInsuranceNumber() + ")", false);
                                            }
                                        }
                                        else
                                        {
                                            invalidCount++;
                                            logger.Log(className, methodName, "Employee - " + cEmployee.GetlastName() + "," + cEmployee.GetfirstName()
                                                + " (" + cEmployee.GetsocialInsuranceNumber() + ") ", false);
                                        }

                                    }
                                    else if ((words[0] == "S") && (words.Length >= 7))
                                    {
                                        // a seasonal employee record
                                        SeasonalEmployee sEmployee = new SeasonalEmployee();

                                        sEmployee.SetemployeeType(words[0][0]);
                                        sEmployee.SetlastName(words[1]);
                                        sEmployee.SetfirstName(words[2]);
                                        sEmployee.SetsocialInsuranceNumber(words[3]);
                                        sEmployee.SetdateOfBirth(words[4]);
                                        sEmployee.Setseason(words[5]);
                                        sEmployee.SetpiecePay(float.Parse(words[6]));


                                        bool validRet = false;
                                        try
                                        {
                                            validRet = sEmployee.Validate();
                                            logger.Log("UIMenu", methodName, "Validate " + sEmployee.GetfirstName() + "-" + sEmployee.GetlastName() + " SIN: " + sEmployee.GetsocialInsuranceNumber(), true);
                                        }
                                        catch (Exception e)
                                        {
                                            logger.Log("UIMenu", methodName, "Validate " + sEmployee.GetfirstName() + "-" + sEmployee.GetlastName() + " SIN: " + sEmployee.GetsocialInsuranceNumber(), false);
                                            Console.WriteLine("Employee with the SIN: " + sEmployee.GetsocialInsuranceNumber() + " is not valid. - " + e.Message);
                                        }

                                        //if (true)
                                        if (validRet)
                                        {
                                            validCount++;
                                            try
                                            {
                                                // add record to dictionary if it is valide
                                                employees.Add(sEmployee.GetsocialInsuranceNumber(), sEmployee);
                                                logger.Log(className, methodName, "Employee - " + sEmployee.GetlastName() + "," + sEmployee.GetfirstName()
                                                    + " (" + sEmployee.GetsocialInsuranceNumber() + ") ", true);
                                            }
                                            catch
                                            {
                                                // catch dictionary.add() exception
                                                logger.Log(className, methodName, "Duplicate or empty SIN for Employee - " + sEmployee.GetlastName() + "," + sEmployee.GetfirstName()
                                                    + " (" + sEmployee.GetsocialInsuranceNumber() + ")", false);
                                            }

                                        }
                                        else
                                        {
                                            invalidCount++;
                                            logger.Log(className, methodName, "Employee - " + sEmployee.GetlastName() + "," + sEmployee.GetfirstName()
                                                + " (" + sEmployee.GetsocialInsuranceNumber() + ") ", false);
                                        }

                                    }
                                    else
                                    {
                                        // invalid record line
                                        invalidCount++;
                                        logger.Log(className, methodName, "Invalid line - " + line , false);
                                    }
                                }

                            }
                            // log read summary
                            string sEvent = "Valid Record Count: " + validCount.ToString() + ", Invalid Record Count: " +
                                invalidCount.ToString() + ", Total Read Count: " + totalCount.ToString();
                            Console.WriteLine(sEvent);
                            logger.Log(className, methodName, sEvent, true);
                            ret = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Stream reader is null");
                        logger.Log(className, methodName, "Stream reader is null", false);
                        ret = false;
                    }
                }
                else
                {
                    // null object
                    Console.WriteLine("Employee containter is null;");
                    logger.Log(className, methodName, "Employee containter is null;", false);
                    ret = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.Log(className, methodName, e.ToString(), false);
                ret = false;
            }
            return ret;
        }


        ///
        /// \brief Read the data container that got passed in, then store different types of employees into the data base file
        /// \details <b>Details</b> 
        /// \param employees - <b>Dictionary<string, Employee></b> - data container where data is stored
        /// \return true - <b>bool </b> - when the data container was successfully written to the data base file, the data base file name and its
        ///                                 folder name are specified in the class private attributes: dBaseFileName, dBaseFolderName, dBaseFileExtension.
        ///                                 The data base folder located under the same folder of the executable 
        /// \return false - <b>bool </b> - when error occured, the database file was not successfully updated or created
        ///
        public bool WriteDatabase(Dictionary<string, Employee> employees)
        {
            string folderPath = Directory.GetCurrentDirectory() + "\\" + dBaseFolderName + "\\";
            UInt32 validCount = 0;
            UInt32 invalidCount = 0;
            UInt32 totalCount = 0;
            Employee empTemp = new Employee();
            string sEvent;
            const string methodName = "WriteDatabase";
            bool ret = false;
            
            try
            {
                if (employees != null)
                {
                    CreateFile(dBaseFileName, dBaseFolderName, dBaseFileExtension);
                    using (StreamWriter sw = new StreamWriter(folderPath + dBaseFileName))
                    {
                        sw.Write("");
                    }


                    // get employee one by one from dictionary
                    foreach (KeyValuePair<string, Employee> entry in employees)
                    {
                        empTemp = entry.Value;

                        // validate employess
                        // if employee type is contract
                        if (empTemp.GetemployeeType() == 'C')
                        {
                            ContractEmployee cEmployeeTemp = (ContractEmployee)empTemp;

                            bool validRet = false;
                            try
                            {
                                validRet = cEmployeeTemp.Validate();
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + cEmployeeTemp.GetfirstName() + "-" + cEmployeeTemp.GetlastName() + " SIN: " + cEmployeeTemp.GetsocialInsuranceNumber(), true);
                            }
                            catch (Exception e)
                            {
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + cEmployeeTemp.GetfirstName() + "-" + cEmployeeTemp.GetlastName() + " SIN: " + cEmployeeTemp.GetsocialInsuranceNumber(), false);
                                Console.WriteLine("Employee with the sin: "+cEmployeeTemp.GetsocialInsuranceNumber() + " is not valid - " + e.Message);
                            }

                            //if(true)
                            if (validRet)
                            {
                                validCount++;

                                string empRecord = cEmployeeTemp.GetemployeeType().ToString() + "|" + cEmployeeTemp.GetlastName() + "||" +
                                    cEmployeeTemp.GetsocialInsuranceNumber() + "|" + cEmployeeTemp.GetdateOfBirth() + "|" + cEmployeeTemp.GetcontractStartDate() +
                                    "|" + cEmployeeTemp.GetcontractStopDate() + "|" + cEmployeeTemp.GetfixedContractAmount() + "|";
                                using (StreamWriter sw = GetStreamWriter(dBaseFileName, dBaseFolderName, dBaseFileExtension))
                                {
                                    sw.WriteLine(empRecord, true);
                                }
                            }
                            else
                            {
                                // not a valid employee
                                invalidCount++;
                            }

                        }
                        // if employee type is full time
                        else if (empTemp.GetemployeeType() == 'F')
                        {
                            FulltimeEmployee fEmployee = (FulltimeEmployee) empTemp;

                            bool validRet = false;
                            try
                            {
                                validRet = fEmployee.Validate();
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + fEmployee.GetfirstName() + "-" + fEmployee.GetlastName() + " SIN: " + fEmployee.GetsocialInsuranceNumber(), validRet);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Employee with the sin: " + fEmployee.GetsocialInsuranceNumber() + " is not valid - " + e.Message);
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + fEmployee.GetfirstName() + "-" + fEmployee.GetlastName() + " SIN: " + fEmployee.GetsocialInsuranceNumber(), false);
                            }

                            //if (true)
                            if (validRet)
                            {
                                validCount++;

                                string empRecord = fEmployee.GetemployeeType().ToString() + "|" + fEmployee.GetlastName() + "|" + fEmployee.GetfirstName() + "|" +
                                    fEmployee.GetsocialInsuranceNumber() + "|" + fEmployee.GetdateOfBirth() + "|" + fEmployee.GetdateOfHire() + "|" + 
                                    fEmployee.GetdateOfTermination() + "|" + fEmployee.Getsalary() + "|";
                                using (StreamWriter sw = GetStreamWriter(dBaseFileName, dBaseFolderName, dBaseFileExtension))
                                {
                                    sw.WriteLine(empRecord,true);
                                }
                            }
                            else
                            {
                                // not a valid employee
                                invalidCount++;
                            }
                        }
                        // employee type is part time
                        else if (empTemp.GetemployeeType() == 'P')
                        {
                            ParttimeEmployee pEmployee = (ParttimeEmployee) empTemp;

                            bool validRet = false;
                            try
                            {
                                validRet = pEmployee.Validate();
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + pEmployee.GetfirstName() + "-" + pEmployee.GetlastName() + " SIN: " + pEmployee.GetsocialInsuranceNumber(), validRet);
                            }
                            catch (Exception e)
                            {
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + pEmployee.GetfirstName() + "-" + pEmployee.GetlastName() + " SIN: " + pEmployee.GetsocialInsuranceNumber(), false);
                                Console.WriteLine(pEmployee.GetsocialInsuranceNumber() + " - " + e.Message);
                            }

                            //if (true)
                            if (validRet)
                            {
                                validCount++;

                                string empRecord = pEmployee.GetemployeeType().ToString() + "|" + pEmployee.GetlastName() + "|" + pEmployee.GetfirstName() + "|" +
                                    pEmployee.GetsocialInsuranceNumber() + "|" + pEmployee.GetdateOfBirth() + 
                                    "|" + pEmployee.GetdateOfHire() + "|" + pEmployee.GetdateOfTermination() + "|" + pEmployee.GethourlyRate().ToString() + "|";
                                using (StreamWriter sw = GetStreamWriter(dBaseFileName, dBaseFolderName, dBaseFileExtension))
                                {
                                    sw.WriteLine(empRecord, true);
                                }
                            }
                            else
                            {
                                // not a valid employee
                                invalidCount++;
                            }
                        }
                        // employee type is seasonal employee
                        else if (empTemp.GetemployeeType() == 'S')
                        {
                            SeasonalEmployee sEmployee = (SeasonalEmployee) empTemp;

                            bool validRet = false;
                            try
                            {
                                validRet = sEmployee.Validate();
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + sEmployee.GetfirstName() + "-" + sEmployee.GetlastName() + " SIN: " + sEmployee.GetsocialInsuranceNumber(), validRet);
                            }
                            catch (Exception e)
                            {
                                logger.Log("UIMenu", "WriteDatabase", "Validate " + sEmployee.GetfirstName() + "-" + sEmployee.GetlastName() + " SIN: " + sEmployee.GetsocialInsuranceNumber(), false);
                                Console.WriteLine("Employee with the SIN: "+sEmployee.GetsocialInsuranceNumber() + " is not valid. - " + e.Message);
                            }

                            //if (true)
                            if (validRet)
                            {
                                validCount++;

                                string empRecord = sEmployee.GetemployeeType().ToString() + "|" + sEmployee.GetlastName() + "|" + sEmployee.GetfirstName() + "|" 
                                    + sEmployee.GetsocialInsuranceNumber() + "|" + sEmployee.GetdateOfBirth() + "|"
                                    + sEmployee.Getseason() + "|" + sEmployee.GetpiecePay().ToString() + "|";
                                using (StreamWriter sw = GetStreamWriter(dBaseFileName, dBaseFolderName, dBaseFileExtension))
                                {
                                    sw.WriteLine(empRecord, true);
                                }
                            }
                            else
                            {
                                // not a valid employee
                                invalidCount++;
                            }
                        }
                        else
                        {
                            // not a valid employee type
                            invalidCount++;

                        }
                        totalCount++;
                        // Write the file.
                    }
                    //2010-09-01 07:02:00 [FileIO.WriteDataBase] TotalNumberWritten:12,TotalValidRecord:10, TotalInvalidRecord:2
                    sEvent = "Valid Record Count: " + validCount.ToString() + ", Invalid Record Count: " +
                        invalidCount.ToString() + ", Total Write Count: " + totalCount.ToString();
                    Console.WriteLine(sEvent);
                    logger.Log(className, methodName, sEvent, ret);
                    ret = true;
                }
                else
                {

                    logger.Log(className, methodName, "Data container is null", ret);
                            
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.Log(className, methodName, e.ToString(), ret);
                ret = false ;
            }
            return ret;
        }


        ///
        /// \brief Writes the creation date of the file and comment placeholder to the file contents
        /// \details <b>Details</b> 
        /// \param none
        /// \return true - <b>bool </b> - when the creation date of the file and comment placeholder was successfully written to the data 
        ///                                 base file
        /// \return false - <b>bool </b> - when exception occured
        ///
        private bool WriteDateComment ()
        {
            bool ret = false;

            string dateComment = ";DateCreated: " + DateTime.Now + System.Environment.NewLine + ";Further Comments: " + System.Environment.NewLine;

            try
            {
                using (StreamWriter sw = GetStreamWriter(dBaseFileName, dBaseFolderName, dBaseFileExtension))
                {
                    sw.Write(dateComment);
                    ret = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ret = false;
            }

            return ret;
        }

    }
}

