//! \mainpage Software Quality - Project - EMS
//! \author Ab-Code: Becky Linyan Li, Bowen Zhuang, Sekou Diaby, Tuan Ly 
//! \date   November 21, 2014
//! \section intro Program Introduction
//! \details The heart of this project is the development of an Employee Management System (EMS).  This system will be capable of keeping track of a number of different employees and employee-types for the huge multinational Omnicorp Corporation \n
//!          This doxygen file contains the Technical Specification and Test Plan of our EMS.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Presentation;
using TheCompany;
namespace EMS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Container employeeContainer = new Container();
            UIMenu menu = new UIMenu();
            menu.MenuRun();
        }
    }
}
