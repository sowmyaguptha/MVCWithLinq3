using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWithLinq3.Models
{
    public class EmployeeDAL
    {
        MVCDBDataContext dc = new MVCDBDataContext();

        // public object Item { get; private set; }

        public List<SelectListItem> GetDeprtments()
        {
            List<SelectListItem> Depts = new List<SelectListItem>();
            foreach (var item in dc.Departments)
            {
                SelectListItem li = new SelectListItem { Text = item.Dname, Value = item.Did.ToString() };
                Depts.Add(li);
            }
            return Depts;
        }
        public EmpDept GetEmployee(int Eid)
        {
            var Record = (from E in dc.Employees join D in dc.Departments on E.Did equals D.Did where E.Eid == Eid select new { E.Eid, E.Ename, E.job, E.salary, D.Did, D.Dname, D.Location }).Single();
            EmpDept obj = new EmpDept { Eid = Record.Eid, Ename = Record.Ename, job = Record.job, salary = Record.salary, Did = Record.Did, Dname = Record.Dname, Location = Record.Location };
            return obj;
        }
        public List<EmpDept> GetEmployees()
        {
            var Records = (from E in dc.Employees join D in dc.Departments on E.Did equals D.Did where E.Status == true select new { E.Eid, E.Ename, E.job, E.salary, D.Did, D.Dname, D.Location });
            List<EmpDept> Emps = new List<EmpDept>();
            foreach (var Record in Records)
            {
                EmpDept obj = new EmpDept { Eid = Record.Eid, Ename = Record.Ename, job = Record.job, salary = Record.salary, Did = Record.Did, Dname = Record.Dname, Location = Record.Location };
                Emps.Add(obj);
            }
            return Emps;
        }
        public void Employee_Insert(EmpDept obj)
        {
            Employee Emp = new Employee { Ename = obj.Ename, job = obj.job, salary = obj.salary, Did = obj.Did, Status = true };
            dc.Employees.InsertOnSubmit(Emp);
            dc.SubmitChanges();
        }
        public void Employee_Update(EmpDept NewValues)
        {
            Employee OldValues = dc.Employees.Single(E => E.Eid == NewValues.Eid);
            OldValues.Ename = NewValues.Ename;
            OldValues.job = NewValues.job;
            OldValues.salary = NewValues.salary;
            OldValues.Did = NewValues.Did;
            dc.SubmitChanges();
        }
        public void Employee_Delete(int Eid)
        {
            Employee OldValues = dc.Employees.Single(E => E.Eid == Eid);
            OldValues.Status = false;
            dc.SubmitChanges();
        }
    }
}