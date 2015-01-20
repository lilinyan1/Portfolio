/**
* \file	Container.cs
* \author Ab-code: Becky Linyan Li, Bowen Zhuang, Sekou Gassama, Tuan Ly
* \date 11-21-2014
* \brief The Container class keeps all the employees in the company. You can add employee, remove employee, modify employee and search employee
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllEmployees;
using System.Collections;
using Supporting;
namespace TheCompany
{
    //
    /// \class Container
    /// \brief  The Container class keeps all the employees in the company. 
    /// \brief  You can add employee, remove employee, modify employee and search employee
    ///
    public class Container
    {
        /* ====================================== */
        /*              Private                 */
        /* ====================================== */
        /* -------------- ATTRIBUTES ------------ */
        private Dictionary<string, Employee> employeelist = new Dictionary<string, Employee>(); ///< \breif the container of all employee
        
        private Logging logger = new Logging(); ///< \breif the logger does logging work
        
        private FileIO fileManager = new FileIO();///< \breif the fileManagetr read file load to container, write employees in container to files
        

        ///
        /// \brief This function read dbfile and load all the employee to employeelist
        /// \details <b>Details</b>
        /// \param none
        /// \return none
        public bool loadData()
        {
            bool result = false;
            
            if (employeelist == null)
            {
                throw new Exception("Container does not exist.");
            }
            
            result= fileManager.ReadDatabase(ref employeelist);
            
            //logger.Log("Container", "loadData", "Load " + employeelist.Count + " employee(s) from DB file.", result);
            
            return result;
        }

        ///
        /// \brief This function save all the employees in the container to a db file 
        /// \details <b>Details</b> 
        /// \param none
        /// \return none
        public bool saveData()
        {
            if (employeelist == null)
            {
                throw new Exception("Container does not exist.");
            }
           
            bool result = fileManager.WriteDatabase(employeelist);

            //logger.Log("Container", "saveData", "save" + employeelist.Count + " employee(s) to DB file.", result);
            
            return result;
        }

        ///
        /// \brief This function add a valiad employee to container
        /// \details <b>Details</b> 
        /// \param employee - <b>Employee </b> -the employee need to be add 
        /// \return <b>bool</b> true if successful, false if successful
        public bool add(Employee employee)
        {
            bool result = false;
            
            if (employeelist == null)
            {
                throw new Exception("Container does not exist.");
            }
            if (employee != null)
            {
                bool isValid = employee.Validate();
                logger.Log("Container", "add", "Validate " + employee.GetfirstName() + "-" + employee.GetlastName() + " SIN: " + employee.GetsocialInsuranceNumber(), isValid);
                if (isValid)
                {
                    if (employeelist.ContainsKey(employee.GetsocialInsuranceNumber()))
                    {
                        result = false;
                        logger.Log("Container", "add", "Add one employee " + employee.GetfirstName() + "-" + employee.GetlastName(), result);
                    }
                    else
                    {
                        employeelist.Add(employee.GetsocialInsuranceNumber(), employee);
                        result = true;
                        logger.Log("Container", "add", "Add one employee " + employee.GetfirstName() + "-" + employee.GetlastName(), result);
                    }
                    
                }
                else
                {

                    //employee.Details();
                    //logger.Log("Container", "add", "Employee detail is called:\n " + employee.ToString(), true);
                    logger.Log("Container", "add", "Add one employee " + employee.GetfirstName()+ "-" +employee.GetlastName(), result);
                    throw new Exception("The employee is not valid.");
                }
                
            }
            
            return result;
        }

        ///
        /// \brief This function remove a employee from container
        /// \details <b>Details</b> 
        /// \param employee - <b>Employee </b> -the employee need to be remove 
        /// \return <b>bool</b> true if successful, false if failed
        public bool remove(Employee employee)
        {
            bool result = false;
            if (employeelist == null)
            {
                throw (new Exception("Container does not exist."));
            }
            if (employee != null&& employeelist!=null)
            {
                if (employeelist.ContainsKey(employee.GetsocialInsuranceNumber()))
                {
                    employeelist.Remove(employee.GetsocialInsuranceNumber());
                    result = true;
                }

                logger.Log("Container", "remove", "Remove one employee " + employee.GetfirstName() + "-" + employee.GetlastName(), result);
            }
           
            return result;
        }

        ///
        /// \brief This function reset the employee in the container
        /// \details <b>Details</b> 
        /// \param employee - <b>Employee </b> -the employee need to be reset to the container 
        /// \return <b>bool</b> true if successful, false if failed
        public bool modify(Employee employee)
        {
            bool result = true;
            Employee employeeOld = null; 
            if (employee != null)
            {

                bool isValid = employee.Validate();
                logger.Log("Container", "modify", "Validate " + employee.GetfirstName() + "-" + employee.GetlastName() + " SIN: " + employee.GetsocialInsuranceNumber(), isValid);
                if (isValid)
                {
                   employeeOld= employeelist[employee.GetsocialInsuranceNumber()];
                   employeelist[employee.GetsocialInsuranceNumber()] = employee;
                   logger.Log("Container", "modify", "Modify employee from:\n " + employeeOld.ToString() + " to " + employee.ToString(), result);
                }
                else
                {
                    logger.Log("Container", "modify", "Modify employee from:\n " + employeeOld.ToString() + " to " + employee.ToString(), result);
                    throw (new Exception("Employee to be modified is invalid."));
                }
            }
            else
            {
                logger.Log("Container", "modify", "Modify employee from:\n " + employeeOld.ToString() + " to null", result);
                throw (new Exception("Employee to be modified can not be null."));
            }
           
            return result;
        }

        ///
        /// \brief This function print every employee's message in the container
        /// \details <b>Details</b> 
        /// \param none
        /// \return none
        public void traverseEmployee()
        {
            if (employeelist == null)
            {
                throw (new Exception("Container does not exist."));
            }
            Console.WriteLine("===============================================================");
            Console.WriteLine("There is/are " + employeelist.Count + " employee(s) in this company.");
            Console.WriteLine("===============================================================");
            foreach (Employee e in employeelist.Values)
            {
                e.Details();
                logger.Log("Container", "traverseEmployee", "Employee detail is called:\n " + e.ToString(), true);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        

        /// \brief This function search a employee by its SIN
        /// \details <b>Details</b> 
        /// \param sin - <b>string </b> -the employee's SIN number
        /// \return employee - <b>Employee </b> -the employee need to be remove 
        public Employee search(string sin)
        {
            if (employeelist == null)
            {
                throw (new Exception("Container does not exist."));
            }
            if (employeelist.ContainsKey(sin))
            {
                return employeelist[sin];
            }
            else
            {
                return null;
            }
          
        }

    }
}
