using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWithLinq3.Models;

namespace MVCWithLinq3.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDAL obj = new EmployeeDAL();
        public ViewResult DisplayEmployees()
        {
            return View(obj.GetEmployees());
        }
        public ViewResult DisplayEmployee(int Eid)
        {
            return View(obj.GetEmployee(Eid));
        }
        [HttpGet]
        public ViewResult AddEmployee()
        {
            EmpDept emp = new EmpDept();
            emp.Departments = obj.GetDeprtments();
            return View(emp);
        }
        [HttpPost]
        public RedirectToRouteResult AddEmployee(EmpDept emp)
        {
            obj.Employee_Insert(emp);
            return RedirectToAction("DisplayEmployees");
        }
        public ViewResult EditEmployee(int Eid)
        {
            EmpDept Emp= obj.GetEmployee(Eid);
            Emp.Departments = obj.GetDeprtments();
            return View(Emp);
        }
        public RedirectToRouteResult UpdateEmployee(EmpDept emp)
        {
            obj.Employee_Update(emp);
            return RedirectToAction("DisplayEmployees");
        }
        public RedirectToRouteResult DeleteEmployee(int Eid)
        {
            obj.Employee_Delete(Eid);
            return RedirectToAction("DisplayEmployees");
        }
   }
}